using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal sealed class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
{

    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasDiscriminator(x => x.Type)
            .HasValue<InboundTransaction>(TransactionType.Inbound)
            .HasValue<OutboundTransaction>(TransactionType.Outbound);

        builder.Property(x => x.Type)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}
