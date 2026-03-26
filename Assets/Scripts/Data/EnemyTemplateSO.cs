using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTemplate", menuName = "Configs/EnemyTemplate")]
public class EnemyTemplateSO : ScriptableObject
{
    [SerializeField]
    private GameObject _prefab;
    public GameObject Prefab => _prefab;
}
