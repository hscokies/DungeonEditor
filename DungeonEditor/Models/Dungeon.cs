using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DungeonEditor.Constants;
using DungeonEditor.Dungeons;
using DungeonEditor.Enumerations;
using DungeonEditor.Extensions;
using DungeonEditor.Helpers;
using DungeonEditor.Interfaces;
using Index = DungeonEditor.Enumerations.Index;

namespace DungeonEditor.Models;

public class Dungeon : DisposableWrapper
{
    private readonly FileStream _stream;
    private readonly long _offset;

    private readonly byte[] _data  = new byte[ByteSize.Dungeon];
    public string AuthorPsn => AuthorNameEncoder.Decode(_data[Index.Psn]);
    public string AuthorCharacter => AuthorNameEncoder.Decode(_data[Index.CharacterName]);
    public IEnumerable<Effect> Effects => Index.Effects.Select(range => (Effect)_data[range][^1]);
    public ulong DungeonId => _data[Index.DungeonId].AsUlong();
    public Area Area => (Area)_data[Index.Area[0]].AsUshort();
    public Map Map => (Map)_data[Index.Map[0]].AsUshort();
    public byte Seed => _data[Index.Seed[0]][0];
    public JoinRequirements JoinRequirements => (JoinRequirements) _data[Index.Requirements].AsUshort();



    public void SetAuthorPsn(string psn) => SetAuthor(psn, Offset.AuthorPsn);
    public void SetAuthorCharacter(string name) => SetAuthor(name, Offset.AuthorCharacter);
    public void SetEffects(params Effect[] effects)
    {
        Guard.IsTrue(effects.Length <= Index.Effects.Length);
        for (var i = 0; i < Index.Effects.Length; i++)
        {
            if (i + 1 > effects.Length)
            {
                Write((byte)(Index.Effects[i].End.Value - 1), (byte)Effect.SharedDefault);
                continue;
            }
            Write((byte)(Index.Effects[i].End.Value - 1), (byte)effects[i]);
        }
    }
    

    private Dungeon(FileStream stream, long offset)
    {
        _stream = stream;
        _offset = offset;
    }

    public Dictionary<GemCategory, int> GetGemCategories()
    {
        return Effects
            .Select(x => x.GetGemCategory())
            .Where(x => x != GemCategory.Unknown)
            .OrderBy(x => x)
            .Take(8)
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());
    }

    public static async Task<Dungeon> Create(FileStream stream, long offset, CancellationToken ct = default)
    {
        var dungeon = new Dungeon(stream, offset);
        await dungeon.SyncWithStream(ct);
        return dungeon;
    }
    public async Task SyncWithStream(CancellationToken ct)
    {
        _stream.Seek(_offset, SeekOrigin.Begin);
        var read = await _stream.ReadAsync(_data.AsMemory(0, ByteSize.Dungeon), ct);
        if (ByteSize.Dungeon != read)
        {
            throw new InvalidOperationException("Specified offset is not pointing to dungeon position");
        }
        
        var areaBytes1 = _data[Index.Area[0]].AsUshort();
        var areaBytes2 = _data[Index.Area[1]].AsUshort();
        Guard.Equals(areaBytes1, areaBytes2, "Areas mismatch");
        
        var mapBytes1 = _data[Index.Map[0]].AsUshort();
        var mapBytes2 = _data[Index.Map[1]].AsUshort();
        Guard.Equals(mapBytes1, mapBytes2, "Maps mismatch");
        
        var seedByte1 = _data[Index.Seed[0]][0];
        var seedByte2 = _data[Index.Seed[1]][0];
        Guard.Equals(seedByte1, seedByte2, "Seeds mismatch");
        
        var joinRequirementsBytes = _data[Index.Requirements].AsUshort();
        Guard.IsTrue(EnumExtensions.TryParse<JoinRequirements>(joinRequirementsBytes, out _), "Unable to parse join requirements");
    }
    
    private void Write(byte offset, params byte[] bytes)
    {
        Guard.IsTrue(offset + bytes.Length <= ByteSize.Dungeon);
        foreach (var b in bytes)
        {
            _data[offset++] = b;
        }
    }
    
    public override string ToString()
    {
        return $"""
                Dungeon ID: {DungeonId:X}
                Chalice: {JoinRequirements.GetEnumAttribute<JoinRequirements, DescriptionAttribute>()?.Description ?? JoinRequirements.ToString()} 
                Map: m{(ushort)Area >> 8}_{(ushort)Area & 0xFF}_{(byte)Map}_{Seed}
                Gem categories: {string.Join(' ', Effects.Select(x => x.GetGemCategory()).Where(x => x != GemCategory.Unknown).Select(x => (byte)x))}
                PSN: {AuthorPsn}
                Character: {AuthorCharacter}
                """;
    }

    private void SetAuthor(string psn, byte offset)
    {
        var encoded = AuthorNameEncoder.Encode(psn);
        Write(offset, encoded);
    }
    protected override void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _stream.Dispose();
        }

        _disposed = true;
    }
}