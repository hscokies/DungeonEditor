namespace Domain.Dungeons;

public class Map
{


    protected Map() { }

    public Map(byte area, byte layoutSeed, byte instance)
    {
        Area = area;
        LayoutSeed = layoutSeed;
        Instance = instance;
        Id = (Area << 16) | (LayoutSeed << 8) | Instance;
    }

    public Map(int id)
    {
        Area = (byte)((Area >> 16) & 0xFF);
        LayoutSeed = (byte)((LayoutSeed >> 8) & 0xFF);
        Instance = (byte)(Instance & 0xFF);
    }

    public int Id { get; init; }

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

    public override string ToString() => $"m29_{Area:D2}_{LayoutSeed:D2}_{Instance:D2}";

    public static implicit operator string(Map map) => map.ToString();

    public static implicit operator byte[](Map map) => [map.Area, map.LayoutSeed, map.Instance];
}
