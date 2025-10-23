using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PeriodicObstacle : MonoBehaviour
{
    [Header("動作設定")]
    public Transform moveTarget;
    public float moveTime = 1.0f;
    public float stayDuration = 2.0f;
    public float waitDuration = 3.0f;

    private Rigidbody rb;
    private Vector3 originalPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.isKinematic = true; 

        originalPosition = rb.position;

        if (moveTarget == null)
        {
            Debug.LogError("目標地点（Move Target）が設定されていません！", this.gameObject);
        }
        else
        {
            StartCoroutine(MoveCycle());
        }
    }

    private IEnumerator MoveCycle()
    {
        while (true)
        {
            yield return StartCoroutine(MoveToPosition(moveTarget.position, moveTime));
            
            yield return new WaitForSeconds(stayDuration);
            
            yield return StartCoroutine(MoveToPosition(originalPosition, moveTime));
            
            yield return new WaitForSeconds(waitDuration);
        }
    }

    private IEnumerator MoveToPosition(Vector3 target, float timeToMove)
    {
        Vector3 startPosition = rb.position;
        float elapsedTime = 0f;

        while (elapsedTime < timeToMove)
        {
            Vector3 newPosition = Vector3.Lerp(startPosition, target, elapsedTime / timeToMove);
            rb.MovePosition(newPosition); 
            
            elapsedTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        rb.MovePosition(target);
    }
}