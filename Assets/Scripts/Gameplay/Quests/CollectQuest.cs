public class CollectQuest : Quest
{
    private string _requiredItemId;
    private int _requiredAmount;
    private int _currentAmount;

    public CollectQuest(string title, string description, string requiredItemId, int requiredAmount)
    {
        Title = title;
        Description = description;
        _requiredItemId = requiredItemId;
        _requiredAmount = requiredAmount;
        _currentAmount = 0;
    }

    public override void CheckProgress(GameEvent gameEvent)
    {
        if (IsCompleted) return;
        if (gameEvent is ItemCollectedEvent itemEvent && itemEvent.ItemId == _requiredItemId)
        {
            _currentAmount += itemEvent.Item.GetQuantity();
            if (_currentAmount >= _requiredAmount)
                Complete();
        }
    }

    public string GetProgressText() => $"{_currentAmount}/{_requiredAmount}";
}