using UnityEngine;

public class Pushable_Hasi : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static; // Startzustand: unbeweglich
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hasi"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // Prüfe ob Hasi von oben kommt: Kontakt-Normal zeigt nach unten
                if (contact.normal.y < -0.5f)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
                    Debug.Log("Hasi ist von oben draufgesprungen – Objekt fällt!");
                    break;
                }
            }
        }
    }
}
