namespace DungeonEditor.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class NameAttribute(params string[] names) : Attribute
{
    private HashSet<string> _names = names.ToHashSet();

    public IEnumerable<string> Get() => _names;
}