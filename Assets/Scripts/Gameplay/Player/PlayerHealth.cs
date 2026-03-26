using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] 
    private int _maxHealth = 100;
    private int _currentHealth;
    private EventBus _eventBus;

    public void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
        _currentHealth = _maxHealth;
        _eventBus?.Publish(new PlayerHealthChangedEvent(_currentHealth, _maxHealth));
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        _currentHealth = Mathf.Max(_currentHealth, 0);
        _eventBus?.Publish(new PlayerHealthChangedEvent(_currentHealth, _maxHealth));

        if (_currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        _eventBus?.Publish(new PlayerDiedEvent());
        gameObject.SetActive(false);
        // Lose
    }

    public void Heal(int amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        _eventBus?.Publish(new PlayerHealthChangedEvent(_currentHealth, _maxHealth));
    }
}