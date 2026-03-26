using UnityEngine;

public class KeyboardInput : IInputHandler
{
    private Camera _camera;

    public KeyboardInput()
    {
        _camera = Camera.main;
    }

    public Vector3 Move => new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    public bool JumpPressed => Input.GetKeyDown(KeyCode.Space);
    public bool JumpUnpressed => Input.GetKeyUp(KeyCode.Space);
    public bool AttackPressed => Input.GetButtonDown("Fire1");
    public bool AbilityPressed => Input.GetKeyDown(KeyCode.E);

    public Vector3 MouseWorldPosition
    {
        get
        {
            if (_camera == null) return Vector3.forward;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            if (groundPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                return hitPoint;
            }
            return Vector3.zero;
        }
    }
}