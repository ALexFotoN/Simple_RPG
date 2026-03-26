using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] 
    private float _moveSpeed = 3f;
    [SerializeField] 
    private float _chaseRange = 10f;
    [SerializeField] 
    private float _attackRange = 1.5f;
    [SerializeField] 
    private float _returnRange = 12f;

    [Header("Attack")]
    [SerializeField] 
    private float _attackCooldown = 1f;
    [SerializeField] 
    private int _damage = 10;

    private Transform _player;

    private Rigidbody _rb;
    private Vector3 _startPosition;
    private EnemyHealth _health;
    private EnemyAnimator _animator;
    private EventBus _eventBus;

    private enum State { Idle, Chase, Attack, Return }
    private State _currentState = State.Idle;

    private float _lastAttackTime;

    public void Construct(Transform playerTransform, EventBus eventBus)
    {
        _player = playerTransform;
        _eventBus = eventBus;

        _rb = GetComponent<Rigidbody>();
        _health = GetComponent<EnemyHealth>();
        if (_health != null) _health.Construct(_eventBus);
        _animator = GetComponent<EnemyAnimator>();
        _animator.Construct(_eventBus);
    }

    private void Awake()
    {
        _startPosition = transform.position;

        _currentState = State.Idle;
    }

    private void Update()
    {
        if (_player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        switch (_currentState)
        {
            case State.Idle:
                if (distanceToPlayer <= _chaseRange)
                    _currentState = State.Chase;
                else if (distanceToPlayer >= _returnRange)
                    _currentState = State.Return;
                break;

            case State.Chase:
                if (distanceToPlayer <= _attackRange)
                    _currentState = State.Attack;
                else if (distanceToPlayer > _chaseRange)
                    _currentState = State.Return;
                break;

            case State.Attack:
                if (distanceToPlayer > _attackRange)
                    _currentState = State.Chase;
                break;

            case State.Return:
                if (Vector3.Distance(transform.position, _startPosition) < 0.5f)
                    _currentState = State.Idle;
                else if (distanceToPlayer <= _chaseRange)
                    _currentState = State.Chase;
                break;
        }

        switch (_currentState)
        {
            case State.Chase:
                MoveTowards(_player.position);
                break;
            case State.Return:
                MoveTowards(_startPosition);
                break;
            case State.Attack:
                Attack();
                break;
            case State.Idle:
                break;
        }
    }

    private void MoveTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        direction.y = 0;
        Vector3 newPosition = transform.position + direction * _moveSpeed * Time.deltaTime;
        _rb.MovePosition(newPosition);
        if (direction != Vector3.zero)
            transform.forward = direction;
    }

    private void Attack()
    {
        if (Time.time >= _lastAttackTime + _attackCooldown)
        {
            PlayerHealth playerHealth = _player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(_damage);

            _lastAttackTime = Time.time;

            _eventBus?.Publish(new EnemyAttackedEvent());
        }
    }
}