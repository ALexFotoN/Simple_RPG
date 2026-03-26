using UnityEngine;

public abstract class CollectibleItem : MonoBehaviour
{
    [SerializeField] protected string itemId;
    [SerializeField] protected int quantity = 1;

    protected EventBus _eventBus;

    public void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnCollect();
        Destroy(gameObject);
    }

    protected virtual void OnCollect()
    {
        _eventBus?.Publish(new ItemCollectedEvent(itemId, this));
    }

    public string GetItemId() => itemId;
    public int GetQuantity() => quantity;
}