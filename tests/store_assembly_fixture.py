"""Small real PE fixtures for metadata validation; no application execution."""
import functools, pathlib, subprocess, tempfile
@functools.cache
def assembly(marker=True, ai=False, autostart_marker=True, autostart_unsafe=False, controller='safe'):
 folder=pathlib.Path(tempfile.mkdtemp(prefix='makrivo-store-test-'))
 (folder/'fixture.csproj').write_text('<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup><TargetFramework>net10.0</TargetFramework></PropertyGroup>'+('<ItemGroup><EmbeddedResource Include="marker.json" LogicalName="MAKRIVO.Store.NoAi.json" /></ItemGroup>' if marker else '')+('<ItemGroup><EmbeddedResource Include="autostart.json" LogicalName="MAKRIVO.Store.NoAutostart.json" /></ItemGroup>' if autostart_marker else '')+'</Project>')
 (folder/'marker.json').write_text('{"schemaVersion":1,"storeRelease":true,"aiAssistantIncluded":false}')
 (folder/'autostart.json').write_text('{"schemaVersion":1,"storeRelease":true,"autoStartIncluded":false}')
 policy = ('public static bool Supported => false; public static void Apply(System.Action action) => action();' if autostart_unsafe is True else 'public static bool Supported => '+('true' if autostart_unsafe == 'getter' else 'false')+'; public static void Apply(System.Action action) => throw new System.InvalidOperationException("Autostart is unavailable in this Store release.");')
 controller_body = 'System.GC.KeepAlive(enabled);' if controller == 'unguarded' else 'throw new System.InvalidOperationException("Autostart is unavailable in this Store release.");'
 writer = ('public void AnotherWriter() { Microsoft.Win32.Registry.CurrentUser.CreateSubKey("unsafe-example"); }' if controller == 'registry' else '[System.Runtime.InteropServices.DllImport("advapi32.dll", EntryPoint="RegSetValueExW")] private static extern int AnotherWriter();' if controller == 'native' else '')
 (folder/'Fixture.cs').write_text('public class '+('MeetingModelClient' if ai else 'Fixture')+' {} namespace Aivana.Desktop { internal static class AutoStartPolicy { '+policy+' } public class DesktopController { public void SetAutoStart(bool enabled) { '+controller_body+' } '+writer+' } }')
 result=subprocess.run(['dotnet','build',str(folder/'fixture.csproj'),'-c','Release','-v:q'],capture_output=True,text=True)
 if result.returncode: raise RuntimeError(result.stdout+result.stderr)
 return folder/'bin/Release/net10.0/fixture.dll'
