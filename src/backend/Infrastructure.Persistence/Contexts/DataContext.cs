using Domain.Dungeons;
using Domain.SaveFiles;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public class DataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IDataContext
{

    public DataContext(DbContextOptions<DataContext> dbContext)
        : base(dbContext)
    {
    }

    protected DataContext(DbContextOptions options)
        : base(options)
    {
    }

    public User User { get; init; }

    public DbSet<SaveFile> SaveFiles { get; init; }

    public DbSet<Dungeon> Dungeons { get; init; }

    public DbSet<Map> DungeonMaps { get; init; }

    public DbSet<Transaction> Transactions { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
