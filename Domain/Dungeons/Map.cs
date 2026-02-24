namespace Domain.Dungeons;

public class Map(Area area, byte layoutSeed, byte instance)
{
    public Area Area { get; set; } = area;
    public byte LayoutSeed { get; set; } = layoutSeed;
    public byte Instance { get; set; } = instance;

    public override string ToString()
    {
        return $"m29_{(byte) Area}_{LayoutSeed}_{Instance}";
    }

    public static bool TryParse(string mapId, out Map? map)
    {
        map = null;
        
        var parts = mapId.Split('_');
        if (parts is not ["m29", _, _, _])
        {
            return false;
        }

        if (!Enum.TryParse<Area>(parts[1], out var area))
        {
            return false;
        }

        if (!byte.TryParse(parts[2], out var layoutSeed))
        {
            return false;
        }

        if (!byte.TryParse(parts[3], out var instance))
        {
            return false;
        }

        map = new Map(area, layoutSeed, instance);
        
        return true;
    }
}