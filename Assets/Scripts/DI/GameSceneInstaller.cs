using UnityEngine;

public class GameSceneInstaller : MonoBehaviour, ISceneDependencyReceiver
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private HUDView _hudViewPrefab;

    private EventBus _eventBus;
    private LevelManager _levelManager;
    private QuestManager _questManager;

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

    public void Inject(EventBus eventBus, LevelManager levelManager, QuestManager questManager)
    {
        _eventBus = eventBus;
        _levelManager = levelManager;
        _questManager = questManager;

        InitializeScene();
    }

    private void InitializeScene()
    {
        _questManager.ClearAllQuests();

        HUDView hudView = Instantiate(_hudViewPrefab);
        HUDModel hudModel = new HUDModel();

        new HUDPresenter(hudView, hudModel, _eventBus, _levelManager, _questManager);

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
        SetupQuests();
        SpawnItems();

        LevelExit exit = FindFirstObjectByType<LevelExit>();
        if (exit == null)
        {
            Debug.LogWarning("No LevelExit found in scene");
        }
        else
        {
            exit.Construct(_eventBus);
        }
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

    private void SetupQuests()
    {
        LevelTemplateSO levelData = _levelManager.CurentLevel;

        foreach (var quest in levelData.Quests)
        {
            if (quest.Type == QuestTemplateSO.QuestType.Kill)
            {
                _questManager.StartQuest(QuestFactory.CreateKillQuest(quest.Title, quest.Description, quest.TargetCount));
            }
            else if (quest.Type == QuestTemplateSO.QuestType.ReachPoint)
            {
                _questManager.StartQuest(QuestFactory.CreateReachPointQuest(quest.Title, quest.Description, quest.TargetId));
            }
            else if (quest.Type == QuestTemplateSO.QuestType.CollectQuest)
            {
                _questManager.StartQuest(QuestFactory.CreateCollectQuest(quest.Title, quest.Description, quest.TargetId, quest.TargetCount));
            }
        }
    }

    private void SpawnItems()
    {
        LevelTemplateSO levelData = _levelManager.CurentLevel;
        foreach (var spawnInfo in levelData.Items)
        {
            GameObject itemObj = Instantiate(spawnInfo.itemPrefab, spawnInfo.position, Quaternion.identity);
            var collectible = itemObj.GetComponent<CollectibleItem>();
            if (collectible != null)
                collectible.Construct(_eventBus);
        }
    }
}
