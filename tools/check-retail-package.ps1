param([Parameter(Mandatory)][string]$PackagePath,[switch]$AllowUnsignedCandidate)
$ErrorActionPreference='Stop'
$root=[IO.Path]::GetFullPath($PackagePath)
function ReadJson($name) { Get-Content -LiteralPath (Join-Path $root $name) -Raw | ConvertFrom-Json -AsHashtable }
foreach($name in @('app/MAKRIVO.exe','app/coreclr.dll','app/hostfxr.dll','app/hostpolicy.dll','app/example-plugin/coreclr.dll','app/Aivana.AutomationHost.exe','app/Aivana.MobileHost.exe','app/example-plugin/Aivana.PluginHost.exe','app/wwwroot/panel.js','legal/product.json','legal/EULA-review-draft.md','legal/privacy-data-flows.md','third-party-notices.json','release-status.json','inventory.json')) { if(!(Test-Path -LiteralPath (Join-Path $root $name) -PathType Leaf)){throw "Missing required file: $name"} }
$status=ReadJson 'release-status.json'
if($status.experimentalOnnxIncluded -isnot [bool]){throw 'Explicit experimentalOnnxIncluded boolean required; no AI performance acceptance implied'}
if($status.experimentalOnnxIncluded){
 foreach($name in @('app/onnx/Aivana.OnnxHost.exe','app/onnx/coreclr.dll')){if(!(Test-Path -LiteralPath (Join-Path $root $name) -PathType Leaf)){throw "Experimental ONNX host incomplete: $name"}}
}else{
 if(Test-Path -LiteralPath (Join-Path $root 'app/onnx')){throw 'Experimental ONNX payload contradicts status'}
}
$product=ReadJson 'legal/product.json'
if($product.product -cne 'MAKRIVO' -or $product.priceMinor -ne 2900 -or $product.currency -cne 'EUR' -or $product.taxBehavior -cne 'inclusive' -or $product.majorVersion -ne 1 -or $product.maxDevices -ne 4 -or $product.billing -cne 'one_time' -or $product.perpetual -ne $true){throw 'Invalid retail commercial terms'}
$manifest=ReadJson 'inventory.json'
$inventory=$manifest.files
if($manifest.schemaVersion -ne 1 -or !$inventory){throw 'Invalid inventory schema'}
$actual=@{}
foreach($file in Get-ChildItem -LiteralPath $root -File -Recurse -Force){
 if($file.Attributes -band [IO.FileAttributes]::ReparsePoint){throw 'Links are forbidden'}
 $relative=[IO.Path]::GetRelativePath($root,$file.FullName).Replace('\','/')
 if($relative -eq 'inventory.json'){continue}
 if($file.Name -match '(?i)(Logitech|LogiLed|RzChroma|Razer|Corsair|i?CUESDK|SteelSeries).*\.dll$'){throw "Proprietary SDK forbidden: $relative"}
 if($file.Extension -match '^\.(pfx|p12|key)$' -or $file.Name -match '^\.env($|\.)'){throw "Secret file forbidden: $relative"}
 $bytes=[IO.File]::ReadAllBytes($file.FullName);$content=[Text.Encoding]::UTF8.GetString($bytes)
 if($content -match '-----BEGIN (RSA |EC |ENCRYPTED |OPENSSH )?PRIVATE KEY-----|sk_(live|test)_[A-Za-z0-9]{12,}|whsec_[A-Za-z0-9]{12,}'){throw "Private material forbidden: $relative"}
 if($file.Extension -eq '.zip'){
  $archive=[IO.Compression.ZipFile]::OpenRead($file.FullName)
  try{
   if($archive.Entries.Count -gt 4096){throw 'Archive entry limit exceeded'}
   [long]$expanded=0
   foreach($entry in $archive.Entries){
    $expanded += $entry.Length
    if($expanded -gt 2147483648 -or $entry.Length -gt 536870912){throw 'Archive expansion limit exceeded'}
    if($entry.FullName -match '(^|[/\\])\.\.([/\\]|$)|^[/\\]|^[A-Za-z]:' -or $entry.Name -match '(?i)(Logitech|LogiLed|RzChroma|Razer|Corsair|i?CUESDK|SteelSeries).*\.dll$|\.(pfx|p12|key|zip)$|^\.env($|\.)'){throw "Forbidden archive member: $($entry.FullName)"}
    $stream=$entry.Open();$memory=[IO.MemoryStream]::new()
    try{$stream.CopyTo($memory);$entryText=[Text.Encoding]::UTF8.GetString($memory.ToArray());if($entryText -match '-----BEGIN (RSA |EC |ENCRYPTED |OPENSSH )?PRIVATE KEY-----|sk_(live|test)_[A-Za-z0-9]{12,}|whsec_[A-Za-z0-9]{12,}'){throw 'Private material in archive'}}finally{$stream.Dispose();$memory.Dispose()}
   }
  }finally{$archive.Dispose()}
 }
 $actual[$relative]=(Get-FileHash -LiteralPath $file.FullName -Algorithm SHA256).Hash.ToLowerInvariant()
}
foreach($directory in Get-ChildItem -LiteralPath $root -Directory -Recurse -Force){if($directory.Attributes -band [IO.FileAttributes]::ReparsePoint){throw 'Directory links are forbidden'}}
if($actual.Count -ne $inventory.Count){throw 'Inventory file count differs'}
foreach($name in $actual.Keys){if(!$inventory.ContainsKey($name) -or $inventory[$name] -cne $actual[$name]){throw "Inventory mismatch: $name"}}
$notices=ReadJson 'third-party-notices.json'
if(!$notices.Count){throw 'Dependency notices absent'}
foreach($notice in $notices){if(!$notice.id -or !$notice.version -or !$notice.files.Count){throw 'Incomplete dependency notice'};foreach($name in $notice.files){if(!$actual.ContainsKey($name) -or !(Get-Item -LiteralPath (Join-Path $root $name)).Length){throw "Missing dependency license: $name"}}}
$covered=@{}
foreach($notice in $notices){$key="$($notice.id)/$($notice.version)";if($covered.ContainsKey($key)){throw "Duplicate dependency: $key"};$covered[$key]=$true}
$requiredDependencies=@{}
foreach($deps in Get-ChildItem -LiteralPath (Join-Path $root 'app') -Filter '*.deps.json' -Recurse -Force){
 $data=Get-Content -LiteralPath $deps.FullName -Raw | ConvertFrom-Json -AsHashtable
 foreach($key in $data.libraries.Keys){if($data.libraries[$key].type -eq 'package'){$requiredDependencies[$key]=$true}}
}
foreach($config in Get-ChildItem -LiteralPath (Join-Path $root 'app') -Filter '*.runtimeconfig.json' -Recurse -Force){
 $data=Get-Content -LiteralPath $config.FullName -Raw | ConvertFrom-Json -AsHashtable
 foreach($framework in $data.runtimeOptions.includedFrameworks){$requiredDependencies["$($framework.name.ToLowerInvariant()).runtime.win-x64/$($framework.version)"]=$true}
}
foreach($key in $requiredDependencies.Keys){if(!$covered.ContainsKey($key)){throw "Dependency missing from notices: $key"}}
$digestNames=[string[]]@($actual.Keys | Where-Object {$_ -notin @('inventory.json','release-status.json','legal/acceptance.json')})
[Array]::Sort($digestNames,[StringComparer]::Ordinal)
$canonical=($digestNames | ForEach-Object {"$_ $($actual[$_])`n"}) -join ''
$buildDigest=[Convert]::ToHexString([Security.Cryptography.SHA256]::HashData([Text.Encoding]::UTF8.GetBytes($canonical))).ToLowerInvariant()
if($manifest.packageBuildSha256 -cne $buildDigest){throw 'Package build digest mismatch'}
$status=ReadJson 'release-status.json'
if($status.packageBuildSha256 -cne $buildDigest){throw 'Release status build digest mismatch'}
if($AllowUnsignedCandidate){if($status.releaseApproved -ne $false){throw 'Candidate cannot claim release approval'}}
else {
 if($status.unsignedCandidate -ne $false){throw 'Unsigned candidate cannot be released'}
 $acceptance=ReadJson 'legal/acceptance.json'
 if($acceptance.legalAcceptance.approved -ne $true -or !$acceptance.legalAcceptance.evidence){throw 'Legal acceptance missing'}
 foreach($case in @('hardware','lan','dpi','endurance','install','upgrade','uninstall')){
  $item=$acceptance.physicalMatrix[$case]
  if($item.status -ne 'passed' -or !$item.evidence -or !$item.buildSha256 -or !$item.executedAt -or $item.testOnly -ne $false -or $item.buildSha256.ToLowerInvariant() -cne $buildDigest){throw "Actual acceptance evidence missing: $case"}
 }
 if(!(Test-Path -LiteralPath (Join-Path $root 'MAKRIVO.msi'))){throw 'Signed installer missing'}
 $signedPaths=@(Get-ChildItem -LiteralPath (Join-Path $root 'app') -Recurse -File -Force | Where-Object {$_.Name -match '^(MAKRIVO|Aivana\.).*\.(exe|dll)$'} | ForEach-Object {[IO.Path]::GetRelativePath($root,$_.FullName)}) + @('MAKRIVO.msi')
 foreach($name in $signedPaths){
  $sig=Get-AuthenticodeSignature -LiteralPath (Join-Path $root $name)
  if($sig.Status -ne 'Valid' -or !$sig.TimeStamperCertificate){throw "Trusted timestamped signature required: $name"}
 }
}
Write-Output "Retail package integrity verified: packageBuildSha256=$buildDigest; this does not approve legal or physical acceptance."
