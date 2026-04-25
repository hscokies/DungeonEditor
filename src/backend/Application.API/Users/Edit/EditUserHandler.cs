using Application.Common;
using Application.Common.Errors;
using Domain.Common;
using Infrastructure.Persistence.Contexts;

namespace Application.Users.Edit;

public sealed class EditUserHandler(IDataContext dataContext) : ICommandHandler<EditUserCommand>
{
    public async Task<Result> Handle(EditUserCommand command, CancellationToken cancellationToken)
    {
        var user = await dataContext.Users.FindAsync([command.Id], cancellationToken);
        if (user is null)
        {
            return UserErrors.NotFound;
        }

        user.Balance = command.Balance;
        await dataContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
