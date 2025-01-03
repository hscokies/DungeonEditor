namespace DungeonEditor.Models;

public sealed class GemPool
{
    public Dictionary<string, double> Primaries = new();
    public Dictionary<string, double> Secondaries = new();
}