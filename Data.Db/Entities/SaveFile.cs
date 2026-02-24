namespace Data.Db.Entities;

public class SaveFile
{
    public Guid Id { get; init; }
    
    public Guid UserId { get; init; }
    public required User User { get; init; }
    
    public required ICollection<Dungeon> Dungeons { get; init; }
}