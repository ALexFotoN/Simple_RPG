using UnityEngine;

public class HUDPresenter
{
    private IHUDView _view;
    private HUDModel _model;
    private EventBus _eventBus;

    public HUDPresenter(IHUDView view, HUDModel model, EventBus eventBus, LevelManager levelManager)
    {
        _view = view;
        _model = model;
        _eventBus = eventBus;

        _view.OnMenuButtonClicked += OpenMenu;

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
}