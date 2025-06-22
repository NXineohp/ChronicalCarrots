using NUnit.Framework;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class SwingingLiana : MonoBehaviour
{
    public float baseSwingSpeed = 0f;
    public float maxSwingAngle = 50f;
    public float hangDistance = 1.5f;
    public float jumpForce = 12f;
    public float swingBoost = 2f;
    public float swingDamping = 1f;
    public float maxSwingSpeed = 5f;
    public float straightenSpeed = 1f; // Speed to return to center (Z=0)

    private float timer = 0f;
    private float swingSpeed = 0f;
    private Transform hasi = null;
    private Rigidbody2D hasiRb;

    private bool returningToCenter = false;

    public Sprite swingForwardSprite; // z.B. hasi.swing_1
    public Sprite swingBackSprite;    // z.B. hasi.swing_6
    private Sprite currentSprite;
    private SpriteRenderer hasiRenderer;
    private PlayerWASD hasi_Script;
    private Animator Hasi_ANIMATOR;

    private void Start()
    {
        currentSprite = swingForwardSprite; // Start with forward swing sprite
    }
    void Update()
    {
        if (hasi == null && !returningToCenter)
        {
            // If no Hasi and swinging is almost stopped, start returning
            if (Mathf.Abs(swingSpeed) < 0.01f)
            {
                returningToCenter = true;
            }
        }

        if (returningToCenter)
        {
            // Smoothly rotate back to Z=0
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * straightenSpeed);

            return; // Skip rest of Update while returning to center
        }

        float zRotation = Mathf.Sin(timer) * maxSwingAngle;
        transform.localRotation = Quaternion.Euler(0f, 0f, zRotation);

        if (hasi == null)
        {
            // No Hasi -> continue swinging with leftover momentum
            timer += Time.deltaTime * swingSpeed;

            // Slowly dampen the swing
            swingSpeed = Mathf.Lerp(swingSpeed, 0f, Time.deltaTime * swingDamping);
        }
        else
        {
            float input = 0f;
            if (Input.GetKey(KeyCode.A))
            {
                input = -1f;
                Debug.Log("Swinging BACK");
                currentSprite = swingBackSprite;
            }

            if (Input.GetKey(KeyCode.D))
            {
                input = 1f;
                Debug.Log("Swinging FORWARD");
                currentSprite = swingForwardSprite;
            }

            if (hasiRenderer != null)
            {
                Debug.Log("Updating Hasi sprite");
                if (Hasi_ANIMATOR != null)
                {
                    Hasi_ANIMATOR.enabled = false;
                }
                hasiRenderer.sprite = currentSprite;
            }

            float swingDirection = Mathf.Cos(timer);

            if (input != 0f)
            {
                if ((input < 0f && swingDirection < 0f) || (input > 0f && swingDirection > 0f))
                {
                    swingSpeed += swingBoost * Time.deltaTime;
                }
                else
                {
                    swingSpeed -= swingBoost * Time.deltaTime;
                }

                swingSpeed = Mathf.Clamp(swingSpeed, -maxSwingSpeed, maxSwingSpeed);
            }
            else
            {
                swingSpeed = Mathf.Lerp(swingSpeed, 0f, Time.deltaTime * swingDamping);
            }

            // Update swinging
            timer += Time.deltaTime * swingSpeed;

            // Update Hasi hanging position
            Vector3 offset = transform.rotation * Vector3.down * hangDistance;
            hasi.position = transform.position + offset;

            // Check for jump off
            if (Input.GetKeyDown(KeyCode.W))
            {
                DetachHasi();
            }
        }
    }

    private void DetachHasi()
    {
        if (hasiRb != null)
        {
            hasiRb.isKinematic = false;
            hasiRb.linearVelocity = new Vector2(hasiRb.linearVelocity.x, 0f);
            hasiRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        hasi = null;
        hasiRb = null;

        // DO NOT reset swingSpeed -> Keep the momentum
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hasi"))
        {
            hasi = other.transform;
            hasiRb = hasi.GetComponent<Rigidbody2D>();
            hasiRenderer = hasi.GetComponent<SpriteRenderer>();
            Hasi_ANIMATOR = hasi.GetComponent<Animator>();

            if (hasiRenderer == null)
            {
                Debug.LogWarning("hasiRenderer is NULL!");
            }
            hasi_Script = other.gameObject.GetComponent<PlayerWASD>();

            if (hasiRb != null)
            {
                hasiRb.linearVelocity = Vector2.zero;
                hasiRb.isKinematic = true;
            }

            swingSpeed = 0f; // Reset swing when attaching
            returningToCenter = false; // Cancel any return to center if new Hasi comes
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hasi"))
        {
            if (hasiRb != null)
            {
                hasiRb.isKinematic = false;
            }

            PlayerWASD hasi = other.gameObject.GetComponent<PlayerWASD>();
            hasi_Script = other.gameObject.GetComponent<PlayerWASD>();
            hasi_Script.SetAnimBool("isSwinging", false);
            Hasi_ANIMATOR = hasi.GetComponent<Animator>();
            Hasi_ANIMATOR.enabled = true; // Re-enable animator
            if (hasi != null)
            {
                hasi.TriggerAnim("isJumping");
            }
            hasiRenderer = null; // Clear sprite renderer reference
            hasi = null;
            hasiRb = null;
        }
    }
}
