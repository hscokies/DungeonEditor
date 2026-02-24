namespace Infrastructure.Dungeons.Constants;

internal static class ParameterSize
{
    internal const int Map = 4;
    internal const int DungeonId = 8;
    internal const int JoinRequirements = 4; // pad-left 2
    internal const int Effect = 8; // pad-left 7
    internal const int Effects = Effect * 9;
    internal const int Author = 16;
    internal const int AuthorChunk = 4;

    internal const int Dungeon = Map + DungeonId + Map + JoinRequirements + Effects + Author * 2;
}