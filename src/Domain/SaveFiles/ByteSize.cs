namespace Domain.SaveFiles;

public static class ByteSize
{
    public const int Map = 4;
    public const int DungeonId = 8;

    public const int JoinRequirementsPadding = 2;
    public const int JoinRequirementsValue = 2;
    public const int JoinRequirements = JoinRequirementsPadding + JoinRequirementsValue;
    
    public const int EffectPadding = 7;
    public const int EffectValue = 1;
    public const int Effect = EffectPadding + EffectValue;
    public const int Effects = Effect * 9;
    
    public const int Author = 16;
    public const int AuthorChunk = 4;

    public const int Dungeon = Map + DungeonId + Map + JoinRequirements + Effects + Author * 2;
}
