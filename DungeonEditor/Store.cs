using DungeonEditor.Interfaces;

namespace DungeonEditor;

public static class Store
{
    private static Dictionary<string, object> _store = new();

    public static void Remove(string key) => _store.Remove(key); 
    public static bool TrySet(string key, object value) => _store.TryAdd(key, value);
    public static void Set(string key, object value)
    {
        if (_store.TryAdd(key, value)) return;
        _store[key] = value;
    }
    public static bool TryGet<T>(string key, out T result)
    {
        result = default!;
        if (!_store.TryGetValue(key, out var data))
        {
            return false;
        }

        result = (T) data;
        return true;
    }
}