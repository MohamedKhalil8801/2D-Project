using UnityEngine;
using System.Collections;

public class RandomMovement : MonoBehaviour
{
    public Transform[] Points;
    [Range(1, 20)]
    public float MovementSpeed = 5.0f;
    [Range(1, 6)]
    public float MaxRelativeGameSpeed = 2.0f;
    [Range(0.01f, 5)]
    public float WaitBeforeRepositioning = 2.0f;

    private bool isReadyToMove = true;
    private int lastSelectedPoint = 0;
    
    private Vector2 target;
    void Start()
    {
        lastSelectedPoint = Random.Range(0, Points.Length);
        target = Points[lastSelectedPoint].position;
    }

    void Update()
    {
        if(isReadyToMove){
            if(Vector2.Distance(target, transform.position) > 0.05f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, MovementSpeed * GameManager.Instance.GetRelativeGameSpeed(MaxRelativeGameSpeed) * Time.deltaTime);
            } else{
                StartCoroutine(WaitThenSelectPoint(WaitBeforeRepositioning));
            }
        }
    }

    IEnumerator WaitThenSelectPoint(float time){
        isReadyToMove = false;
        yield return new WaitForSeconds(time);
        // Selecting a new point other than the one we had before.
        lastSelectedPoint = GameManager.Instance.RandomRangeExcept(0, Points.Length, lastSelectedPoint);
        target = Points[lastSelectedPoint].position;
        isReadyToMove = true;
    }
}
