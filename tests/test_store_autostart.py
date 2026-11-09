import json
import pathlib
import subprocess
import tempfile
import unittest
import store_assembly_fixture

ROOT = pathlib.Path(__file__).resolve().parents[1]


class StoreAutostartTests(unittest.TestCase):
    def test_compiled_guard_rejects_old_and_unsafe_even_with_marker(self):
        for marker, unsafe, accepted in [(False, False, False), (True, True, False), (True, 'getter', False), (True, False, True)]:
            with self.subTest(marker=marker, unsafe=unsafe):
                assembly = store_assembly_fixture.assembly(autostart_marker=marker, autostart_unsafe=unsafe)
                result = subprocess.run(['pwsh', '-NoProfile', '-Command',
                    f". '{ROOT / 'tools/Assert-StoreNoAutostart.ps1'}'; Assert-StoreNoAutostart '{assembly}'"], capture_output=True, text=True)
                self.assertEqual(result.returncode == 0, accepted, result.stdout + result.stderr)

    def test_package_checker_rejects_old_assembly_after_rehashing_everything(self):
        from test_store_package import StorePackageTests
        fixture = StorePackageTests()
        with tempfile.TemporaryDirectory() as temporary:
            root = pathlib.Path(temporary)
            fixture.fixture(root)
            (root / 'payload/MAKRIVO.dll').write_bytes(store_assembly_fixture.assembly(autostart_marker=False).read_bytes())
            fixture.archive_and_inventory(root)
            result = fixture.check(root)
            self.assertNotEqual(result.returncode, 0, result.stdout + result.stderr)
            self.assertIn('Store autostart', result.stdout + result.stderr)

    def test_guard_cannot_be_bypassed_by_controller_or_another_registry_writer(self):
        for controller in ('unguarded', 'registry', 'native'):
            with self.subTest(controller=controller):
                assembly = store_assembly_fixture.assembly(controller=controller)
                result = subprocess.run(['pwsh', '-NoProfile', '-Command',
                    f". '{ROOT / 'tools/Assert-StoreNoAutostart.ps1'}'; Assert-StoreNoAutostart '{assembly}'"], capture_output=True, text=True)
                self.assertNotEqual(result.returncode, 0, result.stdout + result.stderr)
                self.assertIn('Store autostart', result.stdout + result.stderr)

    def test_store_and_desktop_select_exactly_one_policy_even_when_constants_overridden(self):
        project = ROOT / 'src/Aivana.Desktop/Aivana.Desktop.csproj'
        for store in (True, False):
            result = subprocess.run([
                'dotnet', 'msbuild', str(project),
                '-p:StoreRelease=' + str(store).lower(),
                '-p:DefineConstants=USER_OVERRIDE', '-getItem:Compile'
            ], capture_output=True, text=True)
            self.assertEqual(result.returncode, 0, result.stderr)
            items = {i['Identity'] for i in json.loads(result.stdout)['Items']['Compile']}
            self.assertEqual('AutoStartPolicy.Store.cs' in items, store)
            self.assertEqual('AutoStartPolicy.Desktop.cs' in items, not store)
            self.assertEqual('DesktopController.Autostart.Store.cs' in items, store)
            self.assertEqual('DesktopController.Autostart.Desktop.cs' in items, not store)

    def test_policy_denies_writes_for_store_and_preserves_desktop_behavior(self):
        for store in (True, False):
            source = ROOT / 'src/Aivana.Desktop' / ('AutoStartPolicy.Store.cs' if store else 'AutoStartPolicy.Desktop.cs')
            self.assertTrue(source.is_file(), str(source))
            with tempfile.TemporaryDirectory() as temporary:
                root = pathlib.Path(temporary)
                (root / 'Policy.csproj').write_text(f'''<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup><OutputType>Exe</OutputType><TargetFramework>net10.0</TargetFramework><ImplicitUsings>enable</ImplicitUsings></PropertyGroup>
  <ItemGroup><Compile Include="{source.as_posix()}" Link="Policy.cs" /></ItemGroup>
</Project>''', encoding='utf-8')
                (root / 'Program.cs').write_text('''using Aivana.Desktop;
bool touched = false;
bool store = args[0] == "store";
if (AutoStartPolicy.Supported == store) return 1;
try { AutoStartPolicy.Apply(() => touched = true); }
catch (InvalidOperationException) { if (!store || touched) return 2; return 0; }
return store || !touched ? 3 : 0;
''', encoding='utf-8')
                result = subprocess.run(['dotnet', 'run', '--project', str(root / 'Policy.csproj'), '--', 'store' if store else 'desktop'], capture_output=True, text=True)
                self.assertEqual(result.returncode, 0, result.stdout + result.stderr)


if __name__ == '__main__':
    unittest.main()
