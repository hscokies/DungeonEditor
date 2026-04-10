using Application.Common;
using Common.Data;
using Domain.Common;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Application.DungeonMaps.Get;

public sealed class GetDungeonMapsHandler(IReadOnlyDataContext dataContext, IMemoryCache cache) : IQueryHandler<GetDungeonMapsQuery, GetDungeonMapsResult>
{
    const int batchSize = 75;

    public async Task<Result<GetDungeonMapsResult>> Handle(GetDungeonMapsQuery query, CancellationToken cancellationToken)
    {
        var normalizedSearch = query.Search.Trim().ToLower();

        var key = CacheKey.DungeonMap(normalizedSearch);
        if (cache.TryGetValue<List<string>>(key, out var maps))
        {
            return new GetDungeonMapsResult(maps!);
        }


        maps = await dataContext.DungeonMaps
            .Where(x => EF.Functions.Like(x.Name, $"{normalizedSearch}%"))
            .Take(batchSize)
            .Select(x => x.Name)
            .ToListAsync(cancellationToken);

        if (maps.Count > 0 && !string.IsNullOrWhiteSpace(normalizedSearch))
        {
            cache.Set(key, maps, new MemoryCacheEntryOptions()
            {
                Size = maps.Count,
                SlidingExpiration = TimeSpan.FromMinutes(5),
            });
        }

        return new GetDungeonMapsResult(maps);
    }
}
