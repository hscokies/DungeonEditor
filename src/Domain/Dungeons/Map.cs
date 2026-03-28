namespace Domain.Dungeons;

public record struct Map(byte Area, byte LayoutSeed, byte Instance)
{
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
