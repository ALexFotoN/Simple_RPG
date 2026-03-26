using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] private string zoneId;
    private EventBus _eventBus;

    public void Construct(EventBus eventBus) => _eventBus = eventBus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _eventBus?.Publish(new ZoneEnteredEvent(zoneId));
    }
}