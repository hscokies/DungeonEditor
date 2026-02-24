using Infrastructure.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Db.Entities;

public class Role : IdentityRole<Guid>;

internal class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasData(
            new Role
            {
                Id = new Guid("53E398CB-8E0D-4C09-A515-75FEAD7B65E8"),
                Name = RoleName.Admin,
                NormalizedName = RoleName.Admin.ToUpper(),
            },
            new Role
            {
                Id = new Guid("A59B948F-A4B2-4CB0-B279-407E433401D0"),
                Name = RoleName.User,
                NormalizedName = RoleName.User.ToUpper(),
            }
        );
    }
}