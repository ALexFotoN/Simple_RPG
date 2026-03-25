using UnityEngine;

public class GameSceneInstaller : MonoBehaviour, ISceneDependencyReceiver
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

    public void Inject(LevelManager levelManager)
    {
        InitializeScene();
    }

    private void InitializeScene()
    {
        
    }
}
