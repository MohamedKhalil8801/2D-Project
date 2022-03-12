using UnityEngine;

public class Bullet : MonoBehaviour
{

    [HideInInspector]
    public int Damage;
    [HideInInspector]
    public CharacterTypesEnum CharacterType = CharacterTypesEnum.Enemy;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<Character>() != null){
            if(CharacterType != other.gameObject.GetComponent<Character>().CharacterType){
                other.gameObject.GetComponent<Character>().TakeDamage(Damage);
                Destroy(gameObject);
            }
        } else if (other.gameObject.GetComponent<Bullet>() != null && CharacterType != other.gameObject.GetComponent<Bullet>().CharacterType){
            Destroy(other.gameObject);
        }
    }
}
