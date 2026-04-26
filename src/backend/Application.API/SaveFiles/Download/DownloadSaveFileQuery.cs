using Application.Common;

namespace Application.SaveFiles.Download;

public sealed record DownloadSaveFileQuery(Guid Id, Guid UserId) : IQuery<DownloadSaveFileResult>
{
    public const string Path = "/savefiles/{id:guid}/download";
}
