using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using DungeonEditor.Attributes;
using DungeonEditor.Dungeons;
using DungeonEditor.Enumerations;

namespace DungeonEditor.Helpers;

public static class EnumExtensions
{
    public static HashSet<TValue> AsHashSet<TEnum, TValue>()
        where TEnum : Enum
        where TValue : struct
    {
        return typeof(TEnum).GetEnumValues().Cast<TValue>().ToHashSet();
    }

    public static bool TryParse<TEnum>(ValueType value, out TEnum result) where TEnum : Enum
    {
        result = (TEnum)value;
        return Enum.IsDefined(typeof(TEnum), value);
    }

    public static TAttribute? GetEnumAttribute<TEnum, TAttribute>(this TEnum value)
        where TAttribute : Attribute
        where TEnum : Enum
    {
        return typeof(TEnum).GetField(value.ToString())?.GetCustomAttribute<TAttribute>();
    }
    public static GemCategory GetGemCategory(this Effect value) => value.GetEnumAttribute<Effect, GemCategoryAttribute>()?.Get() ?? GemCategory.Unknown;
}
