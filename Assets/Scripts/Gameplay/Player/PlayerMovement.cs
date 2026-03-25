using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private float _groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody _rb;
    private IInputHandler _input;
    private bool _isGrounded;

    private Vector3 _direction;
    private bool _jumpPressed;

    public void Construct(IInputHandler input)
    {
        _input = input;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null) _rb = gameObject.AddComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        if (_input == null) return;
        _direction = _input.Move;
        if(_input.JumpPressed)
            _jumpPressed = true;
        if (_input.JumpUnpressed)
            _jumpPressed = false;
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics.Raycast(_groundCheckPoint.position, Vector3.down, _groundCheckDistance + 0.1f, _groundLayer);
        Vector3 velocity = new Vector3(_direction.x * _moveSpeed, _rb.linearVelocity.y, _direction.z * _moveSpeed);
        _rb.linearVelocity = velocity;

        if (_jumpPressed && _isGrounded)
        {
            _jumpPressed = false;
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
        }
    }
}