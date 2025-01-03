namespace DungeonEditor.Constants;

public static class ByteSize
{
    public const byte Dungeon = 124;
    public const byte Area = 2;
    public const byte DungeonId = 8;
    public const byte Map = 1;
    public const byte Seed = 1;
    public const byte Layout = Area + Map + Seed;
    public const byte JoinRequirements = 4; // Can be converted to 2 bytes
    public const byte Effect = 8;
    public const byte Effects = Effect * 9;
    public const byte Author = 16;
    public const byte AuthorChunk = Author / 4;
}