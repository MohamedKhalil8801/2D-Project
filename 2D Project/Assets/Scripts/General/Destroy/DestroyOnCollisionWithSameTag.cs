using UnityEngine;

public class DestroyOnCollisionWithSameTag : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag(gameObject.tag)){
            Destroy(gameObject);
        }
    }
}
