using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBootstrapper : MonoBehaviour
{
    public EventBus EventBus { get; private set; }
    public LevelManager LevelManager { get; private set; }

    private void Awake()
    {
        if (FindObjectsOfType<GameBootstrapper>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        EventBus = new EventBus();
        LevelManager = new LevelManager();
    }

    public void RegisterSceneDependencies(ISceneDependencyReceiver receiver)
    {
        receiver.Inject(EventBus, LevelManager);
    }
    private void Start()
    {
        SceneManager.LoadScene("1_Menu");
    }
}
