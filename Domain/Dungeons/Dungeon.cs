using Domain.Gems;

namespace Domain.Dungeons;

public sealed class Dungeon
{
    public required Map Map { get; set; }
    public required JoinRequirement JoinRequirement { get; set; }

    public required string AuthorPSN { get; set; }
    public required string AuthorCharacterName { get; set; }
    
    public required DungeonEffect Effect1 { get; set; }
    public required DungeonEffect Effect2 { get; set; }
    public required DungeonEffect Effect3 { get; set; }
    public required DungeonEffect Effect4 { get; set; }
    public required DungeonEffect Effect5 { get; set; }
    public required DungeonEffect Effect6 { get; set; }
    public required DungeonEffect Effect7 { get; set; }
    public required DungeonEffect Effect8 { get; set; }
    public required DungeonEffect Effect9 { get; set; }

    public IReadOnlyCollection<GemCategory> GemCategories =>
    [
        Effect1.GemCategory,
        Effect2.GemCategory,
        Effect3.GemCategory,
        Effect4.GemCategory,
        Effect5.GemCategory,
        Effect6.GemCategory,
        Effect7.GemCategory,
        Effect8.GemCategory,
        Effect9.GemCategory
    ];
}