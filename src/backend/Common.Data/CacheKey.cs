namespace Common.Data;

public static class CacheKey
{
    public static string DungeonMap(string query) => $"{nameof(DungeonMap)}_{query}";
}
