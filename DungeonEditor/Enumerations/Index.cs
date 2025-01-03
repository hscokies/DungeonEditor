using DungeonEditor.Constants;

namespace DungeonEditor.Enumerations;

public static class Index
{
    public static Range[] Area = [Offset.AreaOpening..Offset.MapOpening, Offset.AreaClosing..Offset.MapClosing];
    public static Range[] Map = [Offset.MapOpening..Offset.SeedOpening, Offset.MapClosing..Offset.SeedClosing];
    public static Range[] Seed = [Offset.SeedOpening..Offset.DungeonId,Offset.SeedClosing..Offset.JoinRequirements];
    public static Range[] Layout = [Offset.MapOpening..Offset.DungeonId, Offset.MapClosing..Offset.JoinRequirements];
    public static Range DungeonId = Offset.DungeonId..Offset.AreaClosing;
    public static Range Requirements = Offset.JoinRequirements..Offset.Effect1;
    public static Range Psn = Offset.AuthorPsn..Offset.AuthorCharacter;
    public static Range CharacterName = Offset.AuthorCharacter..ByteSize.Dungeon;
    public static Range[] Effects =
    [
        Offset.Effect1..Offset.Effect2,
        Offset.Effect2..Offset.Effect3,
        Offset.Effect3..Offset.Effect4,
        Offset.Effect4..Offset.Effect5,
        Offset.Effect5..Offset.Effect6,
        Offset.Effect6..Offset.Effect7,
        Offset.Effect7..Offset.Effect8,
        Offset.Effect8..Offset.Effect9,
        Offset.Effect9..Offset.AuthorPsn,
    ];
}