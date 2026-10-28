param(
 [Parameter(Mandatory)][string]$PublicKeyFile,
 [Parameter(Mandatory)][string]$CommerceUrl,
 [Parameter(Mandatory)][string]$OutputDirectory,
 [switch]$AllowUnsignedCandidate,
 [switch]$StoreRelease,
 [string]$PackageVersion='1.0.0',
 [string]$CertificateThumbprint,
 [string]$TimestampUrl='https://timestamp.digicert.com',
 [string]$SignTool='C:/Program Files (x86)/Windows Kits/10/bin/10.0.26100.0/x64/signtool.exe'
)
$ErrorActionPreference='Stop'
if($PackageVersion -notmatch '^(0|[1-9][0-9]{0,2})\.(0|[1-9][0-9]{0,2})\.(0|[1-9][0-9]{0,4})$') {throw 'PackageVersion must contain exactly three numeric MSI parts: major 0-255, minor 0-255, build 0-65535'}
$versionParts=$PackageVersion.Split('.')
if([int]$versionParts[0] -gt 255 -or [int]$versionParts[1] -gt 255 -or [int]$versionParts[2] -gt 65535){throw 'PackageVersion must contain exactly three numeric MSI parts: major 0-255, minor 0-255, build 0-65535'}
$repository=Split-Path -Parent $PSScriptRoot
if($StoreRelease -and (Test-Path -LiteralPath (Join-Path $repository 'src/Aivana.OnnxHost/Aivana.OnnxHost.csproj'))){throw 'Store no-AI release requires a clean release source without experimental ONNX extensions'}
$destination=[IO.Path]::GetFullPath($OutputDirectory)
if(Test-Path -LiteralPath $destination){throw 'OutputDirectory must be new; existing directories are never replaced'}
$keys=Get-Content -LiteralPath $PublicKeyFile -Raw | ConvertFrom-Json -AsHashtable
if(!$keys.Count){throw 'Retail requires a public key ring'}
foreach($id in $keys.Keys){
 if($id -notmatch '^[A-Za-z0-9._-]{1,64}$' -or $keys[$id] -match 'PRIVATE' -or $keys[$id] -notmatch '^-----BEGIN (RSA )?PUBLIC KEY-----'){throw 'Only named PEM public keys are allowed'}
 $rsa=[Security.Cryptography.RSA]::Create();try{$rsa.ImportFromPem($keys[$id]);if($rsa.KeySize -lt 3072){throw 'RSA keys must be at least 3072 bits'}}finally{$rsa.Dispose()}
}
$uri=[Uri]$CommerceUrl
if(!$uri.IsAbsoluteUri -or $uri.Scheme -ne 'https' -or $uri.UserInfo -or $uri.Query -or $uri.Fragment){throw 'Operator HTTPS commerce URL required'}
if(!$AllowUnsignedCandidate -and !$CertificateThumbprint){throw 'A real signing certificate is required; use AllowUnsignedCandidate only for candidates'}
if(!$AllowUnsignedCandidate -and ([Uri]$TimestampUrl).Scheme -ne 'https'){throw 'HTTPS trusted timestamp endpoint required'}
New-Item -ItemType Directory -Path $destination | Out-Null
$app=Join-Path $destination 'app'
$buildArtifacts=Join-Path ([IO.Path]::GetTempPath()) ('makrivo-retail-build-'+[Guid]::NewGuid().ToString('N'))
$projects=@{'Aivana.Desktop'='';'Aivana.AutomationHost'='';'Aivana.MobileHost'='';'Aivana.OnnxHost'='onnx';'Aivana.PluginHost'='example-plugin'}
$experimentalOnnxIncluded=Test-Path -LiteralPath (Join-Path $repository 'src/Aivana.OnnxHost/Aivana.OnnxHost.csproj') -PathType Leaf
if(!$experimentalOnnxIncluded){$projects.Remove('Aivana.OnnxHost')}
foreach($project in $projects.Keys){
 $output=Join-Path $app $projects[$project]
 dotnet publish (Join-Path $repository "src/$project/$project.csproj") "-p:StoreRelease=$($StoreRelease.IsPresent.ToString().ToLowerInvariant())" -c Release -r win-x64 --self-contained true --artifacts-path $buildArtifacts -o $output '-p:MakrivoRetailBuild=true' "-p:MakrivoLicensePublicKeyFile=$([IO.Path]::GetFullPath($PublicKeyFile))" "-p:MakrivoCommerceUrl=$CommerceUrl"
 if($LASTEXITCODE -ne 0){throw "Retail publish failed: $project; partial candidate retained at $destination"}
}
$legal=New-Item -ItemType Directory -Path (Join-Path $destination 'legal')
foreach($name in @('product.json','EULA-review-draft.md','privacy-data-flows.md','acceptance.json')){Copy-Item -LiteralPath (Join-Path $repository "release/$name") -Destination $legal.FullName}
# Resolve every published dependency and every nested self-contained runtime, with exact versions.
$dependencies=@{}
foreach($deps in Get-ChildItem -LiteralPath $app -Filter '*.deps.json' -Recurse -Force){
 $data=Get-Content -LiteralPath $deps.FullName -Raw | ConvertFrom-Json -AsHashtable
 foreach($name in $data.libraries.Keys){if($data.libraries[$name].type -eq 'package'){$dependencies[$name]=$true}}
}
foreach($config in Get-ChildItem -LiteralPath $app -Filter '*.runtimeconfig.json' -Recurse -Force){
 $data=Get-Content -LiteralPath $config.FullName -Raw | ConvertFrom-Json -AsHashtable
 foreach($framework in $data.runtimeOptions.includedFrameworks){$dependencies["$($framework.name.ToLowerInvariant()).runtime.win-x64/$($framework.version)"]=$true}
}
$cache=if($env:NUGET_PACKAGES){$env:NUGET_PACKAGES}else{Join-Path ([Environment]::GetFolderPath('UserProfile')) '.nuget/packages'}
$notices=@()
foreach($dependency in ($dependencies.Keys | Sort-Object)){
 $parts=$dependency.Split('/');$source=Join-Path $cache $dependency.ToLowerInvariant()
 if(!(Test-Path -LiteralPath $source)){throw "Dependency cache unavailable: $dependency"}
 $files=@(Get-ChildItem -LiteralPath $source -File -Recurse -Force | Where-Object {$_.Name -match '(?i)^(LICENSE|LICENCE|COPYING|NOTICE|THIRD.?PARTY)'})
 $folder=Join-Path $destination ('licenses/'+$dependency.Replace('/','-'));New-Item -ItemType Directory -Path $folder -Force | Out-Null
 $relativeFiles=@();$index=0;$licenseSource=$null
 $nuspec=Get-ChildItem -LiteralPath $source -Filter '*.nuspec' -File -Force | Select-Object -First 1
 if(!$files.Count){
  if(!$nuspec){throw "No license or package metadata: $dependency"}
  [xml]$metadata=Get-Content -LiteralPath $nuspec.FullName -Raw
  $license=$metadata.SelectSingleNode('//*[local-name()="metadata"]/*[local-name()="license"]')
  if(!$license -or $license.type -ne 'expression' -or $license.InnerText -notmatch '^(MIT|Apache-2\.0|BSD-2-Clause|BSD-3-Clause|ISC)$'){throw "No actual license text for $dependency; operator must supply/review complex license"}
  $licenseSource='https://licenses.nuget.org/'+$license.InnerText
  $response=Invoke-WebRequest -Uri $licenseSource -TimeoutSec 30 -MaximumRedirection 0
  if($response.StatusCode -ne 200 -or $response.Content.Length -lt 100 -or $response.Content.Length -gt 1048576){throw "Invalid canonical license response: $dependency"}
  $target=Join-Path $folder 'LICENSE.html';[IO.File]::WriteAllText($target,$response.Content)
  $relativeFiles += [IO.Path]::GetRelativePath($destination,$target).Replace('\','/')
 }
 if($nuspec){$target=Join-Path $folder 'package.nuspec';Copy-Item -LiteralPath $nuspec.FullName -Destination $target;$relativeFiles += [IO.Path]::GetRelativePath($destination,$target).Replace('\','/')}
 foreach($file in $files){$index++;$target=Join-Path $folder "$index-$($file.Name)";Copy-Item -LiteralPath $file.FullName -Destination $target;$relativeFiles += [IO.Path]::GetRelativePath($destination,$target).Replace('\','/')}
 $notices += @{id=$parts[0];version=$parts[1];files=$relativeFiles;licenseSource=$licenseSource}
}
ConvertTo-Json -InputObject $notices -Depth 8 | Set-Content -LiteralPath (Join-Path $destination 'third-party-notices.json') -Encoding utf8
function SignFile($path){
 & $SignTool sign /sha1 $CertificateThumbprint /fd SHA256 /tr $TimestampUrl /td SHA256 $path
 if($LASTEXITCODE -ne 0){throw "Signing failed: $path"}
 & $SignTool verify /pa /all $path
 if($LASTEXITCODE -ne 0){throw "Signature validation failed: $path"}
}
if(!$AllowUnsignedCandidate){foreach($file in Get-ChildItem -LiteralPath $app -Recurse -File -Force | Where-Object {$_.Name -match '^(MAKRIVO|Aivana\.).*\.(exe|dll)$'}){SignFile $file.FullName}}
$example=Join-Path $app 'example-plugin'
$pluginHashes=@{}
foreach($file in Get-ChildItem -LiteralPath $example -Recurse -File -Force){$pluginHashes[[IO.Path]::GetRelativePath($example,$file.FullName).Replace('\','/')]=(Get-FileHash -LiteralPath $file.FullName -Algorithm SHA256).Hash}
@{Id='makrivo.example';Version='1.0.0';EntryPoint='Aivana.PluginHost.exe';ProtocolVersion=1;Capabilities=@('local-state','countdown');Hashes=$pluginHashes;Dependencies=@();Actions=@(
 @{Id='example.countdown';Name='Countdown';ParameterSchema='{"type":"object","properties":{"seconds":{"type":"integer","minimum":0,"maximum":30}},"required":["seconds"],"additionalProperties":false}'},
 @{Id='example.state';Name='Status';ParameterSchema='{"type":"object","additionalProperties":false}'}
)} | ConvertTo-Json -Depth 10 | Set-Content -LiteralPath (Join-Path $example 'manifest.json')
Compress-Archive -Path (Join-Path $example '*') -DestinationPath (Join-Path $app 'makrivo.example.zip')
$payload=Join-Path $destination 'Payload.wxs'
$components=[Text.StringBuilder]::new()
[void]$components.AppendLine('<Wix xmlns="http://wixtoolset.org/schemas/v4/wxs"><Fragment><ComponentGroup Id="Payload" Directory="INSTALLFOLDER">')
foreach($file in Get-ChildItem -LiteralPath $destination -File -Recurse -Force){
 $rel=[IO.Path]::GetRelativePath($destination,$file.FullName).Replace('\','/')
 $escaped=[Security.SecurityElement]::Escape($file.FullName)
 if($rel.StartsWith('app/')){$rel=$rel.Substring(4)}
 $sub=[IO.Path]::GetDirectoryName($rel).Replace('\','/')
 $subAttribute=if($sub){' Subdirectory="'+[Security.SecurityElement]::Escape($sub)+'"'}else{''}
 [void]$components.AppendLine("<Component$subAttribute Guid=`"*`"><File Source=`"$escaped`" /></Component>")
}
[void]$components.AppendLine('</ComponentGroup></Fragment></Wix>');$components.ToString() | Set-Content -LiteralPath $payload
wix build (Join-Path $repository 'installer/Product.wxs') $payload -arch x64 -pdbtype none -d "PackageVersion=$PackageVersion" -o (Join-Path $destination 'MAKRIVO.msi')
if($LASTEXITCODE -ne 0){throw 'WiX installer build failed; candidate retained'}
# Build sources contain absolute local paths and are not distribution content.
Remove-Item -LiteralPath $payload
if(!$AllowUnsignedCandidate){SignFile (Join-Path $destination 'MAKRIVO.msi')}
@{releaseApproved=$false;unsignedCandidate=[bool]$AllowUnsignedCandidate;packageVersion=$PackageVersion;experimentalOnnxIncluded=[bool]$experimentalOnnxIncluded;storeRelease=[bool]$StoreRelease;aiAssistantIncluded=(!$StoreRelease);buildPurpose=if($AllowUnsignedCandidate){'TEST_ONLY'}else{'SIGNED_PENDING_ACCEPTANCE'};commerceUrl=$CommerceUrl;publicKeyIds=@($keys.Keys);legalDocuments='draft-review-required';createdAt=[DateTime]::UtcNow.ToString('o')} | ConvertTo-Json | Set-Content -LiteralPath (Join-Path $destination 'release-status.json')
$inventory=@{}
foreach($file in Get-ChildItem -LiteralPath $destination -File -Recurse -Force){$relative=[IO.Path]::GetRelativePath($destination,$file.FullName).Replace('\','/');$inventory[$relative]=(Get-FileHash -LiteralPath $file.FullName -Algorithm SHA256).Hash.ToLowerInvariant()}
$digestNames=[string[]]@($inventory.Keys | Where-Object {$_ -notin @('inventory.json','release-status.json','legal/acceptance.json')})
[Array]::Sort($digestNames,[StringComparer]::Ordinal)
$canonical=($digestNames | ForEach-Object {"$_ $($inventory[$_])`n"}) -join ''
$buildDigest=[Convert]::ToHexString([Security.Cryptography.SHA256]::HashData([Text.Encoding]::UTF8.GetBytes($canonical))).ToLowerInvariant()
$statusFile=Join-Path $destination 'release-status.json'
$status=Get-Content -LiteralPath $statusFile -Raw | ConvertFrom-Json -AsHashtable
$status.packageBuildSha256=$buildDigest
$status | ConvertTo-Json | Set-Content -LiteralPath $statusFile
$inventory['release-status.json']=(Get-FileHash -LiteralPath $statusFile -Algorithm SHA256).Hash.ToLowerInvariant()
@{schemaVersion=1;packageBuildSha256=$buildDigest;files=$inventory} | ConvertTo-Json -Depth 5 | Set-Content -LiteralPath (Join-Path $destination 'inventory.json')
& (Join-Path $PSScriptRoot 'check-retail-package.ps1') -PackagePath $destination -AllowUnsignedCandidate:$AllowUnsignedCandidate
Write-Output "Candidate retained at $destination; releaseApproved=false. Production acceptance requires independent review."
