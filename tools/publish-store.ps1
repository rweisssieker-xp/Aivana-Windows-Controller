param(
 [Parameter(Mandatory)][string]$RetailPackagePath,
 [Parameter(Mandatory)][string]$OutputDirectory,
 [Parameter(Mandatory)][string]$PackageName,
 [Parameter(Mandatory)][string]$Publisher,
 [Parameter(Mandatory)][AllowEmptyString()][string]$PublisherDisplayName,
 [string]$PackageVersion='1.0.0.0',
 [switch]$AllowTestIdentity,
 [string]$MakeAppx='C:/Program Files (x86)/Windows Kits/10/bin/10.0.26100.0/x64/makeappx.exe'
)
$ErrorActionPreference='Stop'
if($PackageVersion -notmatch '^(0|[1-9][0-9]{0,4})\.(0|[1-9][0-9]{0,4})\.(0|[1-9][0-9]{0,4})\.0$' -or $PackageVersion -eq '0.0.0.0' -or @($PackageVersion.Split('.') | Where-Object {[int]$_ -gt 65535}).Count){throw 'PackageVersion must be four numeric parts (0-65535), greater than zero, with the final part zero for Store delivery'}
if($PackageName -notmatch '^[A-Za-z0-9][A-Za-z0-9.-]{2,49}$' -or !$PublisherDisplayName.Trim() -or $PublisherDisplayName.Length -gt 256){throw 'Invalid Store identity name/display name'}
try{[void][Security.Cryptography.X509Certificates.X500DistinguishedName]::new($Publisher)}catch{throw 'Invalid Store publisher identity distinguished name'}
if($Publisher -notmatch '^CN=' -or $Publisher -match '[\r\n]'){throw 'Invalid Store publisher identity; copy the exact CN from Partner Center'}
$isTestIdentity=$PackageName -eq 'MAKRIVO.TestOnly' -or $Publisher -eq 'CN=MAKRIVO Test Only'
if($AllowTestIdentity){
 if($PackageName -cne 'MAKRIVO.TestOnly' -or $Publisher -cne 'CN=MAKRIVO Test Only'){throw 'Test identity switch requires MAKRIVO.TestOnly / CN=MAKRIVO Test Only'}
}elseif($isTestIdentity){throw 'Test identity requires explicit AllowTestIdentity; it cannot be submitted'}
$source=[IO.Path]::GetFullPath($RetailPackagePath).TrimEnd([IO.Path]::DirectorySeparatorChar)
$destination=[IO.Path]::GetFullPath($OutputDirectory).TrimEnd([IO.Path]::DirectorySeparatorChar)
if(Test-Path -LiteralPath $destination){throw 'OutputDirectory must be new; existing files are preserved'}
if($destination.Equals($source,[StringComparison]::OrdinalIgnoreCase) -or $destination.StartsWith($source+[IO.Path]::DirectorySeparatorChar,[StringComparison]::OrdinalIgnoreCase) -or $source.StartsWith($destination+[IO.Path]::DirectorySeparatorChar,[StringComparison]::OrdinalIgnoreCase)){throw 'Input and output paths must not overlap'}
foreach($path in @($source,[IO.Path]::GetDirectoryName($destination))){
 $current=$path
 while($current){if(Test-Path -LiteralPath $current){if((Get-Item -LiteralPath $current -Force).Attributes -band [IO.FileAttributes]::ReparsePoint){throw 'Source/output ancestor links are forbidden'}};$current=[IO.Path]::GetDirectoryName($current)}
}
if(!(Test-Path -LiteralPath $MakeAppx -PathType Leaf)){throw 'Windows SDK MakeAppx unavailable'}
# Integrity only: this does not convert pending retail acceptance into Store approval.
$sourceInventoryText=Get-Content -LiteralPath (Join-Path $source 'inventory.json') -Raw
& (Join-Path $PSScriptRoot 'check-retail-package.ps1') -PackagePath $source -AllowUnsignedCandidate
if((Get-Content -LiteralPath (Join-Path $source 'inventory.json') -Raw) -cne $sourceInventoryText){throw 'Retail inventory changed during validation'}
$sourceManifest=$sourceInventoryText | ConvertFrom-Json -AsHashtable
$retailStatus=Get-Content -LiteralPath (Join-Path $source 'release-status.json') -Raw | ConvertFrom-Json -AsHashtable
if($retailStatus.experimentalOnnxIncluded -isnot [bool] -or $retailStatus.experimentalOnnxIncluded -ne $false){throw 'Store no-AI release forbids experimental ONNX payload'}
if($retailStatus.storeRelease -isnot [bool] -or $retailStatus.storeRelease -ne $true -or $retailStatus.aiAssistantIncluded -isnot [bool] -or $retailStatus.aiAssistantIncluded -ne $false){throw 'Store no-AI release requires a retail source built with -StoreRelease'}
. (Join-Path $PSScriptRoot 'Assert-StoreNoAi.ps1')
Assert-StoreNoAiPayload (Join-Path $source 'app')
foreach($assembly in @('MAKRIVO.dll','Aivana.Windows.dll')){Assert-StoreNoAi (Join-Path $source "app/$assembly")}
if(!$retailStatus.commerceUrl -or ![Uri]::IsWellFormedUriString($retailStatus.commerceUrl,[UriKind]::Absolute) -or ([Uri]$retailStatus.commerceUrl).Scheme -ne 'https'){throw 'Retail source must record its embedded HTTPS commerce URL'}
# Read metadata only. Never execute an assembly supplied as packaging input.
$stream=[IO.File]::OpenRead((Join-Path $source 'app/MAKRIVO.dll'))
try{
 $pe=[Reflection.PortableExecutable.PEReader]::new($stream)
 try{
  $metadata=[Reflection.Metadata.PEReaderExtensions]::GetMetadataReader($pe)
  $ring=$null
  foreach($handle in $metadata.ManifestResources){
   $resource=$metadata.GetManifestResource($handle)
   if($metadata.GetString($resource.Name) -ceq 'MAKRIVO.Retail.PublicKeys.json'){
    if(!$resource.Implementation.IsNil){throw 'Retail public keys must be embedded'}
    $reader=$pe.GetSectionData($pe.PEHeaders.CorHeader.ResourcesDirectory.RelativeVirtualAddress).GetReader()
    $reader.Offset=[int]$resource.Offset
    $length=$reader.ReadInt32()
    if($length -lt 1 -or $length -gt 65536){throw 'Invalid embedded retail key ring size'}
    $ring=[Text.Encoding]::UTF8.GetString($reader.ReadBytes($length)) | ConvertFrom-Json -AsHashtable
   }
  }
  if(!$ring -or !$ring.Count){throw 'A retail build with embedded public keys is required; development builds cannot be packaged'}
  foreach($key in $ring.Keys){
   if($key -notmatch '^[A-Za-z0-9._-]{1,64}$' -or $ring[$key] -match 'PRIVATE' -or $ring[$key] -notmatch '^-----BEGIN (RSA )?PUBLIC KEY-----'){throw 'Invalid embedded public-only key ring'}
   $rsa=[Security.Cryptography.RSA]::Create();try{$rsa.ImportFromPem($ring[$key]);if($rsa.KeySize -lt 3072){throw 'Weak embedded public key'}}finally{$rsa.Dispose()}
  }
 }finally{$pe.Dispose()}
}finally{$stream.Dispose()}
if(![Text.Encoding]::UTF8.GetString([IO.File]::ReadAllBytes((Join-Path $source 'app/MAKRIVO.dll'))).Contains($retailStatus.commerceUrl)){throw 'Retail status commerce URL differs from compiled assembly metadata'}
foreach($reserved in @('AppxManifest.xml','StoreAssets','legal','licenses','third-party-notices.json')){if(Test-Path -LiteralPath (Join-Path (Join-Path $source 'app') $reserved)){throw "Retail app collides with reserved Store package path: $reserved"}}
New-Item -ItemType Directory -Path $destination | Out-Null
$payload=Join-Path $destination 'payload'
New-Item -ItemType Directory -Path $payload | Out-Null
Get-ChildItem -LiteralPath (Join-Path $source 'app') -Force | Copy-Item -Destination $payload -Recurse -Force
foreach($name in @('legal','licenses','third-party-notices.json')){Copy-Item -LiteralPath (Join-Path $source $name) -Destination $payload -Recurse -Force}
# Bind every copied byte to the already validated source snapshot, even if the input
# directory changes between validation and copying.
$expectedCopied=@{}
foreach($name in $sourceManifest.files.Keys){
 if($name.StartsWith('app/')){$expectedCopied[$name.Substring(4)]=$sourceManifest.files[$name]}
 elseif($name.StartsWith('legal/') -or $name.StartsWith('licenses/') -or $name -ceq 'third-party-notices.json'){$expectedCopied[$name]=$sourceManifest.files[$name]}
}
$copiedFiles=@(Get-ChildItem -LiteralPath $payload -File -Recurse -Force)
foreach($directory in Get-ChildItem -LiteralPath $payload -Directory -Recurse -Force){if($directory.Attributes -band [IO.FileAttributes]::ReparsePoint){throw 'Copied payload directory links forbidden'}}
if($copiedFiles.Count -ne $expectedCopied.Count){throw 'Copied payload count differs from validated retail source'}
foreach($file in $copiedFiles){
 $relative=[IO.Path]::GetRelativePath($payload,$file.FullName).Replace('\','/')
 if(($file.Attributes -band [IO.FileAttributes]::ReparsePoint) -or !$expectedCopied.ContainsKey($relative) -or (Get-FileHash -LiteralPath $file.FullName -Algorithm SHA256).Hash.ToLowerInvariant() -cne $expectedCopied[$relative]){throw "Copied payload differs from validated retail source: $relative"}
}
$assets=Join-Path $payload 'StoreAssets';New-Item -ItemType Directory -Path $assets | Out-Null
Add-Type -AssemblyName System.Drawing
$logo=[Drawing.Image]::FromFile((Join-Path (Split-Path -Parent $PSScriptRoot) 'src/Aivana.Desktop/Assets/makrivo.png'))
try{
 foreach($size in @(44,50,150)){
  $bitmap=[Drawing.Bitmap]::new($size,$size);$graphics=[Drawing.Graphics]::FromImage($bitmap)
  try{$graphics.Clear([Drawing.Color]::Transparent);$graphics.InterpolationMode=[Drawing.Drawing2D.InterpolationMode]::HighQualityBicubic;$graphics.DrawImage($logo,0,0,$size,$size);$bitmap.Save((Join-Path $assets "Logo$size.png"),[Drawing.Imaging.ImageFormat]::Png)}finally{$graphics.Dispose();$bitmap.Dispose()}
 }
}finally{$logo.Dispose()}
function XmlEscape([string]$value){[Security.SecurityElement]::Escape($value)}
$manifest=@"
<?xml version="1.0" encoding="utf-8"?>
<Package xmlns="http://schemas.microsoft.com/appx/manifest/foundation/windows10" xmlns:uap="http://schemas.microsoft.com/appx/manifest/uap/windows10" xmlns:rescap="http://schemas.microsoft.com/appx/manifest/foundation/windows10/restrictedcapabilities" IgnorableNamespaces="uap rescap">
 <Identity Name="$(XmlEscape $PackageName)" Publisher="$(XmlEscape $Publisher)" Version="$PackageVersion" ProcessorArchitecture="x64" />
 <Properties><DisplayName>MAKRIVO</DisplayName><PublisherDisplayName>$(XmlEscape $PublisherDisplayName)</PublisherDisplayName><Logo>StoreAssets\Logo50.png</Logo></Properties>
 <Dependencies><TargetDeviceFamily Name="Windows.Desktop" MinVersion="10.0.19041.0" MaxVersionTested="10.0.26100.0" /></Dependencies>
 <Resources><Resource Language="en-us" /><Resource Language="de-de" /></Resources>
 <Applications><Application Id="MAKRIVO" Executable="MAKRIVO.exe" EntryPoint="Windows.FullTrustApplication"><uap:VisualElements DisplayName="MAKRIVO" Description="Windows hotkeys and workflows. Separate paid license required." BackgroundColor="transparent" Square150x150Logo="StoreAssets\Logo150.png" Square44x44Logo="StoreAssets\Logo44.png" /></Application></Applications>
 <Capabilities><Capability Name="internetClient" /><Capability Name="privateNetworkClientServer" /><rescap:Capability Name="runFullTrust" /></Capabilities>
</Package>
"@
[IO.File]::WriteAllText((Join-Path $payload 'AppxManifest.xml'),$manifest,[Text.UTF8Encoding]::new($false))
$msix=Join-Path $destination 'MAKRIVO.msix'
& $MakeAppx pack /d $payload /p $msix /h SHA256 2>&1 | Set-Content -LiteralPath (Join-Path $destination 'makeappx.log') -Encoding utf8
if($LASTEXITCODE -ne 0){throw 'MakeAppx failed; partial candidate retained'}
$sourceIsTest=$retailStatus.buildPurpose -eq 'TEST_ONLY' -or $retailStatus.commerceUrl -match '(?i)\.invalid([/:]|$)' -or @($ring.Keys | Where-Object {$_ -match '(?i)test|candidate'}).Count -gt 0
$status=@{
 schemaVersion=1;storeRelease=$true;aiAssistantIncluded=$false;experimentalOnnxIncluded=$false;releaseApproved=$false;submissionReady=$false;unsigned=$true;testIdentity=[bool]$AllowTestIdentity;testRetailSource=[bool]$sourceIsTest
 packageName=$PackageName;publisher=$Publisher;packageVersion=$PackageVersion;architecture='x64';licenseModel='external_one_time_four_own_pcs';priceMinor=2900;currency='EUR'
 commerceUrl=$retailStatus.commerceUrl;publicKeyIds=@($ring.Keys);sourcePackageBuildSha256=$sourceManifest.packageBuildSha256
 msixSha256=(Get-FileHash -LiteralPath $msix -Algorithm SHA256).Hash.ToLowerInvariant();createdAt=[DateTime]::UtcNow.ToString('o')
 acceptance=@{packagedInstall='pending';packagedUpgrade='pending';packagedUninstall='pending';dataMigration='pending';hardwareAndHelpers='pending';storeCertification='pending'}
}
$status | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath (Join-Path $destination 'store-status.json') -Encoding utf8
$inventory=@{}
foreach($file in Get-ChildItem -LiteralPath $destination -File -Recurse -Force){$inventory[[IO.Path]::GetRelativePath($destination,$file.FullName).Replace('\','/')]=(Get-FileHash -LiteralPath $file.FullName -Algorithm SHA256).Hash.ToLowerInvariant()}
@{schemaVersion=1;files=$inventory} | ConvertTo-Json -Depth 5 | Set-Content -LiteralPath (Join-Path $destination 'store-inventory.json') -Encoding utf8
& (Join-Path $PSScriptRoot 'check-store-package.ps1') -PackagePath $destination
Write-Output "Unsigned Store candidate retained: $msix; submissionReady=false. No installation or upload performed."
