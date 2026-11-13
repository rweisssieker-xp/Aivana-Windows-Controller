param([string]$OutputDirectory, [switch]$SelfContained, [switch]$StoreRelease)
$ErrorActionPreference = 'Stop'
$repository = Split-Path -Parent $PSScriptRoot
if (-not $OutputDirectory) { $OutputDirectory = Join-Path $repository ('artifacts/release-' + (Get-Date -Format 'yyyyMMdd-HHmmss')) }
$destination = [IO.Path]::GetFullPath($OutputDirectory)
New-Item -ItemType Directory -Path $destination -Force | Out-Null
$runtimeOption = if ($SelfContained) { 'true' } else { 'false' }
foreach ($project in @('Aivana.AutomationHost','Aivana.MobileHost','Aivana.Desktop')) {
    dotnet publish (Join-Path $repository "src/$project/$project.csproj") "-p:StoreRelease=$($StoreRelease.IsPresent.ToString().ToLowerInvariant())" -c Release -r win-x64 --self-contained $runtimeOption -o $destination
    if ($LASTEXITCODE -ne 0) { throw "Publish failed: $project" }
}
$example = Join-Path $destination 'example-plugin'
dotnet publish (Join-Path $repository 'src/Aivana.PluginHost/Aivana.PluginHost.csproj') "-p:StoreRelease=$($StoreRelease.IsPresent.ToString().ToLowerInvariant())" -c Release -r win-x64 --self-contained $runtimeOption -o $example
if ($LASTEXITCODE -ne 0) { throw 'Example publish failed' }
$hashes = @{}
Get-ChildItem -LiteralPath $example -File | ForEach-Object { $hashes[$_.Name] = (Get-FileHash -LiteralPath $_.FullName -Algorithm SHA256).Hash }
$manifest = @{ Id='makrivo.example';Version='1.0.0';EntryPoint='Aivana.PluginHost.exe';ProtocolVersion=1;Capabilities=@('local-state','countdown');Hashes=$hashes;Dependencies=@();Actions=@(
    @{Id='example.countdown';Name='Countdown';ParameterSchema='{"type":"object","properties":{"seconds":{"type":"integer","minimum":0,"maximum":30}},"required":["seconds"],"additionalProperties":false}'},
    @{Id='example.state';Name='Status';ParameterSchema='{"type":"object","additionalProperties":false}'}
)}
$manifest | ConvertTo-Json -Depth 10 | Set-Content -LiteralPath (Join-Path $example 'manifest.json') -Encoding utf8
Compress-Archive -Path (Join-Path $example '*') -DestinationPath (Join-Path $destination 'makrivo.example.zip')
foreach ($file in @('MAKRIVO.exe','Aivana.AutomationHost.exe','Aivana.MobileHost.exe','wwwroot/panel.js','makrivo.example.zip')) {
    if (-not (Test-Path -LiteralPath (Join-Path $destination $file))) { throw "Package incomplete: $file" }
}
$runtimeConfigs = Get-ChildItem -LiteralPath $destination -Filter '*.runtimeconfig.json'
$nugetDirectory = if ($env:NUGET_PACKAGES) { $env:NUGET_PACKAGES } else { Join-Path ([Environment]::GetFolderPath('UserProfile')) '.nuget/packages' }
foreach ($config in $runtimeConfigs) {
    $runtime = Get-Content -LiteralPath $config.FullName -Raw | ConvertFrom-Json
    foreach ($framework in $runtime.runtimeOptions.includedFrameworks) {
        $package = $framework.name.ToLowerInvariant() + '.runtime.win-x64'
        $packageFolder = Join-Path $nugetDirectory "$package/$($framework.version)"
        $noticeFolder = Join-Path $destination "licenses/$package-$($framework.version)"
        New-Item -ItemType Directory -Path $noticeFolder -Force | Out-Null
        Get-ChildItem -LiteralPath $packageFolder -File | Where-Object { $_.Name -match '^(LICENSE|THIRD-PARTY-NOTICES)' } | ForEach-Object { Copy-Item -LiteralPath $_.FullName -Destination $noticeFolder }
    }
}
Copy-Item -LiteralPath (Join-Path $repository 'docs/testing/2026-10-12-six-extensions.md') -Destination (Join-Path $destination 'FEATURES-AND-TESTS.md')
$meetingNotes = Join-Path $repository 'docs/testing/2026-10-12-local-meeting-assistant.md'
if (!$StoreRelease -and (Test-Path -LiteralPath $meetingNotes)) { Copy-Item -LiteralPath $meetingNotes -Destination (Join-Path $destination 'MEETING-ASSISTANT.md') }
Write-Output "Release package: $destination"
