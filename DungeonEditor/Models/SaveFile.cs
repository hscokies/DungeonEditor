using System.Text;
using DungeonEditor.Enumerations;
using DungeonEditor.Helpers;
using DungeonEditor.Interfaces;

namespace DungeonEditor.Models;

public sealed class SaveFile : DisposableWrapper
{
    private readonly string _path;
    private readonly FileStream _saveFileStream;
    public readonly ICollection<Dungeon> Dungeons = new List<Dungeon>();

    public SaveFile(string path)
    {
        Guard.IsTrue(Path.Exists(path), "Save file not found!");
        _path = path;
        _saveFileStream = File.OpenRead(_path);
    }

    public async Task ReadAsync(CancellationToken ct = default)
    {
        await foreach (var offset in _saveFileStream.LocateDungeons(ct))
        {
            var dungeon = await Dungeon.Create(_saveFileStream, offset, ct);
            Dungeons.Add(dungeon);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _saveFileStream.Dispose();
        }

        _disposed = true;
    }
}