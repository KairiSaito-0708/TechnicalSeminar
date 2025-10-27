using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 
public class ObstacleBallRespawner : MonoBehaviour
{
    [Header("リスポーン設定")]
    [Tooltip("ボールが復活する場所（坂の上の空オブジェクトなど）")]
    public Transform spawnPoint;

    [Tooltip("このY座標より下に落ちたらリスポーンする")]
    public float respawnHeight;

    // 内部で使う変数
    private Rigidbody rb;
    private Vector3 initialSpawnPosition; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (spawnPoint != null)
        {
            initialSpawnPosition = spawnPoint.position;
        }
        else
        {
            Debug.LogError("スポーン地点が設定されていません！", this.gameObject);
            initialSpawnPosition = transform.position; 
        }

        Respawn();
    }

    void Update()
    {
        if (transform.position.y < respawnHeight)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = initialSpawnPosition;
    }
}