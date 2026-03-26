using UnityEngine;

public class PoofAbility : MonoBehaviour
{
    [SerializeField]
    private float _force;
    [SerializeField]
    private Collider _collider;
    [SerializeField]
    private float _lifeTime;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var direction = other.transform.position - transform.position;
        direction += Vector3.up;
        other.attachedRigidbody.AddForce(direction.normalized * _force, ForceMode.Impulse);
        _collider.enabled = false;
    }
}
