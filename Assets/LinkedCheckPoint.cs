using UnityEngine;
using TMPro;
using System.Collections;
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
    public AudioClip platePressedSound;
    public AudioClip checkpointUpdateSound;

    [Header("表示設定")]
    public float textDisplayDuration = 2.0f;

    private AudioSource audioSource;
    private bool isPartiallyPressed = false;
    private bool isShowingUpdateText = false;
    private bool isActivated = false;

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
        if (isActivated || isShowingUpdateText)
        {
            return;
        }

        int pressedCount = 0;
        foreach(SwitchPlate plate in requiredPlates)
        {
            if (plate.IsPressed)
            {
                pressedCount++;
            }
        }

        if (pressedCount == requiredPlates.Count && requiredPlates.Count > 0)
        {
            GameManager.instance.blueLastCheckPointPosition = blueRespawnTransform.position;
            GameManager.instance.redLastCheckPointPosition = redRespawnTransform.position;
            
            if (audioSource != null && checkpointUpdateSound != null)
            {
                audioSource.PlayOneShot(checkpointUpdateSound);
            }
            
            StartCoroutine(ShowUpdateText());
            
            isActivated = true;
            isPartiallyPressed = false;
        }
        else if (pressedCount > 0)
        {
            statusText.text = "Check Point 1/2";
            
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

    private IEnumerator ShowUpdateText()
    {
        isShowingUpdateText = true;
        statusText.text = "CheckPoint Complete!";
        
        yield return new WaitForSeconds(textDisplayDuration);
        
        statusText.text = "";
        isShowingUpdateText = false;
    }
}