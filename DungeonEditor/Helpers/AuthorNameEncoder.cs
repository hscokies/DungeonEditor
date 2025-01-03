using System.Text;
using DungeonEditor.Constants;

namespace DungeonEditor.Helpers;

/// <summary>
/// Author's character name and PSN seems to be in weird order 
/// <example>
/// 123456789 = 432156789
/// </example>
/// 
/// </summary>
public static class AuthorNameEncoder
{
    private const byte Null = 0x00;

    public static byte[] Encode(string value)
    {
        Guard.IsTrue(value.Length <= ByteSize.Author);

        var bytes = new byte[ByteSize.Author];

        var stringChunks = value.Chunk(ByteSize.AuthorChunk).Select(c => c.Reverse().ToArray());

        var index = 0;
        foreach (var chunk in stringChunks)
        {
            var chunkSize = chunk.Length;
            while (chunkSize != ByteSize.AuthorChunk && index < ByteSize.Author)
            {
                bytes[index++] = Null;
                chunkSize++;
            }

            for (var i = 0; i < chunk.Length && index < ByteSize.Author; i++)
            {
                bytes[index++] = (byte)chunk[i];   
            }
        }

        return bytes;
    }

    public static string Decode(byte[] bytes)
    {
        Guard.IsTrue(bytes.Length <= ByteSize.Author);
        var sb = new StringBuilder();
        var chunks = bytes.Chunk(ByteSize.AuthorChunk).Select(c => c.Reverse());
        foreach (var chunk in chunks)
        {
            foreach (var b in chunk)
            {
                sb.Append((char)b);
            }
        }

        return sb.ToString();
    }
}