using Application.Common;
using Microsoft.AspNetCore.Http;

namespace Application.SaveFiles.Upload;

public record UploadSaveCommand(Guid UserId, IFormFile SaveFile) : ICommand
{
    public const string Path = "/savefiles";
}
