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
        Dictionary<string, IEnumerable<Error>> errors = [];
        if (command.AuthorCharacter.ByteCount() > ByteSize.Author)
        {
            errors.Add(nameof(command.AuthorCharacter), [DungeonErrors.AuthorNameTooLong]);
        }

        if (command.AuthorPSN.ByteCount() > ByteSize.Author)
        {
            errors.Add(nameof(command.AuthorPSN), [DungeonErrors.AuthorNameTooLong]);
        }

        if (errors.Count > 0)
        {
            return new ValidationError(errors);
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
