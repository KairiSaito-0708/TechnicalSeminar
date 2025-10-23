using UnityEngine;

public class SwitchPlate : MonoBehaviour
{
    [Tooltip("このスイッチが反応するボールの色を選択します。")]
    public BallIdentifier.BallColor targetColor;

    public bool IsPressed { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        BallIdentifier ball = other.GetComponent<BallIdentifier>();
        if (ball != null && ball.color == targetColor)
        {
            IsPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BallIdentifier ball = other.GetComponent<BallIdentifier>();
        if (ball != null && ball.color == targetColor)
        {
            IsPressed = false;
        }
    }
}