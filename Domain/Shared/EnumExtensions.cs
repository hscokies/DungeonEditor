namespace Domain.Shared;

public static class EnumExtensions
{
    public static IEnumerable<TValue> GetValues<TEnum, TValue>()
        where TEnum : Enum
        where TValue : struct
    {
        return typeof(TEnum).GetEnumValues().Cast<TValue>();
    }
    
    public static ushort ToUshort(this byte[] bytes)
    {
        ushort result = bytes[0];
        for (var i = 1; i < bytes.Length; i++)
        {
            result = (ushort)((result << 8) | bytes[i]);
        }

        return result;
    } 
}