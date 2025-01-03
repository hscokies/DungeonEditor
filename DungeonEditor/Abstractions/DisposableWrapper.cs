namespace DungeonEditor.Interfaces;

public abstract class DisposableWrapper : IDisposable, IAsyncDisposable
{
    protected bool _disposed { get; set; }

    protected  abstract void Dispose(bool disposing);

    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public virtual ValueTask DisposeAsync()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
        return new ValueTask();
    }
}