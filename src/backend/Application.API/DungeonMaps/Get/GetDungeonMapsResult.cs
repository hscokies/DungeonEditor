namespace Application.DungeonMaps.Get;

public sealed record GetDungeonMapsResult(ICollection<string> Maps)
{
    public static GetDungeonMapsResult Empty => new([]);
}
