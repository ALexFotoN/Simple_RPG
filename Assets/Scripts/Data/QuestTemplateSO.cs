using UnityEngine;

[CreateAssetMenu(fileName = "QuestTemplate", menuName = "Configs/QuestTemplate")]
public class QuestTemplateSO : ScriptableObject
{
    public enum QuestType
    {
        Kill = 0,
        ReachPoint = 1,
        CollectQuest = 2,
    }

    [SerializeField]
    private QuestType _type;
    public QuestType Type => _type;
    [SerializeField]
    private string _title;
    public string Title => _title;
    [SerializeField]
    private string _description;
    public string Description => _description;
    [SerializeField]
    private int _targetCount;
    public int TargetCount => _targetCount;
    [SerializeField]
    private string _targetId;
    public string TargetId => _targetId;
}
