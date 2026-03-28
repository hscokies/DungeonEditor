using Domain.Common;

namespace Application.Common;

public interface ICommand;

public interface ICommand<TResponse> : ICommand;

public interface ICommandHandler;
public interface ICommandHandler<in TCommand> : ICommandHandler
    where TCommand : ICommand
{
    Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
}

public interface ICommandHandler<in TCommand, TResponse> : ICommandHandler
    where TCommand : ICommand<TResponse>
{
    Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}
