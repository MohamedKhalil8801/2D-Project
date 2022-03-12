using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    public float DestroyDelay = 5.0f;

    void Start()
    {
        Destroy(gameObject, DestroyDelay);
    }
}
