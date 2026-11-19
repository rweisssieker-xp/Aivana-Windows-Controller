param([string]$KeyFile = $env:MakrivoKeyFile,[string]$CommerceUrl = $env:MakrivoCommerceUrl)
$ErrorActionPreference = 'Stop'
try {
    $uri = [Uri]$CommerceUrl
    if (-not $uri.IsAbsoluteUri -or $uri.Scheme -ne 'https' -or $uri.UserInfo) { throw 'Commerce URL must be HTTPS.' }
    $document = [System.Text.Json.JsonDocument]::Parse([IO.File]::ReadAllText($KeyFile))
    try {
        if ($document.RootElement.ValueKind -ne [System.Text.Json.JsonValueKind]::Object) { throw 'Public key ring must be an object.' }
        $names = [Collections.Generic.HashSet[string]]::new([StringComparer]::Ordinal)
        foreach ($entry in $document.RootElement.EnumerateObject()) {
            if ([string]::IsNullOrWhiteSpace($entry.Name) -or -not $names.Add($entry.Name)) { throw 'Invalid or duplicate key ID.' }
            $pem = $entry.Value.GetString()
            if (-not $pem -or $pem.Contains('PRIVATE KEY') -or ($pem -notmatch '^-----BEGIN (RSA )?PUBLIC KEY-----')) { throw 'Only public RSA keys allowed.' }
            $rsa = [Security.Cryptography.RSA]::Create()
            try { $rsa.ImportFromPem($pem); if ($rsa.KeySize -lt 3072) { throw 'RSA keys must be at least 3072 bits.' } }
            finally { $rsa.Dispose() }
        }
        if ($names.Count -eq 0) { throw 'Trusted public keys required.' }
    } finally { $document.Dispose() }
} catch { Write-Error ('MAKRIVO retail configuration: ' + $_.Exception.Message); exit 1 }
