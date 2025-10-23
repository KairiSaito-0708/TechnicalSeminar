using UnityEngine;
using UnityEngine.InputSystem;
 
public class RedPlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
 
    void FixedUpdate()
    {
        var direction = Vector3.zero;
        if (Keyboard.current.upArrowKey.isPressed)
        {
            direction += Vector3.forward;
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            direction += Vector3.back;
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            direction += Vector3.left;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            direction += Vector3.right;
        }
        if (Keyboard.current.rightShiftKey.isPressed)
        {
            direction += Vector3.up;
        }
        _rb.AddForce(direction * 7);
    }
}