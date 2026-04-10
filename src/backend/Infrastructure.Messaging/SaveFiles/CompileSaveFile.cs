using Domain.Dungeons;
using Domain.SaveFiles;
using Domain.Users;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Services.Dungeons;
using Infrastructure.Storage;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MimeMapping;
using NLog;

namespace Infrastructure.Messaging.SaveFiles;

public record CompileSaveFile
{
    public Guid Id { get; init; }
}

public class CompileSaveFileConsumer(IDataContext dataContext, IBlobStorage blobStorage, IBalanceService balanceService) : IConsumer<CompileSaveFile>
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public async Task Consume(ConsumeContext<CompileSaveFile> context)
    {
        var id = context.Message.Id;
        var cancellationToken = context.CancellationToken;

        var saveFile = await dataContext.SaveFiles
            .Where(sf => sf.Id == id)
            .Include(sf => sf.Dungeons.OrderBy(d => d.Offset))
            .ThenInclude(d => d.Map)
            .FirstOrDefaultAsync(cancellationToken);

        if (saveFile is null)
        {
            throw new InvalidOperationException($"unable to process {nameof(CompileSaveFile)} command: Save file entity not found");
        }

        saveFile.State = SaveFileState.Processing;
        await dataContext.SaveChangesAsync(cancellationToken);

        try
        {
            var ms = new MemoryStream();
            await blobStorage.CopyTo(saveFile.Path, ms, cancellationToken);

            foreach (var dungeon in saveFile.Dungeons)
            {
                await WriteMapBytes(ms, dungeon, cancellationToken);
                await WriteJoinRequirement(ms, dungeon, cancellationToken);
                WriteEffects(ms, dungeon);
                await WriteAuthor(ms, dungeon, cancellationToken);
            }

            ms.Position = 0;
            await blobStorage.Write(ms, KnownMimeTypes.Bin, saveFile.Path, cancellationToken);
            saveFile.State = SaveFileState.Processed;
        }
        catch (Exception ex)
        {
            saveFile.State = SaveFileState.Error;
            _logger.Error(ex, "Failed to compile save file: {Id}", id);
            throw;
        }
        finally
        {
            await dataContext.SaveChangesAsync(cancellationToken);
        }
    }

    private static async Task WriteMapBytes(MemoryStream ms, Dungeon dungeon, CancellationToken cancellationToken)
    {
        var map = Map.Parse(dungeon.Map);
        byte[] bytes = [map.Area, map.LayoutSeed, map.Instance];

        ms.Position = dungeon.Offset + Offsets.MapOpen + Offsets.Area;
        await ms.WriteAsync(bytes, cancellationToken);

        ms.Position = dungeon.Offset + Offsets.MapClose + Offsets.Area;
        await ms.WriteAsync(bytes, cancellationToken);
    }

    private static async Task WriteJoinRequirement(MemoryStream ms, Dungeon dungeon, CancellationToken cancellationToken)
    {
        var bytes = BitConverter.GetBytes((ushort)dungeon.JoinRequirement);
        if (BitConverter.IsLittleEndian)
        {
            Array.Reverse(bytes);
        }

        ms.Position = dungeon.Offset + Offsets.JoinRequirement;
        await ms.WriteAsync(bytes, cancellationToken);
    }

    private static void WriteEffects(MemoryStream ms, Dungeon dungeon)
    {
        ms.Position = dungeon.Offset + Offsets.Effect1;
        ms.WriteByte(dungeon.Effect1);

        ms.Position = dungeon.Offset + Offsets.Effect2;
        ms.WriteByte(dungeon.Effect2);

        ms.Position = dungeon.Offset + Offsets.Effect3;
        ms.WriteByte(dungeon.Effect3);

        ms.Position = dungeon.Offset + Offsets.Effect4;
        ms.WriteByte(dungeon.Effect4);

        ms.Position = dungeon.Offset + Offsets.Effect5;
        ms.WriteByte(dungeon.Effect5);

        ms.Position = dungeon.Offset + Offsets.Effect6;
        ms.WriteByte(dungeon.Effect6);

        ms.Position = dungeon.Offset + Offsets.Effect7;
        ms.WriteByte(dungeon.Effect7);

        ms.Position = dungeon.Offset + Offsets.Effect8;
        ms.WriteByte(dungeon.Effect8);

        ms.Position = dungeon.Offset + Offsets.Effect9;
        ms.WriteByte(dungeon.Effect9);
    }

    private static async Task WriteAuthor(MemoryStream ms, Dungeon dungeon, CancellationToken cancellationToken)
    {
        ms.Position = dungeon.Offset + Offsets.AuthorPsn;
        await ms.WriteAsync(AuthorEncoder.Encode(dungeon.AuthorPSN), cancellationToken);

        ms.Position = dungeon.Offset + Offsets.AuthorCharacter;
        await ms.WriteAsync(AuthorEncoder.Encode(dungeon.AuthorCharacter), cancellationToken);
    }
}
