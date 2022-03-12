using UnityEngine;

public class FixedMovement : MonoBehaviour
{
    public Vector2 MovementVelocity = new Vector2(-5, 0);
    [Range(1, 6)]
    public float MaxRelativeGameSpeed = 2.0f;

    void Update()
    {
        transform.Translate(MovementVelocity * GameManager.Instance.GetRelativeGameSpeed(MaxRelativeGameSpeed) * Time.deltaTime);
    }
}
