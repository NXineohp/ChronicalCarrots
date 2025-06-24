using UnityEngine;

public class Breakable : MonoBehaviour
{
    public AudioSource breakSound; // Sound, der beim Zerstören abgespielt wird

    private Collider2D col;
    private SpriteRenderer spr;

    void Start()
    {
        col = GetComponent<Collider2D>();
        spr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Armi"))
        {
            Armi armi = collision.gameObject.GetComponent<Armi>();
            if (armi != null && armi.getIsRounded())
            {
                if (breakSound != null && breakSound.clip != null)
                {
                    breakSound.time = 0.2f;        // ⏩ Skip erste 0.2 Sekunden
                    breakSound.Play();

                    if (col != null) col.enabled = false;
                    if (spr != null) spr.enabled = false;

                    enabled = false;
                    Destroy(gameObject, breakSound.clip.length - 0.2f + 0.05f);
                }

                else
                {
                    Destroy(gameObject); // kein Sound → sofort löschen
                }
            }
        }
    }
}
