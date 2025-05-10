using UnityEngine;

public class Catapult : MonoBehaviour
{
    private Rigidbody2D rb;  // Rigidbody2D des Objekts

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Holen des Rigidbody2D-Components
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hasi"))
        {
            // Blockiere die Bewegung auf der X-Achse (freeze)
            rb.constraints = RigidbodyConstraints2D.FreezeAll;  // X-Achse einfrieren
            Debug.Log("Objekt darf nicht von Hasi bewegt werden, X ist eingefroren!");
        } else if (collision.gameObject.CompareTag("Armi"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.constraints = RigidbodyConstraints2D.None;
        }
    }
}
