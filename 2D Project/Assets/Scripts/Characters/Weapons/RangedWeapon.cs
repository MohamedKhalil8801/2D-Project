using System.Collections;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [Header("Status")]
    [Range(1, 10)]
    public int Damage = 1;
    [Range(1, 50)]
    public int FireRate = 20;
    public bool CanShoot = true;
    [Header("Weapon Fire Camera Shake")]
    [Range(0, 20)]
    public float CamShakeMagnitude = 5;
    [Range(0, 2)]
    public float CamShakeDuration = 0.3f;
    [Header("References")]
    public AudioClip ShootSFX;
    public Bullet Bullet;
    public Transform ShootFrom;
    [Header("Bullet Pattern")]
    [Range(1, 8)]
    public int BulletAmount = 8;
    [Range(1, 60)]
    public int Angle = 10;

    private Character character;
    private bool isPlayerReadyToShoot = true;
 
    public void Start() {
        character = gameObject.GetComponentInParent<Character>();
        if(character == null){
            Debug.LogError("Weapon script parent has no Character script.");
        }

        if(!character.isPlayer){
            StartCoroutine(Shoot());
        }   
    }

    private void Update() {
        if(character.isPlayer && Input.GetButton("Fire1") && isPlayerReadyToShoot){
            isPlayerReadyToShoot = false;
            InstantiateBulletPattern();
            StartCoroutine(Shoot());
        }
    }
    public IEnumerator Shoot()
    {
        if(Bullet != null)
        {
            if (character.isPlayer){
                yield return new WaitForSeconds(5.0f/FireRate);
                isPlayerReadyToShoot = true;
            } else{
                while (Bullet != null)
                {
                    InstantiateBulletPattern();
                    yield return new WaitForSeconds(5.0f/FireRate);
                }
            }
        } 
    }

    public void InstantiateBulletPattern(){
        if(!CanShoot){
            return;
        }

        GameManager.Instance.PlayAudio(ShootSFX, 0.6f, true);

        float currentAngle = Angle * (BulletAmount/2);
        for (int z = 0; z < BulletAmount; z++)
        {
            Quaternion shotRotation = gameObject.transform.rotation;
            shotRotation *= Quaternion.Euler(0, 0, currentAngle);
            Bullet curBullet = Instantiate(Bullet, GetComponent<RangedWeapon>().ShootFrom.position, shotRotation, GameManager.Instance.BulletsPool);
            curBullet.Damage = Damage;
            curBullet.CharacterType = character.CharacterType;
            curBullet.name += " (" + character.name + ")";
            currentAngle = currentAngle - Angle;
        }
        
        if(CamShakeMagnitude > 0){
            Camera.main.GetComponent<CameraShake>().ShakeCamera(CamShakeMagnitude, CamShakeDuration);
        }
    }
}
