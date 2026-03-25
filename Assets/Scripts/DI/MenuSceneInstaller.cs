using UnityEngine;

public class MenuSceneInstaller : MonoBehaviour, ISceneDependencyReceiver
{
    [SerializeField] 
    private MenuView _menuViewPrefab;

    private LevelManager _levelManager;

    private void Awake()
    {
        var bootstrapper = FindFirstObjectByType<GameBootstrapper>();
        if (bootstrapper == null)
        {
            Debug.LogError("GameBootstrapper not found!");
            return;
        }
        bootstrapper.RegisterSceneDependencies(this);
    }

    public void Inject(EventBus eventBus, LevelManager levelManager)
    {
        _levelManager = levelManager;

        InitializeScene();
    }

    private void InitializeScene()
    {
        var menuView = Instantiate(_menuViewPrefab);
        IMenuModel model = new MenuModel(_levelManager.CurentSceneName);
        new MenuPresenter(menuView, model);
    }
}
