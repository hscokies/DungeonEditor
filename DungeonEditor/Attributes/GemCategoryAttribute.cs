using DungeonEditor.Enumerations;

namespace DungeonEditor.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class GemCategoryAttribute(GemCategory category) : Attribute
{
    public GemCategory Get() => category;
}