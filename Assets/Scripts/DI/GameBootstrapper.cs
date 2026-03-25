using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    private void Awake()
    {
        if (FindObjectsOfType<GameBootstrapper>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // Инициализация сервисов
    }

    public void RegisterSceneDependencies(ISceneDependencyReceiver receiver)
    {
        receiver.Inject();
    }
}
