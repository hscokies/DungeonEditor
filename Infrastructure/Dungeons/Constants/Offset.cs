namespace Infrastructure.Dungeons.Constants;

internal static class Offset
{
    internal const int MapOpen = 0;
    internal const int MapClose = DungeonId + ParameterSize.DungeonId;
    
    internal const int DungeonId = MapOpen + ParameterSize.Map;
    
    internal const int JoinRequirement = MapClose + ParameterSize.Map;
    
    // Not includes padding
    internal const byte Effect1 = JoinRequirement + ParameterSize.JoinRequirements;
    internal const byte Effect2 = Effect1 + ParameterSize.Effect;
    internal const byte Effect3 = Effect2 + ParameterSize.Effect;
    internal const byte Effect4 = Effect3 + ParameterSize.Effect;
    internal const byte Effect5 = Effect4 + ParameterSize.Effect;
    internal const byte Effect6 = Effect5 + ParameterSize.Effect;
    internal const byte Effect7 = Effect6 + ParameterSize.Effect;
    internal const byte Effect8 = Effect7 + ParameterSize.Effect;
    internal const byte Effect9 = Effect8 + ParameterSize.Effect;
    
    internal const byte AuthorPsn = Effect9 + ParameterSize.Effect;
    internal const byte AuthorCharacter = AuthorPsn + ParameterSize.Author;
}