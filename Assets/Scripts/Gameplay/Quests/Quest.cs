using System;

public abstract class Quest
{
    public string Title { get; protected set; }
    public string Description { get; protected set; }
    public bool IsCompleted { get; protected set; }
    public event Action OnCompleted;

    public abstract void CheckProgress(GameEvent gameEvent);

    protected void Complete()
    {
        IsCompleted = true;
        OnCompleted?.Invoke();
    }
}