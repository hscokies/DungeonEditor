using System.Reflection;

namespace Common.Data;

public static class Resources
{
    public static Stream GetResource(Assembly assembly, string resourceKey)
    {
        var stream = assembly.GetManifestResourceStream(resourceKey);
        return stream ?? throw new FileNotFoundException($"Unable to locate resource: {resourceKey}");
    }
}
