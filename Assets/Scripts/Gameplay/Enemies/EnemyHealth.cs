using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30;
    private int _currentHealth;
    private EventBus _eventBus;

    public void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _eventBus?.Publish(new EnemyKilledEvent(this));
        Destroy(gameObject);
    }
}