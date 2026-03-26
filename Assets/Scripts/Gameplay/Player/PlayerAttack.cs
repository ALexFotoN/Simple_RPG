using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackCooldown = 0.5f;

    private IInputHandler _input;
    private EventBus _eventBus;
    private float _lastAttackTime;

    public void Construct(IInputHandler input, EventBus eventBus)
    {
        _input = input;
        _eventBus = eventBus;
    }

    private void Update()
    {
        //if (_input.AttackPressed && Time.time >= _lastAttackTime + _attackCooldown)
        //{
        //    PerformAttack();
        //    _lastAttackTime = Time.time;
        //}
    }

    private void PerformAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(_attackPoint.position, _attackRange, _enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            var health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
                health.TakeDamage(_damage);
        }
        _eventBus?.Publish(new PlayerAttackedEvent());
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}