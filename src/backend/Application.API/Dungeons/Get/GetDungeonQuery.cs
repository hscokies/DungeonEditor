using Application.Common;

namespace Application.Dungeons.Get;

public sealed record GetDungeonQuery(Guid UserId, Guid Id) : IQuery<GetDungeonResult>
{
    public const string Path = "/dungeons/{id:guid}";
}
