using Application.Common;

namespace Application.SaveFiles.Remove;

public sealed record RemoveSaveFileCommand(Guid Id, Guid UserId) : ICommand
{
    public const string Path = "/savefiles/{id:guid}";
}
