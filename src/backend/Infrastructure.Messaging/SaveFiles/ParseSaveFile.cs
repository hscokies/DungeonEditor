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
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public async Task Consume(ConsumeContext<ParseSaveFile> context)
    {
        var cancellationToken = context.CancellationToken;
        var saveFileId = context.Message.Id;

        var saveFile = await dataContext.SaveFiles
            .Include(x => x.Dungeons)
            .FirstOrDefaultAsync(sf => sf.Id == saveFileId, cancellationToken);

        if (saveFile is null)
        {
            throw new InvalidOperationException($"unable to process {nameof(ParseSaveFile)} command: Save file entity not found");
        }

        saveFile.State = SaveFileState.Processing;
        await dataContext.SaveChangesAsync(cancellationToken);

        try
        {
            await using var saveFileStream = await blobStorage.OpenRead(saveFile.Path, cancellationToken);

            await foreach (var dungeon in DungeonLocator.GetDungeonOffsets(saveFileStream, cancellationToken))
            {
                saveFile.Dungeons.Add(dungeon);
            }

            if (saveFile.Dungeons.Count is 0)
            {
                saveFile.State = SaveFileState.Failed;
                await balanceService.Deposit(CommonTransactions.SaveFileRefund(saveFile.UserId), cancellationToken);
                await blobStorage.Delete(saveFile.Path, cancellationToken);
            }
            else
            {
                saveFile.State = SaveFileState.Processed;
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to process parse save file: {Id}", saveFileId);
            saveFile.State = SaveFileState.Error;

            throw;
        }
        finally
        {
            await dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
