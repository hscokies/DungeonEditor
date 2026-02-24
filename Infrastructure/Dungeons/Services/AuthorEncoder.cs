using System.Text;
using Infrastructure.Dungeons.Constants;

namespace Infrastructure.Dungeons.Services;

public static class AuthorEncoder
{

    public static byte[] Encode(string value)
    {
        if (value.Length > ParameterSize.Author)
        {
            throw new ArgumentException($"invalid author name format: Value should be length of {ParameterSize.Author}", nameof(value));
        }

        var bytes = new byte[ParameterSize.Author];

        var stringChunks = value.Chunk(ParameterSize.AuthorChunk)
            .Select(c => c.Reverse().ToArray());

        var index = 0;
        foreach (var chunk in stringChunks)
        {
            var chunkSize = chunk.Length;
            if (chunkSize != ParameterSize.AuthorChunk)
            {
                index = ParameterSize.AuthorChunk - chunkSize;
            }

            for (var i = 0; i < chunk.Length && index < ParameterSize.Author; i++)
            {
                bytes[index++] = (byte)chunk[i];   
            }
        }

        return bytes;
    }

    public static string Decode(byte[] bytes)
    {
        if (bytes.Length != ParameterSize.Author)
        {
            throw new ArgumentException($"invalid author name format: Value should be length of {ParameterSize.Author}", nameof(bytes));
        }

        var sb = new StringBuilder();
        var chunks = bytes.Chunk(ParameterSize.AuthorChunk)
            .Select(c => c.Reverse());
        
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