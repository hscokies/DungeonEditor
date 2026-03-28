using Domain.SaveFiles;

namespace Application.SaveFiles.List;

public sealed record ListSaveFilesResult(IEnumerable<SaveFileDto> SaveFiles);

public sealed record SaveFileDto(Guid Id, DateTime UploadedAt, SaveFileState State);
