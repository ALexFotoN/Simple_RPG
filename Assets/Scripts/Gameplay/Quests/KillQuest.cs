public class KillQuest : Quest
{
    private int _requiredKills;
    private int _currentKills;

    public KillQuest(string title, string description, int requiredKills)
    {
        Title = title;
        Description = description;
        _requiredKills = requiredKills;
        _currentKills = 0;
    }

    public override void CheckProgress(GameEvent gameEvent)
    {
        if (IsCompleted) return;
        if (gameEvent is EnemyKilledEvent)
        {
            _currentKills++;
            if (_currentKills >= _requiredKills)
                Complete();
        }
    }

    public string GetProgressText() => $"{_currentKills}/{_requiredKills}";
}