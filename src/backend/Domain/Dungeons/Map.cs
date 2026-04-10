namespace Domain.Dungeons;

public class Map
{


    protected Map() { }

    public Map(byte area, byte layoutSeed, byte instance)
    {
        Name = $"m29_{area:D2}_{layoutSeed:D2}_{instance:D2}";
        Area = area;
        LayoutSeed = layoutSeed;
        Instance = instance;
        Id = (Area << 16) | (LayoutSeed << 8) | Instance;
    }

    public int Id { get; init; }

    public string Name { get; init; }

    public byte Area { get; init; }

    public byte LayoutSeed { get; init; }

    public byte Instance { get; init; }


    public static Map Parse(string map)
    {
        var parts = map.Split('_');

        var area = byte.Parse(parts[1]);
        var layoutSeed = byte.Parse(parts[2]);
        var instance = byte.Parse(parts[3]);

        return new Map(area, layoutSeed, instance);
    }


    public static implicit operator string(Map map) => map.Name;

    public static implicit operator byte[](Map map) => [map.Area, map.LayoutSeed, map.Instance];
}
