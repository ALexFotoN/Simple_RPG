using UnityEngine;

public interface IInputHandler
{
    Vector3 Move { get; }
    bool JumpPressed { get; }
    bool JumpUnpressed { get; }
    bool AttackPressed { get; }
    bool AbilityPressed { get; }
    Vector3 MouseWorldPosition { get; }
}