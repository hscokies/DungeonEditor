using Domain.Common;

namespace Application.Common.Errors;

internal static class SaveFileErrors
{
    internal static readonly Error NotFound = new("SaveFile.NotFound", "Unable to locate specified save file", ErrorType.NotFound);
}
