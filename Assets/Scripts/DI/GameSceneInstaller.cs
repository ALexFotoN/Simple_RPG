using UnityEngine;

public class GameSceneInstaller : MonoBehaviour, ISceneDependencyReceiver
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private HUDView _hudViewPrefab;

    private EventBus _eventBus;
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
        _eventBus = eventBus;
        _levelManager = levelManager;

        InitializeScene();
    }

    private void InitializeScene()
    {
        HUDView hudView = Instantiate(_hudViewPrefab);
        HUDModel hudModel = new HUDModel();

        new HUDPresenter(hudView, hudModel, _eventBus, _levelManager);

        GameObject playerObj = Instantiate(_playerPrefab, _spawnPoint.position, Quaternion.identity);
        var movement = playerObj.GetComponent<PlayerMovement>();
        var attack = playerObj.GetComponent<PlayerAttack>();
        var health = playerObj.GetComponent<PlayerHealth>();
        var ability = playerObj.GetComponent<PlayerAbility>();

        IInputHandler input = new KeyboardInput();

        movement?.Construct(input);
        attack?.Construct(input, _eventBus);
        health?.Construct(_eventBus);
        ability?.Construct(input, _eventBus);

        SpawnLevelObjects();
        SpawnEnemies(playerObj.transform);
    }

    private void SpawnLevelObjects()
    {
        //LevelTemplate
    }

    private void SpawnEnemies(Transform player)
    {
        LevelTemplateSO levelData = _levelManager.CurentLevel;
        foreach (var spawnInfo in levelData.Enemies)
        {
            GameObject enemyObj = Instantiate(spawnInfo.Prefab);
            EnemyAI ai = enemyObj.GetComponent<EnemyAI>();
            if (ai != null)
            {
                ai.Construct(player, _eventBus);
            }
            else
            {
                EnemyHealth health = enemyObj.GetComponent<EnemyHealth>();
                if (health != null) health.Construct(_eventBus);
            }
        }
    }
}
