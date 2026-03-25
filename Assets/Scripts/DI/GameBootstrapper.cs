using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBootstrapper : MonoBehaviour
{
    public LevelManager LevelManager { get; private set; }

    private void Awake()
    {
        if (FindObjectsOfType<GameBootstrapper>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // Инициализация сервисов
        LevelManager = new LevelManager();
    }

    public void RegisterSceneDependencies(ISceneDependencyReceiver receiver)
    {
        receiver.Inject(LevelManager);
    }
    private void Start()
    {
        SceneManager.LoadScene("1_Menu");
    }
}
