namespace Aivana.Core;
public sealed record VendorKeyBinding(string Adapter, string DeviceId, int KeyCode);
public sealed record KeyboardCapability(string Adapter, string Input, string Lighting, string Status, bool HardwareTested = false);
public sealed record VendorKeyEdge(string Adapter, string DeviceId, int KeyCode, bool Pressed);
public sealed class VendorIntegrationSettings
{
    public Dictionary<string,string> ApprovedLibraries { get; set; } = [];
    public float AnalogPress { get; set; } = .6f;
    public float AnalogRelease { get; set; } = .4f;
}
public sealed class AnalogThreshold
{
    private readonly HashSet<(string,int)> _down = [];
    public bool? Update(string device, int key, float value, float press, float release)
    {
        if (!(release >= 0 && release < press && press <= 1)) throw new ArgumentOutOfRangeException(nameof(press));
        var pair=(device,key); var held=_down.Contains(pair);
        if (float.IsNaN(value) || value < 0) { if (_down.Remove(pair)) return false; return null; }
        if(!held && value>=press) { _down.Add(pair);return true; }
        if(held && value<=release) { _down.Remove(pair);return false; }
        return null;
    }
    public (string Device,int Key)[] Reset() { var result=_down.ToArray(); _down.Clear();return result; }
}
