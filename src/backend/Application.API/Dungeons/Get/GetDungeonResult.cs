using Domain.Dungeons;

namespace Application.Dungeons.Get;

public sealed record GetDungeonResult(
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
    string AuthorCharacter);
