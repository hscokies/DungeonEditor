using System.Reflection;

namespace Domain.Common;

public static class EnumExtensions
{
    public static IEnumerable<TValue> GetValues<TEnum, TValue>()
        where TEnum : Enum
        where TValue : struct
    {
        return typeof(TEnum).GetEnumValues().Cast<TValue>();
    }
    
        public static TAttribute? GetEnumAttribute<TEnum, TAttribute>(this TEnum value)
        where TAttribute : Attribute
        where TEnum : Enum
    {
        return typeof(TEnum).GetField(value.ToString())?.GetCustomAttribute<TAttribute>();
    }
}