using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiLockDoor : MonoBehaviour
{
    [Header("必須設定")]
    public List<SwitchPlate> requiredSwitches;
    public GameObject doorVisualObject;

    [Header("プレイヤー設定")]
    public GameObject bluePlayer;
    public GameObject redPlayer;

    [Header("カメラ演出設定")]
    public Vector3 cameraFocusOffset = new Vector3(0, 2, -5);
    public float focusDuration = 1.5f;
    public float postDisappearDelay = 0.5f;

    [Header("サウンド")]
    public AudioClip switchPressSound;
    public AudioClip doorOpenSound;

    private AudioSource audioSource;
    private CameraController cameraController;
    private bool isOpening = false;
    private MonoBehaviour blueControllerScript;
    private MonoBehaviour redControllerScript;
    private Rigidbody blueRb;
    private Rigidbody redRb;

    private HashSet<SwitchPlate> platesAlreadyPressed = new HashSet<SwitchPlate>();

    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        if (doorVisualObject == null) Debug.LogError("ドアのモデルが設定されていません！", this.gameObject);

        if (bluePlayer != null)
        {
            blueControllerScript = bluePlayer.GetComponent<BluePlayerController>();
            blueRb = bluePlayer.GetComponent<Rigidbody>();
        }
        if (redPlayer != null)
        {
            redControllerScript = redPlayer.GetComponent<RedPlayerController>();
            redRb = redPlayer.GetComponent<Rigidbody>();
        }

        audioSource = Camera.main.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("Main CameraにAudioSourceがありません！", this.gameObject);
        }
    }

    void Update()
    {
        if (isOpening) return;

        bool allSwitchesPressed = true;
        foreach (SwitchPlate plate in requiredSwitches)
        {
            if (plate.IsPressed)
            {
                if (!platesAlreadyPressed.Contains(plate))
                {
                    platesAlreadyPressed.Add(plate);
                    if (audioSource != null && switchPressSound != null)
                    {
                        audioSource.PlayOneShot(switchPressSound);
                    }
                }
            }
            else
            {
                allSwitchesPressed = false;
                if (platesAlreadyPressed.Contains(plate))
                {
                    platesAlreadyPressed.Remove(plate);
                }
            }
        }

        if (allSwitchesPressed)
        {
            StartCoroutine(OpenDoorSequence());
        }
    }

    private IEnumerator OpenDoorSequence()
    {
        isOpening = true;

        foreach (SwitchPlate plate in requiredSwitches)
        {
            plate.IsPermanentlyActive = true;
        }

        cameraController.enabled = false;
        if (blueControllerScript != null) blueControllerScript.enabled = false;
        if (redControllerScript != null) redControllerScript.enabled = false;
        if (blueRb != null) blueRb.linearVelocity = Vector3.zero;
        if (redRb != null) redRb.linearVelocity = Vector3.zero;

        Vector3 targetPosition = doorVisualObject.transform.position + cameraFocusOffset;
        Quaternion targetRotation = Quaternion.LookRotation(doorVisualObject.transform.position - targetPosition);
        float travelTime = 0.5f;
        float elapsedTime = 0f;
        Vector3 startingPos = cameraController.transform.position;
        Quaternion startingRot = cameraController.transform.rotation;
        while (elapsedTime < travelTime)
        {
            cameraController.transform.position = Vector3.Lerp(startingPos, targetPosition, elapsedTime / travelTime);
            cameraController.transform.rotation = Quaternion.Lerp(startingRot, targetRotation, elapsedTime / travelTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cameraController.transform.position = targetPosition;
        cameraController.transform.rotation = targetRotation;

        yield return new WaitForSeconds(focusDuration);

        doorVisualObject.SetActive(false);

        if (audioSource != null && doorOpenSound != null)
        {
            audioSource.PlayOneShot(doorOpenSound);
        }

        yield return new WaitForSeconds(postDisappearDelay);

        cameraController.enabled = true;
        if (blueControllerScript != null) blueControllerScript.enabled = true;
        if (redControllerScript != null) redControllerScript.enabled = true;
    }
}