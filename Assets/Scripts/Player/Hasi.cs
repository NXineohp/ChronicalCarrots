using UnityEngine;

public class PlayerWASD : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 13f;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private Vector2 respawnPoint;
    public Animator animator;

    public AudioClip swingSound;
    public AudioClip jumpSound;
    public AudioSource walkSource; // für Loop
    public AudioSource pushSource; // für Loop
    public AudioSource sfxSource; // für OneShots
    public AudioSource climbSource; // für Loop

    private bool wasSwinging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position; // ⬅️ Setzt initialen Respawn-Punkt
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
            animator.SetBool("isWalking", true);
            if(!animator.GetBool("isClimbing") && !animator.GetBool("isSwinging"))
            {
                transform.eulerAngles = new Vector3(0, 180, 0); // Dreht Hasi nach links
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
            animator.SetBool("isWalking", true);
            transform.eulerAngles = new Vector3(0, 0, 0); // Dreht Hasi nach rechts
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        SetSound("isClimbing", climbSource);
        SetSound("isPushing", pushSource);
        if(animator.GetBool("isClimbing") || animator.GetBool("isPushing"))
        {
            walkSource.Stop(); // Stoppt den Geh-Sound, wenn Klettern oder Schieben aktiv ist
        }
        else
        {
            SetSound("isWalking", walkSource, isGrounded);
        }

        bool isSwinging = animator.GetBool("isSwinging");

        if (isSwinging && !wasSwinging)
        {
            Debug.Log("Swinging started!");
            // Spieler fängt gerade an zu schwingen → einmal Sound abspielen
            sfxSource.PlayOneShot(swingSound);
        }

        wasSwinging = isSwinging; // Zustand fürs nächste Frame merken

        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            animator.SetTrigger("isJumping");
            sfxSource.PlayOneShot(jumpSound);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        isGrounded = false;

        ContactPoint2D[] contacts = new ContactPoint2D[5];
        int contactCount = rb.GetContacts(contacts);

        for (int i = 0; i < contactCount; i++)
        {
            ContactPoint2D contact = contacts[i];
            if (contact.collider.CompareTag("Ground") && contact.normal.y > 0.5f)
            {
                isGrounded = true;
                break;
            }
        }

        SetAnimBool("isGrounded", isGrounded);
    }
    public void SetRespawnPoint(Vector2 point)
    {
        respawnPoint = point;
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        rb.linearVelocity = Vector2.zero;
        isGrounded = false;
    }

    public void Die()
    {
        Respawn();
    }

    public void SetAnimBool(string paramName, bool value)
    {
        if (animator != null)
        {
            animator.SetBool(paramName, value);
        }
    }

    public void TriggerAnim(string paramName)
    {
        if (animator != null)
        {
            animator.SetTrigger(paramName);
        }
    }

    public void SetSound(string soundname, AudioSource source, bool value = true)
    {
        if (source != null)
        {
            if(animator.GetBool(soundname) && value)
            {
                if (!source.isPlaying)
                {
                    source.Play();
                }
            }
            else
            {
                if (source.isPlaying)
                {
                    source.Stop();
                }
            }
        }
    }
}
