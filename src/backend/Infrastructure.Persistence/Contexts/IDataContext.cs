using Domain.Dungeons;
using Domain.SaveFiles;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Infrastructure.Persistence.Contexts;

public interface IReadOnlyDataContext
{
    DatabaseFacade Database { get; }

    public DbSet<User> Users { get; set; }

    public DbSet<SaveFile> SaveFiles { get; init; }

    public DbSet<Dungeon> Dungeons { get; init; }

    public DbSet<Map> DungeonMaps { get; init; }

    public DbSet<Transaction> Transactions { get; init; }
}

public interface IDataContext : IReadOnlyDataContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
