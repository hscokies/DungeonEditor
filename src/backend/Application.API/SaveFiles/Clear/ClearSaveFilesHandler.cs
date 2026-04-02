using Application.Common;
using Domain.Common;
using Domain.SaveFiles;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace Application.SaveFiles.Clear;

public sealed class ClearSaveFilesHandler(IDataContext dataContext, IBlobStorage blobStorage) : ICommandHandler<ClearSaveFilesCommand>
{

    public async Task<Result> Handle(ClearSaveFilesCommand command, CancellationToken cancellationToken)
    {
        var query = dataContext.SaveFiles
            .Where(x => x.UserId == command.UserId && x.State != SaveFileState.Processing);


        var paths = await query
            .Select(x => $"{command.UserId}/{x.Id}")
            .ToArrayAsync(cancellationToken);

        await query.ExecuteDeleteAsync(cancellationToken);
        await blobStorage.Delete(paths, cancellationToken);

        return Result.Success();
    }
}
