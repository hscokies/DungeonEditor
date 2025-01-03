namespace DungeonEditor.Extensions;

public static class ByteHelpers
{
    public static ulong AsUlong(this byte[] bytes)
    {
        ulong result = bytes[0];
        for (var i = 1; i < bytes.Length; i++)
        {
            result = (result << 8) | bytes[i];
        }

        return result;
    }
    
    public static ushort AsUshort(this byte[] bytes)
    {
        ushort result = bytes[0];
        for (var i = 1; i < bytes.Length; i++)
        {
            result = (ushort)((result << 8) | bytes[i]);
        }

        return result;
    }

    public static ulong Combine(params byte[] bytes) => bytes.AsUlong();
}