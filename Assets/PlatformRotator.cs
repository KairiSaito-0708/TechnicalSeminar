using UnityEngine;

public class PlatformRotator : MonoBehaviour
{
    [Header("��]�ݒ�")]
    [Tooltip("��]���鑬�x�ƕ������AX, Y, Z���Ŏw��B")]
    public Vector3 rotationSpeed = new Vector3(0, 30, 0);

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}