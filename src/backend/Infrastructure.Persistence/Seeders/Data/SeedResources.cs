using Common.Data;

namespace Infrastructure.Persistence.Seeders.Data;

internal static class SeedResources
{
    public static StreamReader GetDungeonMaps()
    {
        var type = typeof(SeedResources);
        var resourceKey = $"{type.FullName}.KnownDungeonMaps.txt";

        var stream = Resources.GetResource(type.Assembly, resourceKey);
        return new StreamReader(stream);
    }
}
