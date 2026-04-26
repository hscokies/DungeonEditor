using Application.Common;
using Common.Data;
using Domain.Common;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Application.DungeonMaps.Get;

public sealed class GetDungeonMapsHandler(IReadOnlyDataContext dataContext, IMemoryCache cache) : IQueryHandler<GetDungeonMapsQuery, GetDungeonMapsResult>
{

    public async Task<Result<GetDungeonMapsResult>> Handle(GetDungeonMapsQuery query, CancellationToken cancellationToken)
    {
        List<string> maps;


        var normalizedSearch = query.Search?.Trim().ToLower();
        if (string.IsNullOrWhiteSpace(query.Search))
        {
            maps = await dataContext.DungeonMaps
                .OrderBy(x => x.Id)
                .Skip(query.Offset)
                .Take(query.Limit)
                .Select(x => x.Name)
                .ToListAsync(cancellationToken);

            return new GetDungeonMapsResult(maps);
        }

        var key = CacheKey.DungeonMap(normalizedSearch!);
        if (cache.TryGetValue(key, out maps!))
        {
            return new GetDungeonMapsResult(maps);
        }

        maps = await dataContext.DungeonMaps
            .Where(x => EF.Functions.Like(x.Name, $"{normalizedSearch}%"))
            .OrderBy(x => x.Id)
            .Skip(query.Offset)
            .Take(query.Limit)
            .Select(x => x.Name)
            .ToListAsync(cancellationToken);

        if (maps.Count > 0)
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
