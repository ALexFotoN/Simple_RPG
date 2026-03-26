using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] 
    private float _smoothSpeed = 5f;

    private Vector3 offset;
    private Transform _target;
    private Vector3 _velocity = Vector3.zero;

    public void Construct(Transform playerTransform)
    {
        offset = transform.position;
        _target = playerTransform;
    }

    private void Update()
    {
        if (_target == null) return;

        Vector3 desiredPosition = _target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, 1f / _smoothSpeed);
    }
}