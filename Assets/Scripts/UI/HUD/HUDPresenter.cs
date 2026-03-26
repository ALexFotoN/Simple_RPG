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

        _view.OnMenuButtonClicked += OpenMenu;
        _questManager.OnQuestsChanged += UpdateQuestDisplay;

        _eventBus.Subscribe<PlayerHealthChangedEvent>(OnHealthChanged);
        _eventBus.Subscribe<PlayerAbilityUsedEvent>(OnAbilityUsed);
        //Subscribe PlayerAbilityCooldownEvent

        UpdateView();
    }

    private void OnHealthChanged(PlayerHealthChangedEvent evt)
    {
        _model.CurrentHealth = evt.CurrentHealth;
        _model.MaxHealth = evt.MaxHealth;
        UpdateView();
    }

    private void OnAbilityUsed(PlayerAbilityUsedEvent evt)
    {
        // Начать отсчёт кулдауна (в идеале получать реальный кулдаун от PlayerAbility)
        // Для простоты будем обновлять в Update через корутину или метод, но здесь просто пример
        // Рекомендуется использовать событие с временем перезарядки.
        // Упростим: подпишемся на событие и начнём обновление.
    }

    private void UpdateView()
    {
        _view.UpdateHealth(_model.CurrentHealth, _model.MaxHealth);
        // Обновление кулдауна можно вызывать в Update или получать из события
    }

    private void OpenMenu()
    {
        // Открыть меню паузы
    }

    public void UpdateAbilityCooldown(float remaining)
    {
        _view.UpdateAbilityCooldown(remaining);
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
            else if (q is ReachPointQuest) display += "Не выполнено";
            else if (q is CollectQuest cq) display += cq.GetProgressText();
            display += "\n";
        }
        _view.UpdateQuests(display);
    }
}