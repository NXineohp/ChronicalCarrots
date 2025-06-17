using UnityEngine;

public class Pushable : MonoBehaviour
{
    private Rigidbody2D rb;  // Rigidbody2D des Objekts

    [Header("Push Settings")]
    public bool allowHasiPushX = false;  // Im Inspector steuerbar

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Armi"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.gravityScale = 10f;
            Debug.Log("Objekt wird von Armi bewegt, X ist nicht eingefroren!");
        }
        else if (collision.gameObject.CompareTag("Hasi"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;

            if (allowHasiPushX)
            {
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                Debug.Log("Hasi darf das Objekt in X-Richtung schieben!");
            }
            else
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
                Debug.Log("Hasi darf das Objekt NICHT in X-Richtung schieben!");
            }
        }
    }
}
