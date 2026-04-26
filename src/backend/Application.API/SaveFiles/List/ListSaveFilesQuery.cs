using Application.Common;

namespace Application.SaveFiles.List;

public sealed record ListSaveFilesQuery(Guid UserId, string Search, ushort Offset, ushort Limit) : IQuery<ListSaveFilesResult>
{
    public const string Path = "/savefiles";
}
