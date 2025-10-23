using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiLockDoor : MonoBehaviour
{
    [Header("必須設定")]
    [Tooltip("このドアを開けるために必要なスイッチをすべてここに設定する。")]
    public List<SwitchPlate> requiredSwitches;

    [Tooltip("実際に表示・非表示を切り替えるドアのモデルを設定する。")]
    public GameObject doorVisualObject;
    
    [Header("プレイヤー設定")]
    [Tooltip("青ボールのゲームオブジェクトを設定する。")]
    public GameObject bluePlayer;
    [Tooltip("赤ボールのゲームオブジェクトを設定します。")]
    public GameObject redPlayer;


    [Header("カメラ演出設定")]
    [Tooltip("演出時にカメラがドアからどれだけ離れるか。")]
    public Vector3 cameraFocusOffset = new Vector3(0, 2, -5);

    [Tooltip("カメラがドアに注目している時間（秒）。この時間が過ぎるとドアが消える。")]
    public float focusDuration = 1.5f;

    [Tooltip("ドアが消えた後、カメラがその場に留まる時間（秒）。")]
    public float postDisappearDelay = 0.5f;

    private CameraController cameraController;
    private bool isOpening = false;
    
    private MonoBehaviour blueControllerScript;
    private MonoBehaviour redControllerScript;
    private Rigidbody blueRb;
    private Rigidbody redRb;


    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        if (doorVisualObject == null) Debug.LogError("ドアのモデルが設定されていません！", this.gameObject);
        
        if (bluePlayer != null) {
            blueControllerScript = bluePlayer.GetComponent<BluePlayerController>();
            blueRb = bluePlayer.GetComponent<Rigidbody>();
        }
        if (redPlayer != null) {
            redControllerScript = redPlayer.GetComponent<RedPlayerController>();
            redRb = redPlayer.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (isOpening) return;

        if (CheckAllSwitches())
        {
            StartCoroutine(OpenDoorSequence());
        }
    }

    private bool CheckAllSwitches()
    {
        if (requiredSwitches == null || requiredSwitches.Count == 0) return false;
        foreach (SwitchPlate plate in requiredSwitches)
        {
            if (!plate.IsPressed) return false;
        }
        return true;
    }

    private IEnumerator OpenDoorSequence()
    {
        isOpening = true;
        
        cameraController.enabled = false;
        if(blueControllerScript != null) blueControllerScript.enabled = false;
        if(redControllerScript != null) redControllerScript.enabled = false;
        if(blueRb != null) blueRb.linearVelocity = Vector3.zero;
        if(redRb != null) redRb.linearVelocity = Vector3.zero;

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

        yield return new WaitForSeconds(postDisappearDelay);
  
        cameraController.enabled = true;
        if(blueControllerScript != null) blueControllerScript.enabled = true;
        if(redControllerScript != null) redControllerScript.enabled = true;
    }
}