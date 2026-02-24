using Domain.Dungeons;

namespace Infrastructure.Gems;

public static class GemPoolCalculator
{
    internal static GemPool CalculateGemPool(this Dungeon dungeon)
    {
        var frequencies = dungeon.GemCategories
            .GroupBy(c => c)
            .ToDictionary(g => g.Key, g => g.Count());
        

        return new GemPool(
            Calculate(frequencies, GemRarities.Primaries),
            Calculate(frequencies, GemRarities.Secondaries)
        );
    }


    private static Dictionary<TEffect, double> Calculate<TEffect>(
        Dictionary<GemCategory, int> frequencies,
        Dictionary<GemCategory, Dictionary<TEffect, double>> categoryRarities
    ) where TEffect : Enum
    {
        var scores = new Dictionary<TEffect, double>();

        foreach (var (category, rarities) in categoryRarities)
        {
            frequencies.TryGetValue(category, out var frequency);
            foreach (var (effect, rarity) in rarities)
            {
                scores[effect] = scores.TryGetValue(effect, out var score)
                    ? score + rarity * Math.Pow(100, frequency)
                    : rarity * Math.Pow(100, frequency);
            }
        }

        var sum = scores.Values.Sum();
        foreach (var key in scores.Keys)
        {
            scores[key] = scores[key] / sum * 100;
        }

        return scores;
    }
}