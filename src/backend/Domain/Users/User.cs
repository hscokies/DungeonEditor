using Microsoft.AspNetCore.Identity;

namespace Domain.Users;

public class User : IdentityUser<Guid>
{
    public short Balance { get; set; }

    public ICollection<IdentityRole<Guid>> Roles { get; set; }
}
