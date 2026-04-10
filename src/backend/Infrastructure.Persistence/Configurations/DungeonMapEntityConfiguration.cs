using Domain.Dungeons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal sealed class DungeonMapEntityConfiguration : IEntityTypeConfiguration<Map>
{

    public void Configure(EntityTypeBuilder<Map> builder)
    {
        builder.HasIndex(x => x.Name);
        builder.Property(x => x.Name)
            .HasMaxLength(12);

        builder.HasIndex(x => new
        {
            x.Area,
            x.LayoutSeed,
            x.Instance
        });
    }
}
