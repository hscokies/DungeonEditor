using Domain.SaveFiles;
using Domain.Users;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Services.SaveFiles;
using Infrastructure.Storage;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace Infrastructure.Messaging.SaveFiles;

public record ParseSaveFile
{
    public Guid Id { get; init; }
}

public class ParseSaveFileConsumer(IBlobStorage blobStorage, IDataContext dataContext, IBalanceService balanceService) : IConsumer<ParseSaveFile>
{
    private readonly Logger _logger =  LogManager.GetCurrentClassLogger();
    public async Task Consume(ConsumeContext<ParseSaveFile> context)
    {
        var cancellationToken = context.CancellationToken;
        var saveFileId = context.Message.Id;
        
        var saveFile = await dataContext.SaveFiles
            .Include(x => x.Dungeons)
            .FirstOrDefaultAsync(sf => sf.Id == saveFileId, cancellationToken);

        if (saveFile is null)
        {
            _logger.Warn("Unable to process {EventName} command: Save file entity not found", nameof(ParseSaveFile));
            return;
        }
        
        await using var saveFileStream = await blobStorage.OpenRead(saveFile.Path, cancellationToken);

        await foreach (var dungeon in DungeonLocator.GetDungeonOffsets(saveFileStream, cancellationToken))
        {
            saveFile.Dungeons.Add(dungeon);
        }

        if (saveFile.Dungeons.Count is 0)
        {
            saveFile.State = SaveFileState.Error;
            await balanceService.Deposit(CommonTransactions.SaveFileRefund(saveFile.UserId), cancellationToken);
        }
        else
        {
            saveFile.State = SaveFileState.Processed;
        }

        await dataContext.SaveChangesAsync(cancellationToken);
    }
}

