using Domain.Dungeons;
using Domain.Users;

namespace Domain.SaveFiles;

public class SaveFile
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public string FileName { get; init; } = string.Empty;

    public SaveFileState State { get; set; } = SaveFileState.Uploaded;

    public Guid UserId { get; init; }

    public User? User { get; init; }

    public ICollection<Dungeon> Dungeons { get; init; } = [];

    public string Path => $"{UserId}/{Id}";
}

public enum SaveFileState : byte
{
    /// <summary>
    /// Initial state.
    /// </summary>
    Uploaded = 0,

    /// <summary>
    /// Save file is being parsed.
    /// </summary>
    Processing = 1,

    /// <summary>
    /// Save file parsed successfully.
    /// </summary>
    Processed = 2,

    /// <summary>
    /// Unable to locate any dungeons in save file.
    /// </summary>
    Failed = 3,

    /// <summary>
    /// Exception during file parsing
    /// </summary>
    Error = 4,
}
