namespace Domain.Dungeons;

public record struct Map(byte Area, byte LayoutSeed, byte Instance)
{
    public override string ToString() => $"m29_{Area:D2}_{LayoutSeed:D2}_{Instance:D2}";
    
    public static implicit operator string(Map map) => map.ToString();
}
