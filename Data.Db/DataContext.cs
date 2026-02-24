using Data.Db.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Db;

public class DataContext : IdentityDbContext<User, Role, Guid>, IDataContext
{
    public DbSet<SaveFile> SaveFiles { get; init; }
    public DbSet<Dungeon> Dungeons { get; init; }
    
    public DataContext(DbContextOptions<DataContext> dbContext)
        : base(dbContext)
    {
    }

    protected DataContext(DbContextOptions options)
        : base(options)
    {
    }

    public User User { get; init; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}