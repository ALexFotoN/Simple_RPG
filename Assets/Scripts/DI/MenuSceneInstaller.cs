using UnityEngine;

public class MenuSceneInstaller : MonoBehaviour, ISceneDependencyReceiver
{
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

    public void Inject()
    {
        InitializeScene();
    }

    private void InitializeScene()
    {

    }
}
