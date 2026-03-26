public abstract class GameEvent { }

public class EnemyKilledEvent : GameEvent
{
    public EnemyHealth Enemy { get; }
    public EnemyKilledEvent(EnemyHealth enemy) => Enemy = enemy;
}

public class ZoneEnteredEvent : GameEvent
{
    public string ZoneId { get; }
    public ZoneEnteredEvent(string zoneId) => ZoneId = zoneId;
}
public class ItemCollectedEvent : GameEvent
{
    public string ItemId { get; }
    public CollectibleItem Item { get; }

    public ItemCollectedEvent(string itemId, CollectibleItem item)
    {
        ItemId = itemId;
        Item = item;
    }
}

public class LevelCompletedEvent : GameEvent { }