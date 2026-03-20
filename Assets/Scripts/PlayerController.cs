using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSfx;
    public AudioClip crashSfx;

    private Rigidbody rb;
    private InputAction jumpAction;
    private InputAction sprintAction;
    //private bool isOnGround = true;

    public int maxJump = 2;
    public int jumpCount = 0;

    private Animator playerAnim;
    private AudioSource playerAudio;

    public int playerHP = 3;
    public bool gameOver = false;

    public bool isSprint = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.gravity *= gravityModifier;

        jumpAction = InputSystem.actions.FindAction("Jump");
        sprintAction = InputSystem.actions.FindAction("Sprint");

        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpAction.triggered && !gameOver && jumpCount < maxJump)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            //isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSfx);

            jumpCount++;
        }
        if (sprintAction.IsPressed())
        {
            isSprint = true;
            playerAnim.speed = 2;
        }
        else
        {
            playerAnim.speed = 1;
            isSprint = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //isOnGround = true;
            dirtParticle.Play();

            jumpCount = 0;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerHP -= 1;
            
            explosionParticle.Play();
            playerAudio.PlayOneShot(crashSfx);

            if (playerHP <= 0)
            {
                Debug.Log("Game Over!");
                gameOver = true;
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                dirtParticle.Stop();
                explosionParticle.Play();
                playerAudio.PlayOneShot(crashSfx);
            }


        }
    }

}