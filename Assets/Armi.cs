using UnityEngine;

public class Armi : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector3 normalScale;
    private bool isRounded = false;

    public Sprite roundSprite;  // Reference to the round sprite
    public Sprite normalSprite;  // Reference to the round sprite

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalScale = transform.localScale;  // Speichert die aktuelle Gr��e des Objekts beim Start
    }

    void Update()
    {
        float moveX = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;

        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);  // Fix: "linearVelocity" zu "velocity"

        // Shape change when pressing DownArrow
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!isRounded)
            {
                transform.localScale = new Vector3(1f, 1f, 1f); // Beispiel f�r runde Skalierung
                GetComponent<SpriteRenderer>().color = Color.green;  // Farbe auf Rot setzen
                GetComponent<SpriteRenderer>().sprite = roundSprite;  // Rundes Sprite zuweisen
                isRounded = true;
            }
        }
        else if (isRounded)
        {
            transform.localScale = normalScale;  // Zur�cksetzen auf die urspr�ngliche Skalierung
            GetComponent<SpriteRenderer>().color = Color.blue;  // Farbe zur�ck auf Blau setzen
            GetComponent<SpriteRenderer>().sprite = normalSprite;  // Normales Sprite zuweisen
            isRounded = false;
        }
    }
}