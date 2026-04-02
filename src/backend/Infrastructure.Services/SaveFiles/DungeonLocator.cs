using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Domain.Common;
using Domain.Dungeons;
using Domain.SaveFiles;
using Infrastructure.Services.Dungeons;

namespace Infrastructure.Services.SaveFiles;

public static class DungeonLocator
{
    private static readonly Lazy<bool[]> JoinRequirementsLookup = new(() =>
    {
        var lookup = new bool[ushort.MaxValue + 1];
        foreach (var value in EnumExtensions.GetValues<JoinRequirement, ushort>())
        {
            lookup[value] = true;
        }

        return lookup;
    });
    
        public static async IAsyncEnumerable<Dungeon> GetDungeonOffsets(
        Stream stream,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var buffer = new byte[4096];
        long pointer = 0;

        while (await stream.ReadAsync(buffer.AsMemory(), cancellationToken) is var read && read > 0)
        {
            var maximumOffset = read - ByteSize.JoinRequirementsValue;

            // offset of join requirements start in buffer
            // pointer: start of the stream, where we read
            for (var offset = 0; offset <= maximumOffset; offset++)
            {
                var joinRequirement = BinaryPrimitives.ReadUInt16BigEndian(
                    buffer.AsSpan(offset, ByteSize.JoinRequirementsValue));

                if (!JoinRequirementsLookup.Value[joinRequirement])
                {
                    continue;
                }

                var dungeonStart = pointer + offset - Offsets.JoinRequirement;
                if (dungeonStart < 0)
                {
                    continue;
                }

                var currentPosition = stream.Position;

                if (TryCreateDungeon(stream, dungeonStart, out var dungeon))
                {
                    yield return dungeon;
                }

                stream.Position = currentPosition;
            }

            pointer += read;
        }
    }
    
    private static bool TryCreateDungeon(
        Stream stream, long offset, 
        [NotNullWhen(true)] out Dungeon? dungeon)
    {
        dungeon = null;
        stream.Seek(offset, SeekOrigin.Begin);

        var dungeonBytes = new byte[ByteSize.Dungeon];
        if (stream.Read(dungeonBytes) != ByteSize.Dungeon)
        {
            return false;
        }

        const int dungeonMapType = 0x1D;
        if (dungeonBytes[Offsets.MapType] != dungeonMapType)
        {
            return false;
        }

        for (int i = Offsets.MapOpen, j = Offsets.MapClose; i < ByteSize.Map; i++, j++)
        {
            if (dungeonBytes[i] != dungeonBytes[j])
            {
                return false;
            }
        }

        dungeon = DungeonFactory.Create(offset, dungeonBytes);
        return true;
    }
}
