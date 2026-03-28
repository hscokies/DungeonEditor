using System.Text;
using Domain.Common;
using Domain.SaveFiles;

namespace Infrastructure.Services.Dungeons;

public static class AuthorEncoder
{
    public static byte[] Encode(string value)
    {
        if (value.ByteCount() > ByteSize.Author)
        {
            throw new ArgumentException($"invalid author name format: Value should be length of {ByteSize.Author}", nameof(value));
        }

        var bytes = new byte[ByteSize.Author];

        var stringChunks = value.Chunk(ByteSize.AuthorChunk)
            .Select(c => c.Reverse().ToArray());

        var index = 0;
        foreach (var chunk in stringChunks)
        {
            var chunkSize = chunk.Length;
            if (chunkSize != ByteSize.AuthorChunk)
            {
                index = ByteSize.AuthorChunk - chunkSize;
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
        if (bytes.Length != ByteSize.Author)
        {
            throw new ArgumentException($"invalid author name format: Value should be length of {ByteSize.Author}", nameof(bytes));
        }
        
        var normalizedBytes = bytes.Chunk(ByteSize.AuthorChunk)
            .SelectMany(c => c.Reverse())
            .Where(x => x is not 0x00)
            .ToArray();
        
        return Encoding.UTF8.GetString(normalizedBytes);
    }
}
