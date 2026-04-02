using Domain.SaveFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal sealed class SaveFileEntityConfiguration : IEntityTypeConfiguration<SaveFile>
{

    public void Configure(EntityTypeBuilder<SaveFile> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.CreatedAt).IsDescending();
    }
}
