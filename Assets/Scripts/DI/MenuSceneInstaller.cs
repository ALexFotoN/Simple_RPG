using UnityEngine;

public class MenuSceneInstaller : MonoBehaviour, ISceneDependencyReceiver
{
    [SerializeField] 
    private MenuView menuViewPrefab;

    private LevelManager _levelManager;

    private void Awake()
    {
        //var bootstrapper = FindObjectOfType<GameBootstrapper>();
        var bootstrapper = FindFirstObjectByType<GameBootstrapper>();
        if (bootstrapper == null)
        {
            Debug.LogError("GameBootstrapper not found!");
            return;
        }
        bootstrapper.RegisterSceneDependencies(this);
    }

    public void Inject(LevelManager levelManager)
    {
        _levelManager = levelManager;

        InitializeScene();
    }

    private void InitializeScene()
    {
        var menuView = Instantiate(menuViewPrefab);
        IMenuModel model = new MenuModel(_levelManager.CurentSceneName);
        new MenuPresenter(menuView, model);
    }
}
