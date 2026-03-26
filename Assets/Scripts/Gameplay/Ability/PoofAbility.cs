using System.Collections;
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
        StartCoroutine(LifeTime());
    }

    private IEnumerator LifeTime()
    {
        _collider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _collider.enabled = false;
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var direction = other.transform.position - transform.position;
        direction += Vector3.up;
        other.attachedRigidbody.AddForce(direction.normalized * _force, ForceMode.Impulse);
    }
}
