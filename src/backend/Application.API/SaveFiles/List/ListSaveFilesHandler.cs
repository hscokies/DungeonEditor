using Application.Common;
using Domain.Common;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.SaveFiles.List;

public sealed class ListSaveFilesHandler(IReadOnlyDataContext readOnlyDataContext) : IQueryHandler<ListSaveFilesQuery, ListSaveFilesResult>
{
    public async Task<Result<ListSaveFilesResult>> Handle(ListSaveFilesQuery query, CancellationToken cancellationToken)
    {
        var saveFiles = await readOnlyDataContext.SaveFiles
            .Where(x => x.UserId == query.UserId)
            .Take(query.Limit)
            .Skip(query.Offset)
            .Select(x => new SaveFileDto(x.Id, x.FileName, x.CreatedAt, x.State))
            .ToArrayAsync(cancellationToken);

        return new ListSaveFilesResult(saveFiles);
    }
}
