using Domain.Dungeons;
using Domain.Users;

namespace Domain.SaveFiles;

public class SaveFile
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public SaveFileState State { get; set; } = SaveFileState.Uploaded;
    
    public Guid UserId { get; init; }
    public User? User { get; init; }

    public ICollection<Dungeon> Dungeons { get; init; } = [];

    public string Path => $"{UserId}/{Id}";
}

public enum SaveFileState : byte
{
    Uploaded,
    Processing,
    Processed,
    Error,
}
