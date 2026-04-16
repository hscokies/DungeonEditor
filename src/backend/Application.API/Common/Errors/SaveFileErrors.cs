using Domain.Common;

namespace Application.Common.Errors;

internal static class SaveFileErrors
{
    internal static readonly Error NotFound = Error.NotFound("SaveFile.NotFound", "Unable to locate specified save file.");
    internal static readonly Error RemoveProcessing = Error.Validation("SaveFile.RemoveProcessing", "Unable to delete files that are currently being processed.");
    internal static readonly Error CompileProcessing = Error.Validation("SaveFile.CompileProcessing", "Unable to compile files that are currently being processed.");
    internal static readonly Error TooLarge = Error.Validation("SaveFile.TooLarge", "File too large.");
}
