namespace DungeonEditor.Helpers;

public static class Guard
{
    public static void Equals(object expected, object actual, string? message = null)
    {
        if (!expected.Equals(actual))
        {
            throw new InvalidOperationException(message);
        }
    }
    public static void IsTrue(bool result, string? message = null)
    {
        if (!result)
        {
            throw new InvalidOperationException(message);
        }
    }
}