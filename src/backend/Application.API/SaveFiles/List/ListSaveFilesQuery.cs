using Application.Common;

namespace Application.SaveFiles.List;

public sealed record ListSaveFilesQuery(Guid UserId, uint Offset, ushort Limit) : IQuery<ListSaveFilesResult>
{
    public const string Path = "/savefiles";
}
