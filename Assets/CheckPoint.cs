using UnityEngine;
using TMPro;

public class CheckPoint : MonoBehaviour
{
    [Header("UI設定")]
    [Tooltip("状態を表示するTextMeshProのテキスト")]
    public TextMeshProUGUI statusText;

    [Header("リスポーン地点")]
    [Tooltip("このチェックポイントでの青ボールの復活地点")]
    public Transform blueRespawnTransform;
    [Tooltip("このチェックポイントでの赤ボールの復活地点")]
    public Transform redRespawnTransform;

    private bool blueBallOnPlate = false;
    private bool redBallOnPlate = false;

    private void Start()
    {
        if (blueRespawnTransform == null || redRespawnTransform == null)
        {
            Debug.LogError("チェックポイントのリスポーン地点が設定されていません！", this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        BallIdentifier ball = other.GetComponent<BallIdentifier>();
        if (ball == null) return;

        if (ball.color == BallIdentifier.BallColor.Blue)
        {
            blueBallOnPlate = true;
        }
        else if (ball.color == BallIdentifier.BallColor.Red)
        {
            redBallOnPlate = true;
        }
        UpdateStatus();
    }

    private void OnTriggerExit(Collider other)
    {
        BallIdentifier ball = other.GetComponent<BallIdentifier>();
        if (ball == null) return;

        if (ball.color == BallIdentifier.BallColor.Blue)
        {
            blueBallOnPlate = false;
        }
        else if (ball.color == BallIdentifier.BallColor.Red)
        {
            redBallOnPlate = false;
        }
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        if (blueBallOnPlate && redBallOnPlate)
        {
            statusText.text = "チェックポイント更新";
            GameManager.instance.blueLastCheckPointPosition = blueRespawnTransform.position;
            GameManager.instance.redLastCheckPointPosition = redRespawnTransform.position;
        }
        else if (blueBallOnPlate || redBallOnPlate)
        {
            statusText.text = "1/2";
        }
        else
        {
            statusText.text = "";
        }
    }
}