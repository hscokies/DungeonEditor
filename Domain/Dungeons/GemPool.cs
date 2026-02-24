using Domain.Gems;

namespace Domain.Dungeons;

public sealed record GemPool(
    IDictionary<PrimaryGemEffect, double> Primaries,
    IDictionary<SecondaryGemEffect, double> Secondaries
);