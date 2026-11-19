# Inspect PE metadata and IL only; never load or execute packaging input.
function Assert-StoreNoAutostart([string]$AssemblyPath) {
 $stream=$null;$pe=$null
 try {
  $stream=[IO.File]::OpenRead($AssemblyPath)
  $pe=[Reflection.PortableExecutable.PEReader]::new($stream)
  $metadata=[Reflection.Metadata.PEReaderExtensions]::GetMetadataReader($pe)
  function AssertRejectingBody($body,[string]$label) {
   if($body.ExceptionRegions.Length){throw "Unexpected $label exception handler"}
   $il=$body.GetILBytes()
   if($il.Length -ne 11 -or $il[0] -ne 0x72 -or $il[5] -ne 0x73 -or $il[10] -ne 0x7a){throw "$label must reject calls without invoking them"}
   $token=[BitConverter]::ToInt32($il,6)
   if(($token -band 0xff000000) -ne 0x0a000000){throw 'Invalid exception constructor token'}
   $member=$metadata.GetMemberReference([Reflection.Metadata.Ecma335.MetadataTokens]::MemberReferenceHandle($token -band 0x00ffffff))
   if($metadata.GetString($member.Name) -cne '.ctor' -or $member.Parent.Kind -ne [Reflection.Metadata.HandleKind]::TypeReference){throw 'Invalid exception constructor'}
   $typeRef=$metadata.GetTypeReference([Reflection.Metadata.TypeReferenceHandle]::op_Explicit($member.Parent))
   if($metadata.GetString($typeRef.Namespace) -cne 'System' -or $metadata.GetString($typeRef.Name) -cne 'InvalidOperationException'){throw "Unexpected $label exception"}
  }
  $markers=0
  foreach($handle in $metadata.ManifestResources){
   $resource=$metadata.GetManifestResource($handle)
   if($metadata.GetString($resource.Name) -ceq 'MAKRIVO.Store.NoAutostart.json'){
    $markers++
    if(!$resource.Implementation.IsNil){throw 'Marker must be embedded'}
    $reader=$pe.GetSectionData($pe.PEHeaders.CorHeader.ResourcesDirectory.RelativeVirtualAddress).GetReader()
    $reader.Offset=[int]$resource.Offset;$length=$reader.ReadInt32()
    if($length -lt 1 -or $length -gt 1024){throw 'Invalid marker size'}
    $data=[Text.Encoding]::UTF8.GetString($reader.ReadBytes($length)) | ConvertFrom-Json -AsHashtable
    if($data.schemaVersion -ne 1 -or $data.storeRelease -isnot [bool] -or !$data.storeRelease -or $data.autoStartIncluded -isnot [bool] -or $data.autoStartIncluded){throw 'Invalid Store autostart marker'}
   }
  }
  if($markers -ne 1){throw 'Missing or duplicate Store autostart marker'}
  $policies=@();$controllers=@()
  foreach($handle in $metadata.TypeDefinitions){
   $type=$metadata.GetTypeDefinition($handle)
   if($metadata.GetString($type.Namespace) -ceq 'Aivana.Desktop' -and $metadata.GetString($type.Name) -ceq 'AutoStartPolicy'){$policies+=,$type}
   if($metadata.GetString($type.Namespace) -ceq 'Aivana.Desktop' -and $metadata.GetString($type.Name) -ceq 'DesktopController'){$controllers+=,$type}
  }
  if($policies.Count -ne 1){throw 'Missing or duplicate Store autostart policy'}
  $methods=@{}
  foreach($handle in $policies[0].GetMethods()){
   $method=$metadata.GetMethodDefinition($handle);$name=$metadata.GetString($method.Name)
   if($methods.ContainsKey($name)){throw 'Duplicate Store autostart policy method'}
   if(!$method.RelativeVirtualAddress -or !($method.Attributes -band [Reflection.MethodAttributes]::Static)){throw 'Invalid Store autostart method'}
   $body=[Reflection.Metadata.PEReaderExtensions]::GetMethodBody($pe,$method.RelativeVirtualAddress)
   if($body.ExceptionRegions.Length){throw 'Unexpected Store autostart exception handler'}
   $methods[$name]=$body
  }
  # The supported getter must return false. Apply must only construct and throw
  # InvalidOperationException: no call to the supplied registry-writing delegate.
  if($methods.Count -ne 2 -or !$methods.ContainsKey('get_Supported') -or !$methods.ContainsKey('Apply')){throw 'Unexpected Store autostart policy methods'}
  $getter=$methods['get_Supported'].GetILBytes();$apply=$methods['Apply']
  if($getter.Length -ne 2 -or $getter[0] -ne 0x16 -or $getter[1] -ne 0x2a){throw 'Store autostart must report unsupported'}
  AssertRejectingBody $apply 'Store autostart policy'
  if($controllers.Count -ne 1){throw 'Missing or duplicate Store autostart controller'}
  $setters=@()
  foreach($handle in $controllers[0].GetMethods()){
   $method=$metadata.GetMethodDefinition($handle)
   if($metadata.GetString($method.Name) -ceq 'SetAutoStart'){$setters+=,$method}
  }
  if($setters.Count -ne 1 -or !$setters[0].RelativeVirtualAddress -or ($setters[0].Attributes -band [Reflection.MethodAttributes]::Static)){throw 'Missing or invalid Store autostart controller method'}
  AssertRejectingBody ([Reflection.Metadata.PEReaderExtensions]::GetMethodBody($pe,$setters[0].RelativeVirtualAddress)) 'Store autostart controller'
  # Reject known managed Registry write APIs anywhere in the desktop assembly.
  foreach($handle in $metadata.MemberReferences){
   $member=$metadata.GetMemberReference($handle)
   if($member.Parent.Kind -ne [Reflection.Metadata.HandleKind]::TypeReference){continue}
   $typeRef=$metadata.GetTypeReference([Reflection.Metadata.TypeReferenceHandle]::op_Explicit($member.Parent))
   $ns=$metadata.GetString($typeRef.Namespace);$typeName=$metadata.GetString($typeRef.Name);$name=$metadata.GetString($member.Name)
   if($ns -ceq 'Microsoft.Win32' -and (($typeName -ceq 'RegistryKey' -and $name -in @('CreateSubKey','SetValue','DeleteValue','DeleteSubKey','DeleteSubKeyTree','SetAccessControl')) -or ($typeName -ceq 'Registry' -and $name -ceq 'SetValue'))){throw "Store autostart forbidden registry writer: $typeName.$name"}
  }
  foreach($handle in $metadata.MethodDefinitions){
   $method=$metadata.GetMethodDefinition($handle);$name=$metadata.GetString($method.Name)
   if($method.Attributes -band [Reflection.MethodAttributes]::PinvokeImpl){
    $import=$method.GetImport();$entry=$metadata.GetString($import.Name)
    if($name -match '^(RegCreateKey|RegSet(Value|KeyValue)|RegDelete(Key|Value|Tree)|RegCopyTree)' -or $entry -match '^(RegCreateKey|RegSet(Value|KeyValue)|RegDelete(Key|Value|Tree)|RegCopyTree)'){throw "Store autostart forbidden native registry writer: $entry"}
   }
  }
 } catch {throw "Store autostart assembly verification failed ($AssemblyPath): $_"}
 finally {if($pe){$pe.Dispose()};if($stream){$stream.Dispose()}}
}
