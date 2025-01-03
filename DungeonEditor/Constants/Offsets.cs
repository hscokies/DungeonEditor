namespace DungeonEditor.Constants;

public static class Offset
{
    public const byte AreaOpening = 0;
    public const byte MapOpening = AreaOpening + ByteSize.Area;
    public const byte SeedOpening = MapOpening + ByteSize.Map;
    
    public const byte DungeonId = SeedOpening + ByteSize.Seed;
    
    public const byte AreaClosing = DungeonId + ByteSize.DungeonId;
    public const byte MapClosing = AreaClosing + ByteSize.Area;
    public const byte SeedClosing = MapClosing + ByteSize.Map;

    public const byte JoinRequirements = SeedClosing + ByteSize.Seed;

    public const byte Effect1 = JoinRequirements + ByteSize.JoinRequirements;
    public const byte Effect2 = Effect1 + ByteSize.Effect;
    public const byte Effect3 = Effect2 + ByteSize.Effect;
    public const byte Effect4 = Effect3 + ByteSize.Effect;
    public const byte Effect5 = Effect4 + ByteSize.Effect;
    public const byte Effect6 = Effect5 + ByteSize.Effect;
    public const byte Effect7 = Effect6 + ByteSize.Effect;
    public const byte Effect8 = Effect7 + ByteSize.Effect;
    public const byte Effect9 = Effect8 + ByteSize.Effect;

    public const byte AuthorPsn = Effect9 + ByteSize.Effect;
    public const byte AuthorCharacter = AuthorPsn + ByteSize.Author;
}