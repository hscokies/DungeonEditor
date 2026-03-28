using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal sealed class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
{

    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasDiscriminator<string>("type")
            .HasValue<InboundTransaction>("inbound")
            .HasValue<OutboundTransaction>("outbound");
    }
}
