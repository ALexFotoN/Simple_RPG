using UnityEngine;

public class HUDPresenter
{
    private IHUDView _view;
    private HUDModel _model;
    private EventBus _eventBus;
    private QuestManager _questManager;

    public HUDPresenter(IHUDView view, HUDModel model, EventBus eventBus, LevelManager levelManager, QuestManager questManager)
    {
        _view = view;
        _model = model;
        _eventBus = eventBus;
        _questManager = questManager;

        _questManager.OnQuestsChanged += UpdateQuestDisplay;

        _eventBus.Subscribe<PlayerHealthChangedEvent>(OnHealthChanged);
        _eventBus.Subscribe<PlayerAbilityCooldownEvent>(OnAbilityCooldown);

        UpdateView();
    }

    private void OnHealthChanged(PlayerHealthChangedEvent evt)
    {
        _model.CurrentHealth = evt.CurrentHealth;
        _model.MaxHealth = evt.MaxHealth;
        UpdateView();
    }

    private void OnAbilityCooldown(PlayerAbilityCooldownEvent evt)
    {
        _model.AbilityCooldownRemaining = evt.Remaining;
        UpdateAbilityCooldown();
    }

    private void UpdateView()
    {
        _view.UpdateHealth(_model.CurrentHealth, _model.MaxHealth);
    }

    public void UpdateAbilityCooldown()
    {
        _view.UpdateAbilityCooldown(_model.AbilityCooldownRemaining);
    }

    private void UpdateQuestDisplay()
    {
        var activeQuests = _questManager?.GetActiveQuests();
        if (activeQuests == null) return;

        string display = "";
        foreach (var q in activeQuests)
        {
            display += $"{q.Title}: ";
            if (q is KillQuest kq) display += kq.GetProgressText();
            else if (q is CollectQuest cq) display += cq.GetProgressText();
            display += "\n\n";
        }
        _view.UpdateQuests(display);
    }
}