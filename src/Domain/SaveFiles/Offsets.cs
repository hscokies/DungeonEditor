namespace Domain.SaveFiles;

/// <summary>
/// Defines relative offsets of the dungeon properties value.
/// </summary>
public static class Offsets
{
    /// <summary>
    /// Global BloodBorne map type.
    /// <remarks>
    /// Map 29 corresponds to dungeons.
    /// </remarks> 
    /// </summary>
    public const int MapType = 0;

    public const int Area = 1;
    public const int LayoutSeed = 2;
    public const int Instance = 3;
    public const int DungeonId = MapType + ByteSize.Map;
    public const int JoinRequirement = DungeonId + ByteSize.DungeonId + ByteSize.Map + ByteSize.JoinRequirementsPadding;
    
    public const int Effect1 = JoinRequirement + ByteSize.JoinRequirementsValue + ByteSize.EffectPadding;
    public const byte Effect2 = Effect1 + ByteSize.EffectPadding + 1; // 35
    public const byte Effect3 = Effect2 + ByteSize.EffectPadding + 1;
    public const byte Effect4 = Effect3 + ByteSize.EffectPadding + 1;
    public const byte Effect5 = Effect4 + ByteSize.EffectPadding + 1;
    public const byte Effect6 = Effect5 + ByteSize.EffectPadding + 1;
    public const byte Effect7 = Effect6 + ByteSize.EffectPadding + 1;
    public const byte Effect8 = Effect7 + ByteSize.EffectPadding + 1;
    public const byte Effect9 = Effect8 + ByteSize.EffectPadding + 1;
    
    public const byte AuthorPsn = Effect9 + 1;
    public const byte AuthorCharacter = AuthorPsn + ByteSize.Author;
    
    public const int MapOpen = 0;
    public const int MapClose = DungeonId + ByteSize.DungeonId;
}