using UnityEngine;

public class LadderZone : MonoBehaviour
{
    public float climbSpeed = 3f;
    public float slideSpeed = 1f;

    private Rigidbody2D hasiRb;
    private bool hasiInZone = false;
    private float originalGravity;

    private PlayerWASD hasi;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hasi"))
        {
            hasiRb = other.GetComponent<Rigidbody2D>();
            if (hasiRb != null)
            {
                hasi = other.gameObject.GetComponent<PlayerWASD>();
                if (hasi != null)
                {
                    hasi.SetAnimBool("isClimbing", true);
                }
                hasiInZone = true;
                originalGravity = hasiRb.gravityScale; // merken
                hasiRb.gravityScale = 0f;
                hasiRb.linearVelocity = Vector2.zero;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hasi") && hasiRb != null)
        {
            hasi = other.gameObject.GetComponent<PlayerWASD>();
            if (hasi != null)
            {
                hasi.SetAnimBool("isClimbing", false);
            }
            ExitLadder();
        }
    }

    void Update()
    {

        if (!hasiInZone || hasiRb == null) return;

        if (Input.GetKey(KeyCode.W))
        {
            hasiRb.linearVelocity = new Vector2(hasiRb.linearVelocity.x, climbSpeed);
        }
        else
        {
            hasiRb.linearVelocity = new Vector2(hasiRb.linearVelocity.x, -slideSpeed);
        }
    }

    private void ExitLadder()
    {
        if (hasiRb != null)
        {
            hasiRb.gravityScale = originalGravity; // zurücksetzen
        }

        hasiInZone = false;
        hasiRb = null;
    }
}
