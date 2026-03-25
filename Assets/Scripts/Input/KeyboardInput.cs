using UnityEngine;

public class KeyboardInput : IInputHandler
{
    public Vector3 Move => new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    public bool JumpPressed => Input.GetKeyDown(KeyCode.Space);
    public bool JumpUnpressed => Input.GetKeyUp(KeyCode.Space);
    public bool AttackPressed => Input.GetButtonDown("Fire1");
    public bool AbilityPressed => Input.GetKeyDown(KeyCode.E);
}