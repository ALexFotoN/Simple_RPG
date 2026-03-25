public class PlayerHealthChangedEvent
{
    public int CurrentHealth;
    public int MaxHealth;
    public PlayerHealthChangedEvent(int current, int max)
    {
        CurrentHealth = current;
        MaxHealth = max;
    }
}

public class PlayerDiedEvent { }

public class PlayerAttackedEvent { }

public class PlayerAbilityUsedEvent { }

public class PlayerAbilityCooldownEvent
{
    public float Remaining;
    public PlayerAbilityCooldownEvent(float remaining) => Remaining = remaining;
}