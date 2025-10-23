using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("初期スポーン地点")]
    public Transform blueInitialSpawnPoint;
    public Transform redInitialSpawnPoint;

    public Vector3 blueLastCheckPointPosition;
    public Vector3 redLastCheckPointPosition;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (blueInitialSpawnPoint != null)
        {
            blueLastCheckPointPosition = blueInitialSpawnPoint.position;
        }
        else
        {
            Debug.LogError("青ボールの初期スポーン地点が設定されていません！");
        }

        if (redInitialSpawnPoint != null)
        {
            redLastCheckPointPosition = redInitialSpawnPoint.position;
        }
        else
        {
            Debug.LogError("赤ボールの初期スポーン地点が設定されていません！");
        }
    }
}