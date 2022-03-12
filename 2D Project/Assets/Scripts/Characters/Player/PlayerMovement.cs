using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Jump")]
    [Range(1, 10)]
    public float FallMultiplier = 5f;
    [Range(0, 10)]
    public int JumpHeight = 6;
    [Range(1, 20)]
    public float JumpSpeed = 3.0f;
    [Header("Ground Hit Camera Shake")]
    [Range(0, 1)]
    public float FallTimeActuationPoint = 0.42f;
    [Range(0, 20)]
    public float CamShakeMagnitude = 5;
    [Range(0, 2)]
    public float CamShakeDuration = 0.3f;
    [Header("References")]
    public AudioClip GroundHitSFX;
    public ParticleSystem Dust;

    private bool canJump = true;
    private float jumpForce;
    private float worldGravity;
    private float fallTime = 0;
    private bool jumpRequest = false;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // High gravityScale = faster jump and fall.
        rb.gravityScale = JumpSpeed;
        worldGravity = Physics2D.gravity.y;
    }

    void Update()
    {
        if(Input.GetButtonDown("Jump") && canJump){
            CreateDust();
            fallTime = 0;
            jumpRequest = true;
            canJump = false;
        }

        if(rb.velocity.y != 0){
            fallTime += Time.deltaTime/2.0f;
        }
    }

    private void FixedUpdate() {
        if(jumpRequest){
            jumpRequest = false;
            // Calculating the required jumpForce every time we jump to make sure all jumps are consistent and always reach the same height.
            jumpForce = Mathf.Sqrt(JumpHeight * -2 * (worldGravity * rb.gravityScale));
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if(rb.velocity.y < 0 || (rb.velocity.y > 0 && !Input.GetButton("Jump"))){
            rb.velocity += Vector2.up * (worldGravity * (FallMultiplier - 1) * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ground")){

            GameManager.Instance.PlayAudio(GroundHitSFX, 1, true);

            CreateDust();

            if(fallTime > FallTimeActuationPoint){
                Camera.main.GetComponent<CameraShake>().ShakeCamera(CamShakeMagnitude, CamShakeDuration);
            }
            
            canJump = true;
        }
    }

    public void CreateDust(){
        Dust.Play();
    }
}
