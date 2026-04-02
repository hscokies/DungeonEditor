using Application.Common;
using Application.Common.Errors;
using Domain.Common;
using Domain.SaveFiles;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;

namespace Application.SaveFiles.Remove;

public sealed class RemoveSaveFileHandler(IDataContext dataContext, IBlobStorage blobStorage) : ICommandHandler<RemoveSaveFileCommand>
{

    public async Task<Result> Handle(RemoveSaveFileCommand command, CancellationToken cancellationToken)
    {
        var saveFile = await dataContext.SaveFiles.FirstOrDefaultAsync(x => x.Id == command.Id && x.UserId == command.UserId, cancellationToken: cancellationToken);
        if (saveFile is null)
        {
            return SaveFileErrors.NotFound;
        }

        if (saveFile.State is SaveFileState.Processing)
        {
            return SaveFileErrors.RemoveProcessing;
        }

        dataContext.SaveFiles.Remove(saveFile);

        if (saveFile.State is SaveFileState.Failed)
        {
            await blobStorage.Delete(saveFile.Path, cancellationToken);
        }

        await dataContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
