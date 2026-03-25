using System;

public interface IMenuView
{
    event Action OnPlayClicked;
    event Action OnExitClicked;

    void Show();
    void Hide();
}