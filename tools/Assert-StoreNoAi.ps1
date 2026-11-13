# Inspect PE metadata only; never load or execute packaging input.
function Assert-StoreNoAi([string]$AssemblyPath) {
 $stream=$null;$pe=$null
 try {
  $stream=[IO.File]::OpenRead($AssemblyPath)
  $pe=[Reflection.PortableExecutable.PEReader]::new($stream)
  $metadata=[Reflection.Metadata.PEReaderExtensions]::GetMetadataReader($pe)
  $marker=$false
  foreach($handle in $metadata.ManifestResources){
   $resource=$metadata.GetManifestResource($handle)
   if($metadata.GetString($resource.Name) -ceq 'MAKRIVO.Store.NoAi.json'){
    if(!$resource.Implementation.IsNil){throw 'Marker must be embedded'}
    $reader=$pe.GetSectionData($pe.PEHeaders.CorHeader.ResourcesDirectory.RelativeVirtualAddress).GetReader()
    $reader.Offset=[int]$resource.Offset;$length=$reader.ReadInt32()
    if($length -lt 1 -or $length -gt 1024){throw 'Invalid marker size'}
    $data=[Text.Encoding]::UTF8.GetString($reader.ReadBytes($length)) | ConvertFrom-Json -AsHashtable
    $marker=$data.schemaVersion -eq 1 -and $data.storeRelease -is [bool] -and $data.storeRelease -eq $true -and $data.aiAssistantIncluded -is [bool] -and $data.aiAssistantIncluded -eq $false
   }
  }
  if(!$marker){throw 'Missing compiled Store no-AI marker'}
  foreach($handle in $metadata.TypeDefinitions){
   $type=$metadata.GetTypeDefinition($handle)
   if($metadata.GetString($type.Name) -in @('MeetingModelClient','CompatibleMeetingClient')){throw 'AI model transport is present'}
   foreach($methodHandle in $type.GetMethods()){
    if($metadata.GetString($metadata.GetMethodDefinition($methodHandle).Name) -in @('ShowMeetingAssistant','StageMeetingDraft')){throw 'AI assistant route is present'}
   }
  }
 } catch { throw "Store no-AI assembly verification failed ($AssemblyPath): $_" }
 finally {if($pe){$pe.Dispose()};if($stream){$stream.Dispose()}}
}
function Assert-StoreNoAiPayload([string]$PayloadPath) {
 foreach($file in Get-ChildItem -LiteralPath $PayloadPath -File -Recurse -Force){
  $relative=[IO.Path]::GetRelativePath($PayloadPath,$file.FullName).Replace('\','/')
  if($relative -match '(?i)(^|/)onnx(/|$)' -or $file.Name -match '(?i)^(Aivana\.OnnxHost|Microsoft\.ML\.OnnxRuntime|onnxruntime)(\.|_|$)' -or $file.Extension -match '(?i)^\.(onnx|gguf|safetensors)$'){
   throw "Store no-AI release forbids ONNX/runtime/model payload: $relative"
  }
 }
}
