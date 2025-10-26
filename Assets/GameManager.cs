using UnityEngine;
using TMPro;

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

    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;

            int minutes = (int)(elapsedTime / 60);
            int seconds = (int)(elapsedTime % 60);
            int milliseconds = (int)((elapsedTime - (minutes * 60) - seconds) * 100);

            if (timerText != null)
            {
                timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
            }
        }
    }
}