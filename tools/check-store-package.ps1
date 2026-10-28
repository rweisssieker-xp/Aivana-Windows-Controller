param([Parameter(Mandatory)][string]$PackagePath)
$ErrorActionPreference='Stop'
$root=[IO.Path]::GetFullPath($PackagePath)
function ReadJson($name){Get-Content -LiteralPath (Join-Path $root $name) -Raw | ConvertFrom-Json -AsHashtable}
foreach($name in @('MAKRIVO.msix','store-status.json','store-inventory.json','payload/AppxManifest.xml','payload/MAKRIVO.exe','payload/MAKRIVO.dll','payload/coreclr.dll','payload/Aivana.AutomationHost.exe','payload/Aivana.MobileHost.exe','payload/example-plugin/Aivana.PluginHost.exe','payload/wwwroot/panel.js','payload/third-party-notices.json','payload/legal/product.json')){if(!(Test-Path -LiteralPath (Join-Path $root $name) -PathType Leaf)){throw "Missing Store package file: $name"}}
$status=ReadJson 'store-status.json'
if($status.schemaVersion -ne 1 -or $status.releaseApproved -ne $false -or $status.submissionReady -ne $false -or $status.unsigned -ne $true -or $status.testIdentity -isnot [bool] -or $status.testRetailSource -isnot [bool]){throw 'Store candidate cannot claim release/submission approval'}
if($status.licenseModel -cne 'external_one_time_four_own_pcs' -or $status.priceMinor -ne 2900 -or $status.currency -cne 'EUR'){throw 'Store commercial terms changed'}
if($status.experimentalOnnxIncluded -isnot [bool] -or $status.experimentalOnnxIncluded -ne $false){throw 'Store no-AI release forbids experimental ONNX payload'}
if($status.storeRelease -isnot [bool] -or $status.storeRelease -ne $true -or $status.aiAssistantIncluded -isnot [bool] -or $status.aiAssistantIncluded -ne $false){throw 'Store no-AI release metadata required'}
$manifest=ReadJson 'store-inventory.json'
if($manifest.schemaVersion -ne 1 -or !$manifest.files){throw 'Invalid Store inventory'}
foreach($directory in Get-ChildItem -LiteralPath $root -Directory -Recurse -Force){if($directory.Attributes -band [IO.FileAttributes]::ReparsePoint){throw 'Store directory links forbidden'}}
$actual=@{}
foreach($file in Get-ChildItem -LiteralPath $root -File -Recurse -Force){
 if($file.Attributes -band [IO.FileAttributes]::ReparsePoint){throw 'Store file links forbidden'}
 $relative=[IO.Path]::GetRelativePath($root,$file.FullName).Replace('\','/')
 if($relative -ceq 'store-inventory.json'){continue}
 $actual[$relative]=(Get-FileHash -LiteralPath $file.FullName -Algorithm SHA256).Hash.ToLowerInvariant()
}
if($actual.Count -ne $manifest.files.Count){throw 'Store inventory count mismatch'}
foreach($name in $actual.Keys){if(!$manifest.files.ContainsKey($name) -or $actual[$name] -cne $manifest.files[$name]){throw "Store inventory mismatch: $name"}}
if($status.msixSha256 -cne $actual['MAKRIVO.msix']){throw 'MSIX digest mismatch'}
[xml]$xml=Get-Content -LiteralPath (Join-Path $root 'payload/AppxManifest.xml') -Raw
$ns=[Xml.XmlNamespaceManager]::new($xml.NameTable);$ns.AddNamespace('f','http://schemas.microsoft.com/appx/manifest/foundation/windows10');$ns.AddNamespace('r','http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities');$ns.AddNamespace('u','http://schemas.microsoft.com/appx/manifest/uap/windows10')
$identity=$xml.SelectSingleNode('/f:Package/f:Identity',$ns)
if($identity.Name -cne $status.packageName -or $identity.Publisher -cne $status.publisher -or $identity.Version -cne $status.packageVersion -or $identity.ProcessorArchitecture -cne 'x64'){throw 'Manifest identity differs from status'}
if($status.testIdentity -and ($identity.Name -cne 'MAKRIVO.TestOnly' -or $identity.Publisher -cne 'CN=MAKRIVO Test Only')){throw 'Test identity status mismatch'}
if(!$status.testIdentity -and ($identity.Name -eq 'MAKRIVO.TestOnly' -or $identity.Publisher -eq 'CN=MAKRIVO Test Only')){throw 'Test identity was not disclosed'}
$applications=$xml.SelectNodes('/f:Package/f:Applications/f:Application',$ns)
if($applications.Count -ne 1 -or $applications[0].Executable -cne 'MAKRIVO.exe' -or $applications[0].EntryPoint -cne 'Windows.FullTrustApplication'){throw 'Unexpected Store entry point'}
$capabilities=@($xml.SelectNodes('/f:Package/f:Capabilities/*',$ns) | ForEach-Object {$_.Name})
if($capabilities.Count -ne 3 -or @('internetClient','privateNetworkClientServer','runFullTrust' | Where-Object {$_ -notin $capabilities}).Count){throw 'Unexpected Store capabilities'}
Add-Type -AssemblyName System.Drawing
foreach($size in @(44,50,150)){
 $image=[Drawing.Image]::FromFile((Join-Path $root "payload/StoreAssets/Logo$size.png"))
 try{if($image.Width -ne $size -or $image.Height -ne $size){throw 'Store logo dimensions invalid'}}finally{$image.Dispose()}
}
$product=ReadJson 'payload/legal/product.json'
if($product.priceMinor -ne 2900 -or $product.maxDevices -ne 4 -or $product.billing -cne 'one_time' -or $product.perpetual -ne $true){throw 'Packaged license terms changed'}
$archive=[IO.Compression.ZipFile]::OpenRead((Join-Path $root 'MAKRIVO.msix'))
try{
 $names=@{}
 foreach($entry in $archive.Entries){
  $name=$entry.FullName.Replace('\','/')
  if($name -match '(^|/)\.\.(/|$)|^/|^[A-Za-z]:'){throw 'Unsafe MSIX path'}
  if($names.ContainsKey($name)){throw 'Duplicate MSIX entry'};$names[$name]=$true
  if($name -in @('AppxBlockMap.xml','[Content_Types].xml')){continue}
  $payloadName='payload/'+$name
  if(!$actual.ContainsKey($payloadName)){throw "Unexpected MSIX member: $name"}
  $stream=$entry.Open();try{$hash=[Convert]::ToHexString([Security.Cryptography.SHA256]::HashData($stream)).ToLowerInvariant()}finally{$stream.Dispose()}
  if($hash -cne $actual[$payloadName]){throw "MSIX member differs from checked payload: $name"}
 }
 foreach($name in $actual.Keys | Where-Object {$_.StartsWith('payload/')} ) {if(!$names.ContainsKey($name.Substring(8))){throw "MSIX missing payload: $name"}}
 if(!$names.ContainsKey('AppxBlockMap.xml') -or !$names.ContainsKey('[Content_Types].xml')){throw 'Missing MSIX package footprints'}
}finally{$archive.Dispose()}
. (Join-Path $PSScriptRoot 'Assert-StoreNoAi.ps1')
Assert-StoreNoAiPayload (Join-Path $root 'payload')
foreach($assembly in @('MAKRIVO.dll','Aivana.Windows.dll')){Assert-StoreNoAi (Join-Path $root "payload/$assembly")}
Write-Output "Store package integrity verified: $($status.msixSha256); unsigned candidate only, no certification or runtime acceptance."
