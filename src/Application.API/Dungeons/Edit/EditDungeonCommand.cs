using Application.Common;
using Domain.Dungeons;

namespace Application.Dungeons.Edit;

public sealed record EditDungeonCommand(
    Guid UserId,
    Guid Id,
    string Map,
    JoinRequirement JoinRequirement,
    byte Effect1,
    byte Effect2,
    byte Effect3,
    byte Effect4,
    byte Effect5,
    byte Effect6,
    byte Effect7,
    byte Effect8,
    byte Effect9,
    string AuthorPSN,
    string AuthorCharacter) : ICommand
{
    public const string Path = "/dungeons/{id:guid}";
}
