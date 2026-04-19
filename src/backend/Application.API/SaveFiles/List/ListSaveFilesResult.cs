using Domain.SaveFiles;

namespace Application.SaveFiles.List;

public sealed record ListSaveFilesResult(IEnumerable<SaveFileDto> SaveFiles);

public sealed record SaveFileDto(Guid Id, string FileName, DateTime UploadedAt, SaveFileState State);
