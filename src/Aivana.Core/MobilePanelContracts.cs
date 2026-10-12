using System.Security.Cryptography;
using System.Text;
namespace Aivana.Core;
public sealed record MobileAction(Guid Id, string Name, string Icon, bool Enabled, string Status);
public sealed record MobileProfile(Guid Id, string Name, MobileAction[] Actions);
public sealed record MobileSnapshot(bool Paused, MobileProfile[] Profiles, StepVerification[] Results);
public sealed record MobileCommand(Guid CommandId, Guid ActionId, string Operation);
public sealed class MobilePanelSecurity
{
    private readonly object _gate = new();
    private readonly HashSet<string> _hashes = [];
    private string? _code; private DateTimeOffset _expires; private int _attempts;
    public string NewPairingCode()
    {
        lock (_gate) { _code = RandomNumberGenerator.GetInt32(1000000).ToString("D6"); _expires = DateTimeOffset.UtcNow.AddSeconds(120); _attempts = 0; return _code; }
    }
    public string? Pair(string code)
    {
        lock (_gate)
        {
            if (_code == null || DateTimeOffset.UtcNow >= _expires || ++_attempts > 5 || code != _code) return null;
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32)); _hashes.Add(Hash(token)); _code = null; return token;
        }
    }
    public bool Authenticate(string? token) { lock (_gate) return token is { Length: 64 } && _hashes.Contains(Hash(token)); }
    public string[] ExportHashes() { lock (_gate) return _hashes.ToArray(); }
    public void ImportHashes(IEnumerable<string> hashes) { lock (_gate) foreach (var hash in hashes) if (hash.Length == 64 && hash.All(Uri.IsHexDigit)) _hashes.Add(hash); }
    public void RevokeAll() { lock (_gate) _hashes.Clear(); }
    private static string Hash(string token) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
}
public sealed class CommandDeduplicator
{
    private readonly HashSet<Guid> _ids = []; private readonly object _gate = new();
    // Fail closed when full; restarting the explicitly enabled service starts a new session.
    public bool TryAccept(Guid id) { lock (_gate) return id != Guid.Empty && _ids.Count < 10000 && _ids.Add(id); }
}
