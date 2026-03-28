using Domain.SaveFiles;

namespace Domain.Dungeons;

public sealed class Dungeon
{
    public Guid Id { get; init; }
    public long Offset { get; init; }
    public Guid SaveFileId { get; init; }
    public SaveFile? SaveFile { get; init; }

    public required string Map { get; init; }
    public JoinRequirement JoinRequirement { get; set; }
    
    public byte Effect1 { get; set; }
    public byte Effect2 { get; set; }
    public byte Effect3 { get; set; }
    public byte Effect4 { get; set; }
    public byte Effect5 { get; set; }
    public byte Effect6 { get; set; }
    public byte Effect7 { get; set; }
    public byte Effect8 { get; set; }
    public byte Effect9 { get; set; }
    
    public required string AuthorPSN { get; set; }
    public required string AuthorCharacter { get; set; }
}