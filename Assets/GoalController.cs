using UnityEngine;
using TMPro;

public class GoalController : MonoBehaviour
{
    [Header("UIê›íË")]
    public TextMeshProUGUI goalText;
    public TextMeshProUGUI scoreText;

    [Header("ÉTÉEÉìÉh")]
    public AudioClip goalSound;

    private AudioSource audioSource;
    private bool isBlueFinished = false;
    private bool isRedFinished = false;
    private bool isGoalActivated = false;

    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();

        if (goalText != null) goalText.text = "";
        if (scoreText != null) scoreText.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGoalActivated) return;
        BallIdentifier ball = other.GetComponent<BallIdentifier>();
        if (ball == null) return;

        if (ball.color == BallIdentifier.BallColor.Blue)
        {
            isBlueFinished = true;
        }
        else if (ball.color == BallIdentifier.BallColor.Red)
        {
            isRedFinished = true;
        }

        if (isBlueFinished && isRedFinished)
        {
            ActivateGoal();
        }
    }

    private void ActivateGoal()
    {
        isGoalActivated = true;

        if (GameManager.instance != null)
        {
            GameManager.instance.isTimerRunning = false;
        }

        if (audioSource != null && goalSound != null)
        {
            audioSource.PlayOneShot(goalSound);
        }

        if (goalText != null)
        {
            goalText.text = "Goal!";
        }

        if (scoreText != null && GameManager.instance != null)
        {
            float finalTime = GameManager.instance.GetCurrentTime();
            scoreText.text = "Score: " + GameManager.instance.FormatTime(finalTime);
        }
    }
}