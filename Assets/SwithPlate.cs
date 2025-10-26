using UnityEngine;

public class SwitchPlate : MonoBehaviour
{
    [Tooltip("このスイッチが反応するボールの色を選択する")]
    public BallIdentifier.BallColor targetColor;

    [Header("サウンド")]
    [Tooltip("スイッチを踏んだ時に鳴らす音")]
    public AudioClip switchPressSound;

    private AudioSource audioSource;

    public bool IsPressed { get; private set; }
    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        BallIdentifier ball = other.GetComponent<BallIdentifier>();
        if (ball != null && ball.color == targetColor)
        {
            bool wasPressed = IsPressed; 
            IsPressed = true;

            if (!wasPressed && audioSource != null && switchPressSound != null)
            {
                audioSource.PlayOneShot(switchPressSound);
            }
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