$ErrorActionPreference='Stop'
$root=Split-Path (Split-Path $PSScriptRoot -Parent) -Parent
$validator=Join-Path $root 'build/ValidateRetailConfiguration.ps1'
$directory=Join-Path ([IO.Path]::GetTempPath()) ('makrivo-build-check-'+[Guid]::NewGuid().ToString('N'))
[IO.Directory]::CreateDirectory($directory) | Out-Null
$rsa=[Security.Cryptography.RSA]::Create(3072)
try {
 $public=$rsa.ExportSubjectPublicKeyInfoPem()
 $path=Join-Path $directory 'keys.json'
 foreach ($case in @(
  @{Name='valid';Json=(@{test=$public}|ConvertTo-Json -Compress);Url='https://example.com';Success=$true},
  @{Name='private';Json=(@{test=$rsa.ExportRSAPrivateKeyPem()}|ConvertTo-Json -Compress);Url='https://example.com';Success=$false},
  @{Name='empty';Json='{}';Url='https://example.com';Success=$false},
  @{Name='malformed';Json='[';Url='https://example.com';Success=$false},
  @{Name='duplicate';Json=('{"test":'+($public|ConvertTo-Json -Compress)+',"test":'+($public|ConvertTo-Json -Compress)+'}');Url='https://example.com';Success=$false},
  @{Name='insecure-commerce';Json=(@{test=$public}|ConvertTo-Json -Compress);Url='http://example.com';Success=$false}
 )) {
  [IO.File]::WriteAllText($path,$case.Json)
  $output=& pwsh -NoProfile -NonInteractive -File $validator -KeyFile $path -CommerceUrl $case.Url 2>&1
  if (($LASTEXITCODE -eq 0) -ne $case.Success) { throw ('Unexpected result for '+$case.Name+': '+($output -join ' ')) }
  Write-Host ('PASS '+$case.Name)
 }
} finally { $rsa.Dispose(); [IO.Directory]::Delete($directory,$true) }
