using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private EventBus _eventBus;

    private static readonly int AttackTrigger = Animator.StringToHash("Attack");

    public void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;

        _eventBus.Subscribe<PlayerAttackedEvent>(OnAttack);
    }

    private void OnAttack(PlayerAttackedEvent evt)
    {
        if (_animator != null)
            _animator.SetTrigger(AttackTrigger);
    }
}