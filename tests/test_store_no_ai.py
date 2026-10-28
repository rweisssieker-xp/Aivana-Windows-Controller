import pathlib, subprocess, tempfile, unittest
import store_assembly_fixture
ROOT=pathlib.Path(__file__).resolve().parents[1]
class StoreNoAiTests(unittest.TestCase):
 def test_store_compile_items_remove_transport_and_ui_even_with_constants_overridden(self):
  for project, excluded in [('Aivana.Windows','MeetingModelClient.cs'),('Aivana.Desktop','MainWindow.MeetingAssistant.cs')]:
   r=subprocess.run(['dotnet','msbuild',str(ROOT/'src'/project/(project+'.csproj')),'-p:StoreRelease=true','-p:DefineConstants=USER_OVERRIDE','-getItem:Compile'],capture_output=True,text=True)
   self.assertEqual(r.returncode,0,r.stderr)
   self.assertNotIn('"Identity": "'+excluded+'"',r.stdout)
 def test_ordinary_build_keeps_optional_assistant(self):
  import json
  for project, included in [('Aivana.Windows','MeetingModelClient.cs'),('Aivana.Desktop','MainWindow.MeetingAssistant.cs')]:
   result=subprocess.run(['dotnet','msbuild',str(ROOT/'src'/project/(project+'.csproj')),'-p:StoreRelease=false','-getItem:Compile'],capture_output=True,text=True)
   self.assertEqual(result.returncode,0,result.stderr)
   self.assertIn(included,[item['Identity'] for item in json.loads(result.stdout)['Items']['Compile']])
 def test_packaging_rejects_assemblies_without_no_ai_marker(self):
  with tempfile.TemporaryDirectory() as temp:
   script=pathlib.Path(temp)/'check.ps1'
   script.write_text(". '"+str(ROOT/'tools/Assert-StoreNoAi.ps1')+"'\nAssert-StoreNoAi '"+str(store_assembly_fixture.assembly(marker=False))+"'\n")
   r=subprocess.run(['pwsh','-NoProfile','-File',str(script)],capture_output=True,text=True)
   self.assertNotEqual(r.returncode,0)
   self.assertIn('Store no-AI',r.stdout+r.stderr)

class StoreMetadataTests(unittest.TestCase):
 def test_marker_cannot_hide_present_transport(self):
  for marker, ai, valid in [(True,False,True),(True,True,False),(False,False,False)]:
   with self.subTest(marker=marker,ai=ai):
    path=store_assembly_fixture.assembly(marker,ai)
    script=". '"+str(ROOT/'tools/Assert-StoreNoAi.ps1')+"'; Assert-StoreNoAi '"+str(path)+"'"
    result=subprocess.run(['pwsh','-NoProfile','-Command',script],capture_output=True,text=True)
    self.assertEqual(result.returncode==0,valid,result.stdout+result.stderr)

class StorePayloadTests(unittest.TestCase):
 def test_rehashed_onnx_payload_is_rejected_even_without_disclosure_flag(self):
  from test_store_package import StorePackageTests
  fixture=StorePackageTests()
  for name, missing_flag in [('onnx/Aivana.OnnxHost.dll',False),('Aivana.OnnxHost.dll',False),('onnxruntime.dll',False),('models/meeting.onnx',False),('models/local.gguf',False),('models/weights.safetensors',False),('onnx/Aivana.OnnxHost.dll',True)]:
   with self.subTest(name=name,missing_flag=missing_flag), tempfile.TemporaryDirectory() as temporary:
    root=pathlib.Path(temporary); fixture.fixture(root)
    path=root/'payload'/name; path.parent.mkdir(parents=True,exist_ok=True); path.write_text('experimental host/model')
    if missing_flag:
     import json
     status_path=root/'store-status.json'; status=json.loads(status_path.read_text()); del status['experimentalOnnxIncluded']; status_path.write_text(json.dumps(status))
    fixture.archive_and_inventory(root)
    result=fixture.check(root)
    self.assertNotEqual(result.returncode,0,result.stdout+result.stderr)
    self.assertIn('ONNX',result.stdout+result.stderr)

if __name__=='__main__': unittest.main()
