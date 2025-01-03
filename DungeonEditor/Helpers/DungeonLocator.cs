using DungeonEditor.Constants;
using DungeonEditor.Dungeons;
using DungeonEditor.Extensions;

namespace DungeonEditor.Helpers;

public static class DungeonLocator
{
    private static readonly HashSet<ushort> Requirements = EnumExtensions.AsHashSet<JoinRequirements, ushort>();
    
    public static async IAsyncEnumerable<long> LocateDungeons(this Stream stream, CancellationToken ct = default)
    {
            var buffer = new byte[1024];
            var offset = 0L;
            while (await stream.ReadAsync(buffer, ct) is var read and > 0)
            {
                
                for (var i = 0; i < read - ByteSize.JoinRequirements + 1; i ++)
                {
                    var bytes = buffer[new Range(i, i + ByteSize.JoinRequirements)].AsUshort();;
                    if (!Requirements.Contains(bytes))
                    {
                        continue;
                    }

                    var currentPosition = stream.Position;
                    var dungeonStart = offset + i - Offset.JoinRequirements;
                    if (stream.IsDungeon(dungeonStart))
                    {
                        yield return offset + i - Offset.JoinRequirements;
                    }
                    stream.Seek(currentPosition, SeekOrigin.Begin);
                }
                offset += read;
            }
    }

    private static bool IsDungeon(this Stream stream, long offset)
    {
        var layoutBytes1 = new byte[ByteSize.Layout];
        stream.Seek(offset, SeekOrigin.Begin);
        var read = stream.Read(layoutBytes1, 0, ByteSize.Layout);
        if (read != ByteSize.Layout || layoutBytes1[0] is not 0x_1D)
        {
            return false;
        }

        var layoutBytes2 = new byte[ByteSize.Layout];
        stream.Seek(ByteSize.DungeonId, SeekOrigin.Current);
        read = stream.Read(layoutBytes2, 0, ByteSize.Layout);
        return read == ByteSize.Layout
            && layoutBytes2.SequenceEqual(layoutBytes1);
    }
}