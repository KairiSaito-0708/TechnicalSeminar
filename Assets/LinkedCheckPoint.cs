using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LinkedCheckPoint : MonoBehaviour
{
    [Header("監視対象")]
    [Tooltip("このチェックポイントを有効にするために必要なスイッチをすべて設定します。")]
    public List<SwitchPlate> requiredPlates;

    [Header("UI設定")]
    [Tooltip("状態を表示するTextMeshProのテキスト")]
    public TextMeshProUGUI statusText;

    [Header("リスポーン地点")]
    [Tooltip("このチェックポイントでの青ボールの復活地点")]
    public Transform blueRespawnTransform;
    [Tooltip("このチェックポイントでの赤ボールの復活地点")]
    public Transform redRespawnTransform;

    private bool isActivated = false;

    void Update()
    {
        if (isActivated) return;

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
            statusText.text = "チェックポイント更新";
            GameManager.instance.blueLastCheckPointPosition = blueRespawnTransform.position;
            GameManager.instance.redLastCheckPointPosition = redRespawnTransform.position;
            isActivated = true; 
        }
        else if (pressedCount > 0)
        {
            statusText.text = pressedCount + "/" + requiredPlates.Count;
        }
        else
        {
            statusText.text = "";
        }
    }
}