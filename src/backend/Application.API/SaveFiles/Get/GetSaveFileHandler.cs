using Application.Common;
using Application.Common.Errors;
using Domain.Common;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.SaveFiles.Get;

public sealed class GetSaveFileHandler(IReadOnlyDataContext dataContext) : IQueryHandler<GetSaveFileQuery, GetSaveFileResult>
{
    public async Task<Result<GetSaveFileResult>> Handle(GetSaveFileQuery query, CancellationToken cancellationToken)
    {
        var saveFile = await dataContext.SaveFiles
            .Where(f => f.Id == query.Id && f.UserId == query.UserId)
            .Select(f => new GetSaveFileResult(f.State, f.Dungeons.Select(d => new DungeonDto(d.Id, d.JoinRequirement)).ToArray()))
            .FirstOrDefaultAsync(cancellationToken);

        if (saveFile is null)
        {
            return SaveFileErrors.NotFound;
        }

        return saveFile;
    }
}
