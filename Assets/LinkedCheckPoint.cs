using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LinkedCheckPoint : MonoBehaviour
{
    [Header("監視対象")]
    public List<SwitchPlate> requiredPlates;

    [Header("UI設定")]
    public TextMeshProUGUI statusText;

    [Header("リスポーン地点")]
    public Transform blueRespawnTransform;
    public Transform redRespawnTransform;

    [Header("サウンド")]
    [Tooltip("プレートを1つ踏んだ時の音")]
    public AudioClip platePressedSound;
    [Tooltip("チェックポイント更新時の音")]
    public AudioClip checkpointUpdateSound;

    private AudioSource audioSource;
    private bool isActivated = false;
    private bool isPartiallyPressed = false; 

    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("Main CameraにAudioSourceがありません！", this.gameObject);
        }
    }

    void Update()
    {
        if (isActivated) return;

        int pressedCount = 0;
        foreach (SwitchPlate plate in requiredPlates)
        {
            if (plate.IsPressed)
            {
                pressedCount++;
            }
        }

        if (pressedCount == requiredPlates.Count && requiredPlates.Count > 0)
        {
            statusText.text = "CheckPoint!";
            GameManager.instance.blueLastCheckPointPosition = blueRespawnTransform.position;
            GameManager.instance.redLastCheckPointPosition = redRespawnTransform.position;

            if (audioSource != null && checkpointUpdateSound != null)
            {
                audioSource.PlayOneShot(checkpointUpdateSound);
            }

            isActivated = true;
            isPartiallyPressed = false;
        }
        else if (pressedCount > 0)
        {
            statusText.text = pressedCount + "/" + requiredPlates.Count;

            if (!isPartiallyPressed)
            {
                if (audioSource != null && platePressedSound != null)
                {
                    audioSource.PlayOneShot(platePressedSound);
                }
                isPartiallyPressed = true;
            }
        }
        else
        {
            statusText.text = "";
            isPartiallyPressed = false; 
        }
    }
}