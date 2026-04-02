using Application.Common;
using Application.Common.Errors;
using Domain.Common;
using Domain.SaveFiles;
using Infrastructure.Messaging.SaveFiles;
using Infrastructure.Persistence.Contexts;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Application.SaveFiles.Compile;

public sealed class CompileSaveFileHandler(IReadOnlyDataContext dataContext, IPublishEndpoint endpoint) : ICommandHandler<CompileSaveFileCommand>
{

    public async Task<Result> Handle(CompileSaveFileCommand command, CancellationToken cancellationToken)
    {
        var saveFile = await dataContext.SaveFiles
            .Where(x => x.Id == command.Id && x.UserId == command.UserId)
            .Select(x => new
            {
                x.Id,
                x.State
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (saveFile is null)
        {
            return SaveFileErrors.NotFound;
        }

        if (saveFile.State is SaveFileState.Processing)
        {
            return SaveFileErrors.CompileProcessing;
        }

        await endpoint.Publish(new CompileSaveFile
        {
            Id = command.Id,
        }, cancellationToken);

        return Result.Success();
    }
}
