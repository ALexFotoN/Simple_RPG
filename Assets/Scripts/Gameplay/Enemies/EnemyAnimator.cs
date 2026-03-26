using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private EventBus _eventBus;

    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    public void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;

        _eventBus.Subscribe<EnemyAttackedEvent>(OnAttack);
    }

    private void OnAttack(EnemyAttackedEvent evt)
    {
        if (_animator != null)
            _animator.SetTrigger(AttackTrigger);
    }
}
