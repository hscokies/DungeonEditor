using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Domain.Dungeons;
using Domain.Shared;
using Infrastructure.Dungeons.Constants;
using Infrastructure.Dungeons.Models;

namespace Infrastructure.Dungeons.Services;

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

    public static async IAsyncEnumerable<RawDungeonInformation> GetDungeonOffsets(
        Stream stream,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var buffer = new byte[4096];
        long pointer = 0;

        while (await stream.ReadAsync(buffer.AsMemory(), cancellationToken) is var read && read > 0)
        {
            var maximumOffset = read - ParameterSize.JoinRequirements;

            for (var offset = 0; offset <= maximumOffset; offset++)
            {
                var joinRequirement = BinaryPrimitives.ReadUInt16LittleEndian(
                    buffer.AsSpan(offset, sizeof(ushort)));

                if (!JoinRequirementsLookup.Value[joinRequirement])
                    continue;

                var dungeonStart = pointer + offset - Offset.JoinRequirement;
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
    
    private static bool TryCreateDungeon(Stream stream, long offset, [NotNullWhen(true)] out RawDungeonInformation? dungeon)
    {
        dungeon = null;
        stream.Seek(offset, SeekOrigin.Begin);

        var dungeonBytes = new byte[ParameterSize.Dungeon];
        if (stream.Read(dungeonBytes) != ParameterSize.Dungeon)
        {
            return false;
        }

        const int dungeonMapId = 0x1D;
        if (dungeonBytes[Offset.MapOpen] != dungeonMapId)
        {
            return false;
        }

        for (int i = Offset.MapOpen, j = Offset.MapClose; i < ParameterSize.Map; i++)
        {
            if (dungeonBytes[i] != dungeonBytes[j])
            {
                return false;
            }
        }

        dungeon = new RawDungeonInformation(offset, dungeonBytes);
        return true;
    }
}
