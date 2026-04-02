using Application.Common;

namespace Application.SaveFiles.Get;

public sealed record GetSaveFileQuery(Guid UserId, Guid Id) : IQuery<GetSaveFileResult>
{
    public const string Path = "/savefiles/{id:guid}";
}
