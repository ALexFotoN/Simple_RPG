public static class QuestFactory
{
    public static Quest CreateKillQuest(string title, string desc, int kills) => new KillQuest(title, desc, kills);
    public static Quest CreateReachPointQuest(string title, string desc, string zoneId) => new ReachPointQuest(title, desc, zoneId);
    public static Quest CreateCollectQuest(string title, string desc, string itemId, int amount) => new CollectQuest(title, desc, itemId, amount);
}