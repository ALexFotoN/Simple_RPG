using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private EventBus _eventBus;

    public void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void OnTriggerEnter(Collider other)
    {
        _eventBus.Publish(new LevelCompletedEvent());
    }
}