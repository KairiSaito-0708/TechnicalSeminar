using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("追従するターゲット")]
    public Transform BluePlayer;
    public Transform RedPlayer;

    [Header("カメラ設定")]
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 0.125f;

    private Transform currentTarget;

    void Start()
    {
        currentTarget = BluePlayer;
    }

    void Update()
    {
        if (Keyboard.current.digit1Key.isPressed)
        {
            currentTarget = BluePlayer;
        }
        else if (Keyboard.current.digit2Key.isPressed)
        {
            currentTarget = RedPlayer;
        }
    }

    void LateUpdate()
    {
        if (currentTarget == null) return;

        Vector3 desiredPosition = currentTarget.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(currentTarget);
    }
}