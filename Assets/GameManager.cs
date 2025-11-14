using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("初期スポーン地点")]
    public Transform blueInitialSpawnPoint;
    public Transform redInitialSpawnPoint;

    public Vector3 blueLastCheckPointPosition;
    public Vector3 redLastCheckPointPosition;

    [Header("タイマー設定")]
    public TextMeshProUGUI timerText;

    private float elapsedTime = 0f;
    public bool isTimerRunning = true;

    [Header("落下メッセージ設定")]
    public TextMeshProUGUI fallText;
    public AudioClip fallSound;
    public float fallMessageDuration = 2.0f;

    private AudioSource audioSource;
    private bool isShowingFallMessage = false;


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

    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;

            if (timerText != null)
            {
                timerText.text = FormatTime(elapsedTime);
            }
        }
    }

    public string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time - (minutes * 60) - seconds) * 100);
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    public float GetCurrentTime()
    {
        return elapsedTime;
    }

    public void TriggerFallMessage(BallIdentifier.BallColor color)
    {
        if (!isShowingFallMessage)
        {
            StartCoroutine(ShowFallMessage(color));
        }
    }

    private IEnumerator ShowFallMessage(BallIdentifier.BallColor color)
    {
        isShowingFallMessage = true;

        if (audioSource != null && fallSound != null)
        {
            audioSource.PlayOneShot(fallSound);
        }

        if (color == BallIdentifier.BallColor.Blue)
        {
            fallText.text = "<color=blue>Blue Ball</color> Fell!";
        }
        else
        {
            fallText.text = "<color=red>Red Ball</color> Fell!";
        }

        yield return new WaitForSeconds(fallMessageDuration);

        fallText.text = "";
        isShowingFallMessage = false;
    }
}