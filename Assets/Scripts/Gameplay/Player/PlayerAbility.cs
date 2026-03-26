using System.Collections;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] 
    private float _cooldown = 3f;
    [SerializeField] 
    private GameObject _abilityPrefab;
    [SerializeField] 
    private float _abilityRange = 5f;

    private IInputHandler _input;
    private EventBus _eventBus;
    private float _leftTime;

    private bool _canUse = true;

    public void Construct(IInputHandler input, EventBus eventBus)
    {
        _input = input;
        _eventBus = eventBus;
    }

    private void Update()
    {
        if (_input.AbilityPressed && _canUse)
        {
            UseAbility();
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        _canUse = false;
        _leftTime = _cooldown;
        while (_leftTime > 0)
        {
            _leftTime -= Time.deltaTime;
            if(_leftTime < 0)
            {
                _leftTime = 0;
            }
            _eventBus?.Publish(new PlayerAbilityCooldownEvent(_leftTime / _cooldown));
            yield return null;
        }
        _canUse = true;
    }

    private void UseAbility()
    {
        Instantiate(_abilityPrefab, transform.position, Quaternion.identity);

        _eventBus?.Publish(new PlayerAbilityUsedEvent());
    }
}