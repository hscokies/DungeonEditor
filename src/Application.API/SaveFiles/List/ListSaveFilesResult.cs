using Domain.SaveFiles;

namespace Application.SaveFiles.List;

public sealed record ListSaveFilesResult(SaveFileDto[] SavedFiles);

public sealed record SaveFileDto(Guid Id, DateTime UploadedAt, SaveFileState State);
