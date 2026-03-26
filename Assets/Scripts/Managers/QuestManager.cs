using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
    private List<Quest> _activeQuests = new List<Quest>();
    private EventBus _eventBus;

    public event Action OnQuestsChanged;

    public QuestManager(EventBus eventBus)
    {
        _eventBus = eventBus;
        _eventBus.Subscribe<EnemyKilledEvent>(OnGameEvent);
        _eventBus.Subscribe<ZoneEnteredEvent>(OnGameEvent);
        _eventBus.Subscribe<ItemCollectedEvent>(OnGameEvent);
    }

    public void StartQuest(Quest quest)
    {
        if (!_activeQuests.Contains(quest))
        {
            _activeQuests.Add(quest);
            quest.OnCompleted += () => OnQuestCompleted(quest);
            OnQuestsChanged?.Invoke();
        }
    }

    private void OnGameEvent(GameEvent gameEvent)
    {
        bool anyChanged = false;
        for (int i = 0; i < _activeQuests.Count; i++)
        {
            var quest = _activeQuests[i];
            bool wasCompleted = quest.IsCompleted;
            quest.CheckProgress(gameEvent);
            if (quest == null)
            {
                continue;
            }
            if (!wasCompleted && quest.IsCompleted)
                anyChanged = true;
            else if (!wasCompleted && !quest.IsCompleted)
                anyChanged = true;
        }
        if (anyChanged)
            OnQuestsChanged?.Invoke();
    }

    private void OnQuestCompleted(Quest quest)
    {
        _activeQuests.Remove(quest);
        OnQuestsChanged?.Invoke();
    }

    public void ClearAllQuests()
    {
        _activeQuests = new();
    }

    public List<Quest> GetActiveQuests() => _activeQuests;
}