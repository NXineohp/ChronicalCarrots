using UnityEngine;

public class Pushable : MonoBehaviour
{
    private Rigidbody2D rb;  // Rigidbody2D des Objekts
    private float mass; // Gewicht des Objects
    private float linearDamping; // Reibung mit Boden

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Holen des Rigidbody2D-Components
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Armi"))
        {
            // Erlaube die Bewegung in alle Richtungen, X wird nicht eingefroren
            rb.bodyType = RigidbodyType2D.Dynamic;  // Setzt den Rigidbody auf dynamisch
            rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Einschrönkung nur in Z - Achse (Rotieren)
            Debug.Log("Objekt wird von Armi bewegt, X ist nicht eingefroren!");
        }
        else if (collision.gameObject.CompareTag("Hasi"))
        {
            // Blockiere die Bewegung auf der X-Achse (freeze)
            rb.bodyType = RigidbodyType2D.Dynamic;  // Setzt den Rigidbody auf dynamisch
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;  // X-Achse einfrieren
            Debug.Log("Objekt darf nicht von Hasi bewegt werden, X ist eingefroren!");
        }
    }
}
