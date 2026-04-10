using Application.Common;

namespace Application.DungeonMaps.Get;

public record GetDungeonMapsQuery(string Search) : IQuery<GetDungeonMapsResult>
{
    public const string Path = "/dungeons/map";
}
