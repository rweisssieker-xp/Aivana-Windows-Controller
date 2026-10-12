namespace Aivana.Core;
public static class VendorBindingValidator
{
    public static IEnumerable<string> ValidateProfile(Profile profile)
    {
        var tiles=profile.Tiles.Where(t=>t is { Enabled:true, VendorBinding:not null }).ToArray();
        var layers=profile.Tiles.Where(t=>t.Enabled && (t.VendorBinding!=null || t.KeyboardBinding!=null || t.CorsairBinding!=null) && !string.IsNullOrWhiteSpace(t.HeldLayer)).Select(t=>t.HeldLayer.Trim()).ToHashSet(StringComparer.OrdinalIgnoreCase);
        foreach(var tile in tiles)
        {
            if(!string.IsNullOrWhiteSpace(tile.HeldLayer) && (tile.KeyGesture!=KeyGesture.Press || !string.IsNullOrWhiteSpace(tile.KeyLayer))) yield return "Eine gehaltene Herstellertaste muss auf der Basisebene mit Sofortdruck belegt sein.";
            if(!string.IsNullOrWhiteSpace(tile.KeyLayer) && !layers.Contains(tile.KeyLayer.Trim())) yield return "Herstellertaste verwendet eine Ebene ohne gehaltene Ebenentaste.";
        }
        foreach(var group in tiles.GroupBy(t=>(t.VendorBinding!.Adapter.ToUpperInvariant(),t.VendorBinding.DeviceId.ToUpperInvariant(),t.VendorBinding.KeyCode,(t.KeyLayer ?? "").Trim().ToUpperInvariant())))
            if(group.GroupBy(t=>t.KeyGesture).Any(g=>g.Count()>1) || group.Any(t=>t.KeyGesture==KeyGesture.Press) && group.Count()>1) yield return "Eine Herstellertaste ist mit widersprüchlichen Gesten mehrfach belegt.";
    }
}
