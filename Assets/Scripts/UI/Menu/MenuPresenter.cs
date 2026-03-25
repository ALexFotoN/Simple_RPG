using UnityEngine.SceneManagement;

public class MenuPresenter
{
    private readonly IMenuView _view;
    private readonly IMenuModel _model;

    public MenuPresenter(IMenuView view, IMenuModel model)
    {
        _view = view;
        _model = model;

        _view.OnPlayClicked += StartGame;
        _view.OnExitClicked += ExitGame;

        _view.Show();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(_model.SceneNameForPlay);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}