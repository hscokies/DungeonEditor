namespace Application.SaveFiles.Download;

public sealed record DownloadSaveFileResult(string Filename, string ContentType, Stream Stream);
