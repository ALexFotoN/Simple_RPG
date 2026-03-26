using System;

public interface IHUDView
{
    void UpdateHealth(int current, int max);
    void UpdateAbilityCooldown(float remaining);
    void UpdateQuests(string questsText);
    void Show();
    void Hide();
}