using Domain.Dungeons;
using Infrastructure.Persistence.Seeders.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

internal static class DungeonMapSeeder
{
    internal static async Task SeedAsync(DbContext dataContext, CancellationToken cancellationToken)
    {
        var maps = dataContext.Set<Map>();

        var hasData = await maps.AnyAsync(cancellationToken);
        if (hasData)
        {
            return;
        }

        const int batchSize = 500;
        var counter = 0;

        using var reader = SeedResources.GetDungeonMaps();
        while (await reader.ReadLineAsync(cancellationToken) is { } line)
        {
            counter++;

            var map = Map.Parse(line);
            await dataContext.AddAsync(map, cancellationToken);

            if (counter % batchSize == 0)
            {
                await dataContext.SaveChangesAsync(cancellationToken);
            }
        }

        if (counter % batchSize != 0)
        {
            await dataContext.SaveChangesAsync(cancellationToken);
        }
    }

    internal static void Seed(DbContext dataContext)
    {
        var maps = dataContext.Set<Map>();

        var hasData = maps.Any();
        if (hasData)
        {
            return;
        }

        const int batchSize = 500;
        var counter = 0;

        using var reader = SeedResources.GetDungeonMaps();
        while (reader.ReadLine() is { } line)
        {
            counter++;

            var map = Map.Parse(line);
            dataContext.Add(map);

            if (counter % batchSize == 0)
            {
                dataContext.SaveChanges();
            }
        }

        if (counter % batchSize != 0)
        {
            dataContext.SaveChanges();
        }
    }
}
