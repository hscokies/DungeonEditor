using Domain.Dungeons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

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
