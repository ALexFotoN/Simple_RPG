using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private IInputHandler _input;

    public void Construct(IInputHandler input)
    {
        _input = input;
    }

    private void Update()
    {
        if (_input == null) return;

        Vector3 mouseWorldPos = _input.MouseWorldPosition;
        if (mouseWorldPos != Vector3.zero)
        {
            Vector3 direction = (mouseWorldPos - transform.position).normalized;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation;
            }
        }
    }
}