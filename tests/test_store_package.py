import hashlib, json, pathlib, subprocess, tempfile, unittest, zipfile, struct, zlib
import test_retail_package

ROOT = pathlib.Path(__file__).resolve().parents[1]


class StorePackageTests(unittest.TestCase):
    def publish(self, output, **overrides):
        values = dict(RetailPackagePath=str(output.parent / 'missing-retail'),
                      OutputDirectory=str(output), PackageName='Aivana.MAKRIVO',
                      Publisher='CN=Aivana GmbH', PublisherDisplayName='Aivana GmbH',
                      PackageVersion='1.0.0.0')
        values.update(overrides)
        command = ['pwsh', '-NoProfile', '-File', str(ROOT / 'tools/publish-store.ps1')]
        for key, value in values.items():
            command.extend(['-' + key, value])
        return subprocess.run(command, capture_output=True, text=True)

    def test_invalid_versions_fail_before_creating_output(self):
        for version in ['1.0.0', '1.0.0.1', '65536.0.0.0', '01.0.0.0', '0.0.0.0', '1.2.-3.0']:
            with self.subTest(version=version), tempfile.TemporaryDirectory() as temporary:
                output = pathlib.Path(temporary) / 'out'
                result = self.publish(output, PackageVersion=version)
                self.assertNotEqual(result.returncode, 0)
                self.assertIn('PackageVersion must', result.stdout + result.stderr)
                self.assertFalse(output.exists())

    def test_invalid_identity_and_reserved_test_identity_fail_before_output(self):
        for values in [dict(PackageName='bad name'), dict(PackageName='../MAKRIVO'),
                       dict(Publisher='not-a-distinguished-name'), dict(PublisherDisplayName=''),
                       dict(PackageName='MAKRIVO.TestOnly', Publisher='CN=MAKRIVO Test Only')]:
            with self.subTest(values=values), tempfile.TemporaryDirectory() as temporary:
                output = pathlib.Path(temporary) / 'out'
                result = self.publish(output, **values)
                self.assertNotEqual(result.returncode, 0)
                self.assertIn('identity', (result.stdout + result.stderr).lower())
                self.assertFalse(output.exists())

    def test_existing_output_is_preserved(self):
        with tempfile.TemporaryDirectory() as temporary:
            output = pathlib.Path(temporary) / 'out'
            output.mkdir()
            marker = output / 'keep.txt'
            marker.write_text('preserve')
            result = self.publish(output)
            self.assertNotEqual(result.returncode, 0)
            self.assertIn('OutputDirectory must be new', result.stdout + result.stderr)
            self.assertEqual(marker.read_text(), 'preserve')

    def test_contained_output_rejected_before_source_validation(self):
        with tempfile.TemporaryDirectory() as temporary:
            source = pathlib.Path(temporary) / 'source'
            source.mkdir()
            output = source / 'out'
            result = self.publish(output, RetailPackagePath=str(source))
            self.assertNotEqual(result.returncode, 0)
            self.assertIn('overlap', result.stdout + result.stderr)
            self.assertFalse(output.exists())

    def test_source_inventory_tamper_rejected_before_output(self):
        with tempfile.TemporaryDirectory() as temporary:
            source = pathlib.Path(temporary) / 'source'
            source.mkdir()
            test_retail_package.RetailTests().fixture(source)
            (source / 'app/MAKRIVO.exe').write_text('tampered after inventory')
            output = pathlib.Path(temporary) / 'out'
            result = self.publish(output, RetailPackagePath=str(source))
            self.assertNotEqual(result.returncode, 0)
            self.assertIn('Inventory mismatch', result.stdout + result.stderr)
            self.assertFalse(output.exists())

    def fixture(self, root):
        payload = root / 'payload'
        for name in ['MAKRIVO.exe', 'MAKRIVO.dll', 'coreclr.dll', 'Aivana.AutomationHost.exe',
                     'Aivana.MobileHost.exe', 'example-plugin/Aivana.PluginHost.exe', 'wwwroot/panel.js',
                     'third-party-notices.json']:
            target = payload / name
            target.parent.mkdir(parents=True, exist_ok=True)
            target.write_text('package fixture')
        (payload / 'legal').mkdir()
        (payload / 'legal/product.json').write_text(json.dumps(dict(priceMinor=2900, maxDevices=4, billing='one_time', perpetual=True)))
        (payload / 'StoreAssets').mkdir()
        def png(size):
            def chunk(kind, data):
                return struct.pack('>I', len(data)) + kind + data + struct.pack('>I', zlib.crc32(kind + data))
            return b'\x89PNG\r\n\x1a\n' + chunk(b'IHDR', struct.pack('>IIBBBBB', size, size, 8, 6, 0, 0, 0)) + chunk(b'IDAT', zlib.compress((b'\x00' + b'\0' * size * 4) * size)) + chunk(b'IEND', b'')
        for size in [44, 50, 150]:
            (payload / f'StoreAssets/Logo{size}.png').write_bytes(png(size))
        (payload / 'AppxManifest.xml').write_text('''<Package xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10" xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10" xmlns:r="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities"><Identity Name="MAKRIVO.TestOnly" Publisher="CN=MAKRIVO Test Only" Version="1.0.0.0" ProcessorArchitecture="x64"/><Applications><Application Executable="MAKRIVO.exe" EntryPoint="Windows.FullTrustApplication"/></Applications><Capabilities><Capability Name="internetClient"/><Capability Name="privateNetworkClientServer"/><r:Capability Name="runFullTrust"/></Capabilities></Package>''')
        status = dict(schemaVersion=1, releaseApproved=False, submissionReady=False, unsigned=True,
                      testIdentity=True, testRetailSource=True, packageName='MAKRIVO.TestOnly',
                      publisher='CN=MAKRIVO Test Only', packageVersion='1.0.0.0',
                      licenseModel='external_one_time_four_own_pcs', priceMinor=2900, currency='EUR')
        (root / 'store-status.json').write_text(json.dumps(status))
        self.archive_and_inventory(root)

    def archive_and_inventory(self, root):
        with zipfile.ZipFile(root / 'MAKRIVO.msix', 'w') as archive:
            for file in (root / 'payload').rglob('*'):
                if file.is_file():
                    archive.write(file, file.relative_to(root / 'payload').as_posix())
            archive.writestr('AppxBlockMap.xml', '<BlockMap/>')
            archive.writestr('[Content_Types].xml', '<Types/>')
        self.inventory(root)

    def inventory(self, root):
        status = json.loads((root / 'store-status.json').read_text())
        status['msixSha256'] = hashlib.sha256((root / 'MAKRIVO.msix').read_bytes()).hexdigest()
        (root / 'store-status.json').write_text(json.dumps(status))
        files = {file.relative_to(root).as_posix(): hashlib.sha256(file.read_bytes()).hexdigest()
                 for file in root.rglob('*') if file.is_file() and file.name != 'store-inventory.json'}
        (root / 'store-inventory.json').write_text(json.dumps(dict(schemaVersion=1, files=files)))

    def check(self, root):
        return subprocess.run(['pwsh', '-NoProfile', '-File', str(ROOT / 'tools/check-store-package.ps1'),
                               '-PackagePath', str(root)], capture_output=True, text=True)

    def test_checker_accepts_structural_fixture_and_rejects_mutations(self):
        for scenario in ['valid', 'tamper', 'extra', 'false_approval', 'identity', 'capability', 'price', 'wrong_logo', 'payload_rehashed', 'archive_rehashed']:
            with self.subTest(scenario=scenario), tempfile.TemporaryDirectory() as temporary:
                root = pathlib.Path(temporary)
                self.fixture(root)
                if scenario == 'tamper':
                    (root / 'payload/MAKRIVO.dll').write_text('changed')
                if scenario == 'extra':
                    (root / 'unexpected.txt').write_text('extra')
                if scenario == 'false_approval':
                    path = root / 'store-status.json'
                    status = json.loads(path.read_text()); status['submissionReady'] = True
                    path.write_text(json.dumps(status)); self.archive_and_inventory(root)
                if scenario in ['identity', 'capability']:
                    path = root / 'payload/AppxManifest.xml'
                    path.write_text(path.read_text().replace('CN=MAKRIVO Test Only', 'CN=wrong') if scenario == 'identity' else path.read_text().replace('internetClient', 'broadFileSystemAccess'))
                    self.archive_and_inventory(root)
                if scenario == 'price':
                    (root / 'payload/legal/product.json').write_text('{"priceMinor":99}')
                    self.archive_and_inventory(root)
                if scenario == 'wrong_logo':
                    (root / 'payload/StoreAssets/Logo44.png').write_bytes((root / 'payload/StoreAssets/Logo50.png').read_bytes())
                    self.archive_and_inventory(root)
                if scenario == 'payload_rehashed':
                    (root / 'payload/MAKRIVO.dll').write_text('changed with a fresh outer inventory')
                    self.inventory(root)
                if scenario == 'archive_rehashed':
                    with zipfile.ZipFile(root / 'MAKRIVO.msix') as archive:
                        files = {entry.filename: archive.read(entry) for entry in archive.infolist()}
                    files['MAKRIVO.dll'] = b'changed archive member'
                    with zipfile.ZipFile(root / 'MAKRIVO.msix', 'w') as archive:
                        for name, content in files.items():
                            archive.writestr(name, content)
                    self.inventory(root)
                result = self.check(root)
                if scenario == 'valid':
                    self.assertEqual(result.returncode, 0, result.stdout + result.stderr)
                else:
                    self.assertNotEqual(result.returncode, 0, result.stdout + result.stderr)
                    if scenario in ['payload_rehashed', 'archive_rehashed']:
                        self.assertIn('MSIX member differs', result.stdout + result.stderr)


if __name__ == '__main__':
    unittest.main()
