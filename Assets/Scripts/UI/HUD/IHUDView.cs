using System;

public interface IHUDView
{
    event Action OnMenuButtonClicked;
    void UpdateHealth(int current, int max);
    void UpdateAbilityCooldown(float remaining);
    void UpdateQuests(string questsText);
    void Show();
    void Hide();
}