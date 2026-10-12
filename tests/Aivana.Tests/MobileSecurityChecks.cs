using Aivana.Core;
internal static class MobileSecurityChecks
{
    public static Task RunAsync()
    {
        var security = new MobilePanelSecurity();
        var code = security.NewPairingCode();
        if (security.Pair("000000" == code ? "111111" : "000000") != null) throw new Exception("Wrong code paired");
        var token = security.Pair(code) ?? throw new Exception("Pairing failed");
        if (!security.Authenticate(token) || security.Authenticate("fake")) throw new Exception("Authentication failed");
        if (security.Pair(code) != null) throw new Exception("Code reused");
        security.RevokeAll(); if (security.Authenticate(token)) throw new Exception("Revoked client accepted");
        var commands = new CommandDeduplicator(); var id = Guid.NewGuid();
        if (!commands.TryAccept(id) || commands.TryAccept(id)) throw new Exception("Duplicate execution accepted");
        return Task.CompletedTask;
    }
}
