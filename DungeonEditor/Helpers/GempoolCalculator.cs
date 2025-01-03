using System.Reflection;
using DungeonEditor.Attributes;
using DungeonEditor.Constants;
using DungeonEditor.Enumerations;
using DungeonEditor.Models;

namespace DungeonEditor.Helpers;

public static class GempoolCalculator
{
    private static Dictionary<string, Dictionary<GemCategory, double>> _primaries = new();
    private static Dictionary<string, Dictionary<GemCategory, double>> _secondaries = new();
    
    static GempoolCalculator()
    {
        var primaries = typeof(Gems.Primaries).GetFields();
        foreach (var fieldInfo in primaries)
        {
            var value = (Dictionary<GemCategory, double>)fieldInfo.GetValue(null)!;
            var gems = fieldInfo.GetCustomAttribute<NameAttribute>()!.Get();
            foreach (var gemName in gems)
            {
                _primaries.Add(gemName, value);
            }
        }
        
        var secondaries = typeof(Gems.Secondaries).GetFields();
        foreach (var fieldInfo in secondaries)
        {
            var value = (Dictionary<GemCategory, double>)fieldInfo.GetValue(null)!;
            var gems = fieldInfo.GetCustomAttribute<NameAttribute>()!.Get();
            foreach (var gemName in gems)
            {
                _secondaries.Add(gemName, value);
            }
        }
    }
    public static GemPool GetGemPool(Dictionary<GemCategory, int> categories)
    {
        return new GemPool
        {
            Primaries = CalculatePercents(categories, _primaries),
            Secondaries = CalculatePercents(categories, _secondaries)
        };
    }

    private static Dictionary<string, double> CalculatePercents(
        IReadOnlyDictionary<GemCategory, int> categories,
        Dictionary<string, Dictionary<GemCategory, double>> gems)
    {
        var gemInformation = new Dictionary<string, double>();
        var total = 0ul;
        foreach (var (gemName, gemCategories) in gems)
        {
            var value = 0d;
            foreach (var (category, rarity) in gemCategories)
            {
                value += rarity * Math.Pow(100, categories.GetValueOrDefault(category, 0));
            }
            value *= 100;

            total += (ulong)value;
            gemInformation.Add(gemName, value);
        }
        
        foreach (var name in gemInformation.Keys)
        {
            gemInformation[name] = gemInformation[name] / total * 100;
        }

        return gemInformation;
    }
}