using Application.Common;
using Application.Common.Errors;
using Domain.Common;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Dungeons.Get;

public sealed class GetDungeonHandler(IReadOnlyDataContext dataContext) : IQueryHandler<GetDungeonQuery, GetDungeonResult>
{

    public async Task<Result<GetDungeonResult>> Handle(GetDungeonQuery query, CancellationToken cancellationToken)
    {
        var dungeon = await dataContext.Dungeons
            .Where(x => x.SaveFile!.UserId == query.UserId && x.Id == query.Id)
            .Include(x => x.Map)
            .Select(x => new GetDungeonResult(
                x.Map!.ToString(), x.JoinRequirement,
                x.Effect1, x.Effect2, x.Effect3,
                x.Effect4, x.Effect5, x.Effect6,
                x.Effect7, x.Effect8, x.Effect9,
                x.AuthorPSN, x.AuthorCharacter))
            .FirstOrDefaultAsync(cancellationToken);

        if (dungeon is null)
        {
            return DungeonErrors.NotFound;
        }

        return dungeon;
    }
}
