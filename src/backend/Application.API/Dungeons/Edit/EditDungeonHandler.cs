using Application.Common;
using Application.Common.Errors;
using Domain.Common;
using Domain.SaveFiles;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Application.Dungeons.Edit;

public sealed class EditDungeonHandler(IDataContext dataContext) : ICommandHandler<EditDungeonCommand>
{
    private static readonly DungeonsMapper Mapper = new();

    public async Task<Result> Handle(EditDungeonCommand command, CancellationToken cancellationToken)
    {
        if (command.AuthorCharacter.ByteCount() > ByteSize.Author ||
            command.AuthorPSN.ByteCount() > ByteSize.Author)
        {
            return DungeonErrors.AuthorNameTooLong;
        }


        var result = await dataContext.Dungeons
            .Where(x => x.Id == command.Id && x.SaveFile!.UserId == command.UserId)
            .Select(x => new
            {
                Dungeon = x,
                State = x.SaveFile!.State,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (result is null)
        {
            return DungeonErrors.NotFound;
        }

        if (result.State is SaveFileState.Processing)
        {
            return DungeonErrors.ProcessingSaveFile;
        }

        Mapper.Map(command, result.Dungeon);
        await dataContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
