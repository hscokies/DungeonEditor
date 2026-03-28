using System.Text;

namespace Domain.Common;

public static class StringExtensions
{
    public static int ByteCount(this string value)
    {
        return Encoding.UTF8.GetByteCount(value);
    }
}
