"""Small real PE fixtures for metadata validation; no application execution."""
import functools, pathlib, subprocess, tempfile
@functools.cache
def assembly(marker=True, ai=False):
 folder=pathlib.Path(tempfile.mkdtemp(prefix='makrivo-store-test-'))
 (folder/'fixture.csproj').write_text('<Project Sdk="Microsoft.NET.Sdk"><PropertyGroup><TargetFramework>net10.0</TargetFramework></PropertyGroup>'+('<ItemGroup><EmbeddedResource Include="marker.json" LogicalName="MAKRIVO.Store.NoAi.json" /></ItemGroup>' if marker else '')+'</Project>')
 (folder/'marker.json').write_text('{"schemaVersion":1,"storeRelease":true,"aiAssistantIncluded":false}')
 (folder/'Fixture.cs').write_text('public class '+('MeetingModelClient' if ai else 'Fixture')+' {}')
 result=subprocess.run(['dotnet','build',str(folder/'fixture.csproj'),'-v:q'],capture_output=True,text=True)
 if result.returncode: raise RuntimeError(result.stdout+result.stderr)
 return folder/'bin/Debug/net10.0/fixture.dll'
