public class ReachPointQuest : Quest
{
    private string _targetZoneId;

    public ReachPointQuest(string title, string description, string targetZoneId)
    {
        Title = title;
        Description = description;
        _targetZoneId = targetZoneId;
    }

    public override void CheckProgress(GameEvent gameEvent)
    {
        if (IsCompleted) return;
        if (gameEvent is ZoneEnteredEvent zoneEvent && zoneEvent.ZoneId == _targetZoneId)
            Complete();
    }
}