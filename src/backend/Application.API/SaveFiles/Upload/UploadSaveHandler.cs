using Application.Common;
using Application.Common.Errors;
using Domain.Common;
using Domain.SaveFiles;
using Domain.Users;
using Infrastructure.Messaging.SaveFiles;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Storage;
using MassTransit;
using MimeMapping;

namespace Application.SaveFiles.Upload;

public sealed class UploadSaveHandler(IBalanceService balanceService, IBlobStorage blobStorage, IDataContext dataContext, IPublishEndpoint endpoint) : ICommandHandler<UploadSaveCommand, Guid>
{
    private const int MaxSizeBytes = 5 * 1024 * 1024; // 5 MB

    public async Task<Result<Guid>> Handle(UploadSaveCommand command, CancellationToken cancellationToken)
    {
        if (command.SaveFile.Length > MaxSizeBytes)
        {
            return SaveFileErrors.TooLarge;
        }

        var chargeResult = await balanceService.Charge(CommonTransactions.SaveFileUpload(command.UserId), cancellationToken);
        if (chargeResult.IsFailure)
        {
            return chargeResult.Error;
        }

        var saveFile = new SaveFile
        {
            UserId = command.UserId,
        };
        dataContext.SaveFiles.Add(saveFile);

        await using var stream = command.SaveFile.OpenReadStream();
        await blobStorage.Write(stream, KnownMimeTypes.Bin, saveFile.Path, cancellationToken);
        await dataContext.SaveChangesAsync(cancellationToken);

        await endpoint.Publish(new ParseSaveFile
        {
            Id = saveFile.Id,
        }, cancellationToken);

        return saveFile.Id;
    }
}
