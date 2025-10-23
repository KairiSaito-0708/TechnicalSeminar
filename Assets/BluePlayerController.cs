using UnityEngine;
using UnityEngine.InputSystem;

public class BluePlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private AudioSource _audio;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var direction = Vector3.zero;
        if (Keyboard.current.wKey.isPressed)
        {
            direction += Vector3.forward;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            direction += Vector3.back;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            direction += Vector3.left;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            direction += Vector3.right;
        }
        if (Keyboard.current.spaceKey.isPressed)
        {
            direction += Vector3.up;
        }
        _rb.AddForce(direction.normalized * 7); 
    }
}