using Domain.Common;

namespace Application.Common.Errors;

internal static class DungeonErrors
{
    internal static readonly Error NotFound = Error.NotFound("Dungeon.NotFound", "Specified dungeon cannot be found");
    internal static readonly Error ProcessingSaveFile = Error.Problem("Dungeon.ProcessingSaveFile", "Unable to change dungeons while save file is being processed");
    internal static readonly Error AuthorNameTooLong = Error.Problem("Dungeon.AuthorNameTooLong", "Author name should be less or equal to 16 bytes");
}
