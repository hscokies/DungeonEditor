using Application.Common;

namespace Application.DungeonMaps.Get;

public record GetDungeonMapsQuery(string? Search, ushort Offset, ushort Limit) : IQuery<GetDungeonMapsResult>
{
    public const string Path = "/dungeons/maps";
}
