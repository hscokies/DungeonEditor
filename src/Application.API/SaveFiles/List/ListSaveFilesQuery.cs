using Application.Common;

namespace Application.SaveFiles.List;

public sealed record ListSaveFilesQuery(Guid UserId) : IQuery<ListSaveFilesResult>
{
    public const string Path = "/savefiles";
}
