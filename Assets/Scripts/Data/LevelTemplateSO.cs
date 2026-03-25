using UnityEngine;

[CreateAssetMenu(fileName = "LevelTemplate", menuName = "Configs/LevelTemplate")]
public class LevelTemplateSO : ScriptableObject
{
    [SerializeField]
    private string _sceneName;
    public string SceneName => _sceneName;
    [SerializeField]
    private EnemyTemplateSO[] _enemies;
    public EnemyTemplateSO[] Enemies => _enemies;
    [SerializeField]
    private QuestTemplateSO[] _quests;
    public QuestTemplateSO[] Quests => _quests;

    //items (optional)
}
