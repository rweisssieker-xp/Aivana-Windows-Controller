import hashlib,json,pathlib,subprocess,tempfile,unittest,zipfile,os
ROOT=pathlib.Path(__file__).resolve().parents[1]
class RetailTests(unittest.TestCase):
 def fixture(self,p):
  for name in ['app/MAKRIVO.exe','app/coreclr.dll','app/hostfxr.dll','app/hostpolicy.dll','app/onnx/coreclr.dll','app/example-plugin/coreclr.dll','app/Aivana.AutomationHost.exe','app/Aivana.MobileHost.exe','app/onnx/Aivana.OnnxHost.exe','app/example-plugin/Aivana.PluginHost.exe','app/wwwroot/panel.js','legal/EULA-review-draft.md','legal/privacy-data-flows.md','licenses/test/LICENSE.txt']:
   f=p/name;f.parent.mkdir(parents=True,exist_ok=True);f.write_text('fixture')
  (p/'legal/product.json').write_text(json.dumps(dict(product='MAKRIVO',priceMinor=2900,currency='EUR',taxBehavior='inclusive',maxDevices=4,majorVersion=1,billing='one_time',perpetual=True)))
  (p/'third-party-notices.json').write_text(json.dumps([dict(id='test',version='1.0',files=['licenses/test/LICENSE.txt'])]))
  (p/'release-status.json').write_text(json.dumps(dict(releaseApproved=False,unsignedCandidate=True,experimentalOnnxIncluded=True)))
  self.inventory(p)
 def inventory(self,p):
  files={f.relative_to(p).as_posix():hashlib.sha256(f.read_bytes()).hexdigest() for f in p.rglob('*') if f.is_file() and f.name!='inventory.json'}
  canonical=''.join(f'{n} {files[n]}\n' for n in sorted(files) if n not in ['inventory.json','release-status.json','legal/acceptance.json'])
  digest=hashlib.sha256(canonical.encode()).hexdigest()
  status=json.loads((p/'release-status.json').read_text());status['packageBuildSha256']=digest;(p/'release-status.json').write_text(json.dumps(status))
  files['release-status.json']=hashlib.sha256((p/'release-status.json').read_bytes()).hexdigest()
  (p/'inventory.json').write_text(json.dumps(dict(schemaVersion=1,packageBuildSha256=digest,files=files)))
 def check(self,p,unsigned=True):
  return subprocess.run(['pwsh','-NoProfile','-File',str(ROOT/'tools/check-retail-package.ps1'),'-PackagePath',str(p)]+(['-AllowUnsignedCandidate'] if unsigned else []),capture_output=True,text=True)
 def test_valid_unsigned_fixture(self):
  with tempfile.TemporaryDirectory() as d:
   p=pathlib.Path(d);self.fixture(p);r=self.check(p);self.assertEqual(r.returncode,0,r.stdout+r.stderr)
 def test_rejects_missing_tamper_unexpected_secrets_vendor_wrong_terms(self):
  for scenario in ['host','notices','tamper','unexpected','private','vendor','price','seats','approval','unsigned','nested_runtime_notice','vendor_archive','hidden_private','hidden_runtime','hidden_deps']:
   with self.subTest(scenario=scenario),tempfile.TemporaryDirectory() as d:
    p=pathlib.Path(d);self.fixture(p)
    if scenario=='host': (p/'app/Aivana.MobileHost.exe').unlink();self.inventory(p)
    if scenario=='notices': (p/'licenses/test/LICENSE.txt').unlink();self.inventory(p)
    if scenario=='tamper': (p/'app/MAKRIVO.exe').write_text('changed')
    if scenario=='unexpected': (p/'extra').write_text('extra')
    if scenario=='private': (p/'app/key.pem').write_text('-----BEGIN PRIVATE KEY-----');self.inventory(p)
    if scenario=='vendor': (p/'app/LogitechLedEnginesWrapper.dll').write_text('vendor');self.inventory(p)
    if scenario in ['price','seats']:
     f=p/'legal/product.json';v=json.loads(f.read_text());v['priceMinor' if scenario=='price' else 'maxDevices']=99;f.write_text(json.dumps(v));self.inventory(p)
    if scenario in ['hidden_runtime','hidden_deps']:
     f=p/('app/onnx/hidden.runtimeconfig.json' if scenario=='hidden_runtime' else 'app/hidden.deps.json')
     data={'runtimeOptions':{'includedFrameworks':[{'name':'Microsoft.NETCore.App','version':'10.0.4'}]}} if scenario=='hidden_runtime' else {'libraries':{'Unnoticed/1.2.3':{'type':'package'}}}
     f.write_text(json.dumps(data));subprocess.run(['attrib','+H',str(f)],check=True);self.inventory(p)
    if scenario=='hidden_private':
     f=p/'app/secret.pem';f.write_text('-----BEGIN PRIVATE KEY-----');subprocess.run(['attrib','+H',str(f)],check=True)
    if scenario=='vendor_archive':
     with zipfile.ZipFile(p/'app/hidden.zip','w') as z:z.writestr('iCUESDK.x64.dll','vendor')
     self.inventory(p)
    if scenario=='nested_runtime_notice':
     f=p/'app/onnx/runtime.runtimeconfig.json';f.write_text(json.dumps({'runtimeOptions':{'includedFrameworks':[{'name':'Microsoft.NETCore.App','version':'10.0.4'}]}}));self.inventory(p)
    if scenario=='approval': (p/'release-status.json').write_text('{"releaseApproved":true,"unsignedCandidate":true}');self.inventory(p)
    self.assertNotEqual(self.check(p,scenario!='unsigned').returncode,0)
 def test_invalid_msi_versions_rejected_before_build(self):
  for version in ['1.0','1.0.0.1','256.0.0','1.256.0','1.0.65536','1.0.1-beta']:
   with self.subTest(version=version),tempfile.TemporaryDirectory() as d:
    output=pathlib.Path(d)/'new-package'
    result=subprocess.run(['pwsh','-NoProfile','-File',str(ROOT/'tools/publish-retail.ps1'),'-PublicKeyFile',str(pathlib.Path(d)/'missing.json'),'-CommerceUrl','https://commerce.invalid','-OutputDirectory',str(output),'-PackageVersion',version,'-AllowUnsignedCandidate'],capture_output=True,text=True)
    self.assertNotEqual(result.returncode,0);self.assertIn('PackageVersion must contain',result.stdout+result.stderr);self.assertFalse(output.exists())
 def test_onnx_absent_is_explicit_optional_experiment(self):
  with tempfile.TemporaryDirectory() as d:
   p=pathlib.Path(d);self.fixture(p)
   for f in (p/'app/onnx').iterdir():f.unlink()
   (p/'app/onnx').rmdir()
   status=json.loads((p/'release-status.json').read_text());status['experimentalOnnxIncluded']=False;(p/'release-status.json').write_text(json.dumps(status));self.inventory(p)
   result=self.check(p);self.assertEqual(result.returncode,0,result.stdout+result.stderr)
 def test_onnx_flag_must_match_payload(self):
  with tempfile.TemporaryDirectory() as d:
   p=pathlib.Path(d);self.fixture(p);status=json.loads((p/'release-status.json').read_text());status['experimentalOnnxIncluded']=False;(p/'release-status.json').write_text(json.dumps(status));self.inventory(p)
   self.assertNotEqual(self.check(p).returncode,0)
 def test_stale_acceptance_rejects_changed_managed_code_with_fresh_inventory(self):
  with tempfile.TemporaryDirectory() as d:
   p=pathlib.Path(d);self.fixture(p);(p/'app/MAKRIVO.dll').write_text('managed-v1');(p/'MAKRIVO.msi').write_text('msi');self.inventory(p)
   old=json.loads((p/'inventory.json').read_text())['packageBuildSha256']
   acceptance=dict(legalAcceptance=dict(approved=True,evidence='fixture'),physicalMatrix={k:dict(status='passed',evidence='fixture',buildSha256=old,executedAt='2026-10-13T00:00:00Z',testOnly=False) for k in ['hardware','lan','dpi','endurance','install','upgrade','uninstall']})
   (p/'legal/acceptance.json').write_text(json.dumps(acceptance));(p/'release-status.json').write_text(json.dumps(dict(releaseApproved=False,unsignedCandidate=False,experimentalOnnxIncluded=True)))
   host_hash=hashlib.sha256((p/'app/MAKRIVO.exe').read_bytes()).hexdigest();(p/'app/MAKRIVO.dll').write_text('managed-v2');self.inventory(p)
   self.assertEqual(host_hash,hashlib.sha256((p/'app/MAKRIVO.exe').read_bytes()).hexdigest())
   result=self.check(p,False);self.assertNotEqual(result.returncode,0);self.assertIn('Actual acceptance evidence missing',result.stdout+result.stderr)
if __name__=='__main__':unittest.main()
