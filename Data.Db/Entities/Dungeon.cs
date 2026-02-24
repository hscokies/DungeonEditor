using Domain.Dungeons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Db.Entities;

public sealed class Dungeon
{
    public Guid Id { get; init; }
    public long Offset { get; init; }
    public Guid SaveFileId { get; init; }
    public SaveFile SaveFile { get; init; } = null!;
    
    public required string Map { get; set; }
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

internal sealed class DungeonEntityConfiguration : IEntityTypeConfiguration<Dungeon>
{
    public void Configure(EntityTypeBuilder<Dungeon> builder)
    {
        builder.HasOne(x => x.SaveFile)
            .WithMany(x => x.Dungeons)
            .HasForeignKey(x => x.SaveFileId);

        builder.Property(x => x.Map)
            .HasMaxLength(12);

        builder.Property(x => x.AuthorPSN)
            .HasMaxLength(16);
        
        builder.Property(x => x.AuthorCharacter)
            .HasMaxLength(16);
    }
}