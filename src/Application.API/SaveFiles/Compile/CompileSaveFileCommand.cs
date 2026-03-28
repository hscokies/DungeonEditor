using Application.Common;

namespace Application.SaveFiles.Compile;

public record CompileSaveFileCommand(Guid Id, Guid UserId) : ICommand
{
    public const string Path = "/savefiles/{id:guid}";
}
