using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private float _cooldown = 3f;
    [SerializeField] private GameObject _abilityEffect;
    [SerializeField] private float _abilityRange = 5f;

    private IInputHandler _input;
    private EventBus _eventBus;
    private float _lastUseTime;

    public void Construct(IInputHandler input, EventBus eventBus)
    {
        _input = input;
        _eventBus = eventBus;
    }

    private void Update()
    {
        if (_input.AbilityPressed && Time.time >= _lastUseTime + _cooldown)
        {
            UseAbility();
            _lastUseTime = Time.time;
            _eventBus?.Publish(new PlayerAbilityCooldownEvent(_cooldown));
        }
        else if (Time.time < _lastUseTime + _cooldown)
        {
            float remaining = (_lastUseTime + _cooldown - Time.time) / _cooldown;
            _eventBus?.Publish(new PlayerAbilityCooldownEvent(remaining));
        }
        else
        {
            _eventBus?.Publish(new PlayerAbilityCooldownEvent(0));
        }
    }

    private void UseAbility()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, _abilityRange, LayerMask.GetMask("Enemy"));
        foreach (var enemy in enemies)
        {
            var health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
                health.TakeDamage(20);
        }

        if (_abilityEffect != null)
            Instantiate(_abilityEffect, transform.position, Quaternion.identity);

        _eventBus?.Publish(new PlayerAbilityUsedEvent());
    }
}