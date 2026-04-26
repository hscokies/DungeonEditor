using Application.Common;
using Application.Common.Errors;
using Domain.Common;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using MimeMapping;

namespace Application.SaveFiles.Download;

public sealed class DownloadSaveFileHandler(IReadOnlyDataContext dataContext, IBlobStorage storage) : IQueryHandler<DownloadSaveFileQuery, DownloadSaveFileResult>
{
    public async Task<Result<DownloadSaveFileResult>> Handle(DownloadSaveFileQuery query, CancellationToken cancellationToken)
    {
        var saveFile = await dataContext.SaveFiles.FirstOrDefaultAsync(x => x.Id == query.Id && x.UserId == query.UserId, cancellationToken);
        if (saveFile is null)
        {
            return SaveFileErrors.NotFound;
        }

        var stream = await storage.OpenRead(saveFile.Path, cancellationToken);
        return new DownloadSaveFileResult(saveFile.FileName, KnownMimeTypes.Bin, stream);
    }
}
