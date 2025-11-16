using UnityEngine;

public class SwitchPlate : MonoBehaviour
{
    [Tooltip("このスイッチが反応するボールの色を選択します。")]
    public BallIdentifier.BallColor targetColor;

    [Header("サウンド")]
    public AudioClip switchPressSound;
    private AudioSource audioSource;

    public bool IsPressed { get; private set; }
    public bool IsPermanentlyActive { get; set; }

    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        IsPermanentlyActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        BallIdentifier ball = other.GetComponent<BallIdentifier>();
        if (ball != null && ball.color == targetColor)
        {
            bool wasPressed = IsPressed;
            IsPressed = true;

            if (!wasPressed && !IsPermanentlyActive && audioSource != null && switchPressSound != null)
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