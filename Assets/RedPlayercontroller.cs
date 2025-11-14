using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class RedPlayerController : MonoBehaviour
{
    private Rigidbody _rb;

    [Header("操作設定")]
    public float moveSpeed = 15f;

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
    

        _rb.AddForce(direction.normalized * moveSpeed);
    }
}