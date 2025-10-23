using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // このオブジェクトにはRigidbodyが必須
public class ObstacleBallRespawner : MonoBehaviour
{
    [Header("リスポーン設定")]
    [Tooltip("ボールが復活する場所（坂の上の空オブジェクトなど）")]
    public Transform spawnPoint;

    [Tooltip("このY座標より下に落ちたらリスポーンする")]
    public float respawnHeight;

    // 内部で使う変数
    private Rigidbody rb;
    private Vector3 initialSpawnPosition; // 最初に設定されたスポーン地点

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // spawnPointが設定されていればそこを、なければゲーム開始時の位置を記憶
        if (spawnPoint != null)
        {
            initialSpawnPosition = spawnPoint.position;
        }
        else
        {
            Debug.LogError("スポーン地点が設定されていません！", this.gameObject);
            initialSpawnPosition = transform.position; // 念のため
        }

        // 念のため、ゲーム開始時に一度スポーン地点に移動させておく
        Respawn();
    }

    void Update()
    {
        // もし設定したY座標より下に落ちたら
        if (transform.position.y < respawnHeight)
        {
            Respawn();
        }
    }

    // 復活処理
    void Respawn()
    {
        // 1. ボールの動きを完全に止める
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // 2. 決められたスポーン地点に戻す
        transform.position = initialSpawnPosition;
    }
}