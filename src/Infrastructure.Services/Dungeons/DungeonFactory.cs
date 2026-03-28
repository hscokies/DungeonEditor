using System.Buffers.Binary;
using Domain.Dungeons;
using Domain.SaveFiles;

namespace Infrastructure.Services.Dungeons;

public static class DungeonFactory
{
    public static Dungeon Create(long offset, byte[] bytes)
    {
        var area = bytes[Offsets.Area];
        var layoutSeed = bytes[Offsets.LayoutSeed];
        var instance = bytes[Offsets.Instance];
        
        return new Dungeon
        {
            Offset = offset,
            Map = new Map(area, layoutSeed, instance),

            JoinRequirement = ParseJoinRequirement(bytes),

            Effect1 = bytes[Offsets.Effect1],
            Effect2 = bytes[Offsets.Effect2],
            Effect3 = bytes[Offsets.Effect3],
            Effect4 = bytes[Offsets.Effect4],
            Effect5 = bytes[Offsets.Effect5],
            Effect6 = bytes[Offsets.Effect6],
            Effect7 = bytes[Offsets.Effect7],
            Effect8 = bytes[Offsets.Effect8],
            Effect9 = bytes[Offsets.Effect9],

            AuthorPSN = ParseAuthor(bytes, Offsets.AuthorPsn),
            AuthorCharacter = ParseAuthor(bytes, Offsets.AuthorCharacter),
        };
    }

    private static JoinRequirement ParseJoinRequirement(byte[] bytes)
    {
        return (JoinRequirement) BinaryPrimitives.ReadUInt16BigEndian(bytes.AsSpan(Offsets.JoinRequirement, ByteSize.JoinRequirementsValue));
    }

    private static string ParseAuthor(byte[] bytes, int offset)
    {
        var bytesRange = new Range(offset, offset + ByteSize.Author); // and here
        return AuthorEncoder.Decode(bytes[bytesRange]);
    }
}
