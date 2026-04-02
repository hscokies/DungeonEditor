using Application.Common;

namespace Application.SaveFiles.Clear;

public sealed record ClearSaveFilesCommand(Guid UserId) : ICommand
{
    public const string Path = "/savefiles";
}
