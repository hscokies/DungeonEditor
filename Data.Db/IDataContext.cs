using Data.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Data.Db;


public interface IReadOnlyDataContext
{
    DatabaseFacade Database { get; }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    public DbSet<SaveFile> SaveFiles { get; init; }
    public DbSet<Dungeon> Dungeons { get; init; }
}

public interface IDataContext : IReadOnlyDataContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}