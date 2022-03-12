using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class Character : MonoBehaviour
{
    public CharacterTypesEnum CharacterType = CharacterTypesEnum.Enemy;
    [Header("Status")]
    [Range(1, 20)]
    public int Health = 3;
    [Header("Flash when taking damage")]
    public Color HurtColor = Color.red;
    [Header("Character Death Camera Shake")]
    [Range(0, 20)]
    public float CamShakeMagnitude = 5;
    [Range(0, 2)]
    public float CamShakeDuration = 0.3f;
    [Header("References")]
    public RangedWeapon Weapon;
    public SpriteRenderer[] BodyParts;

    [HideInInspector]
    public bool isPlayer = false;

    private void Awake() {
        if(CompareTag("Player")){
            isPlayer = true;
        }
    }
    public void TakeDamage(int damage){
        Health -= damage;
        StartCoroutine(Flash());

        if(Health <= 0){
            Die();
        } else if(isPlayer){
            Debug.Log("Damage taken: " + damage + "\tHealth: " + Health);
        }
    }

    private void Die(){
        Camera.main.GetComponent<CameraShake>().ShakeCamera(CamShakeMagnitude, CamShakeDuration);

        if(isPlayer){
            Debug.Log("You died!");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Destroy(gameObject);
        } else{
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.GetComponent<Character>() != null){
            if(CharacterType != other.gameObject.GetComponent<Character>().CharacterType){
                // When an Ally touches an Enemy, both Ally and Enemy will damage each other.
                other.gameObject.GetComponent<Character>().TakeDamage(Weapon.Damage);
            }
        }
    }

    private IEnumerator Flash(){
        Color[] originalColors = new Color[BodyParts.Length];

        for (int i = 0; i < BodyParts.Length; i++)
        {
            originalColors[i] = BodyParts[i].color;
            BodyParts[i].color = HurtColor; //new Color(BodyParts[i].color.r - 10, BodyParts[i].color.g - 10, BodyParts[i].color.b - 10);
        }

        yield return new WaitForSeconds(0.05f);

        for (int i = 0; i < BodyParts.Length; i++)
        {
            BodyParts[i].color = originalColors[i];
        }
    }
}
