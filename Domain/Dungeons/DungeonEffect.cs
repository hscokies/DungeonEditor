using System.Text.Json.Serialization;

namespace Domain.Dungeons;

public sealed class DungeonEffect
{
    public required byte Identifier { get; init; }
    public required EffectCategory Category { get; init; }
    public required string Description { get; init; }
    public required GemCategory GemCategory { get; init; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EffectCategory : byte
{
    SpecialEnemyShop,
    UniqueItem,
    GemEffect,
    DifficultyUp,
    AdditionalLayer,
    Environment,
    Rite,
    RootChaliceDrop
}