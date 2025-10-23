using UnityEngine;

[RequireComponent(typeof(BallIdentifier))]
public class PlayerRespawner : MonoBehaviour
{
    [Tooltip("このY座標より下に落ちたらリスポーンする")]
    public float respawnHeight = 50f;

    private Rigidbody rb;
    private BallIdentifier ballIdentifier;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballIdentifier = GetComponent<BallIdentifier>();
        
        if (ballIdentifier.color == BallIdentifier.BallColor.Blue)
        {
            transform.position = GameManager.instance.blueLastCheckPointPosition;
        }
        else if (ballIdentifier.color == BallIdentifier.BallColor.Red)
        {
            transform.position = GameManager.instance.redLastCheckPointPosition;
        }
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

        // 復活時、自分の色に合った最新のチェックポイント位置に戻る
        if (ballIdentifier.color == BallIdentifier.BallColor.Blue)
        {
            transform.position = GameManager.instance.blueLastCheckPointPosition;
        }
        else if (ballIdentifier.color == BallIdentifier.BallColor.Red)
        {
            transform.position = GameManager.instance.redLastCheckPointPosition;
        }
    }
}