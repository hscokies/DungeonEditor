using Domain.Dungeons;
using Domain.SaveFiles;

namespace Application.SaveFiles.Get;

public record GetSaveFileResult(SaveFileState State, ICollection<DungeonDto> Dungeons);

public sealed record DungeonDto(Guid Id, JoinRequirement JoinRequirement);