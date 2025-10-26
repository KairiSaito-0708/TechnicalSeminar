using UnityEngine;

public class PlatformRotator : MonoBehaviour
{
    [Header("回転設定")]
    [Tooltip("回転する速度と方向を、X, Y, Z軸で指定。")]
    public Vector3 rotationSpeed = new Vector3(0, 30, 0);

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}