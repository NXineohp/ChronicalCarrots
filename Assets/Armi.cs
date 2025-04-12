using UnityEngine;

public class PlayerArmi : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector3 normalScale;
    private bool isRounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalScale = transform.localScale;
    }

    void Update()
    {
        float moveX = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;

        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // Rund werden bei Pfeil runter
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!isRounded)
            {
                transform.localScale = new Vector3(1f, 1f, 1f); // z.B. runde Form
                GetComponent<SpriteRenderer>().color = Color.red; // farbliche �nderung
                isRounded = true;
            }
        }
        else if (isRounded)
        {
            transform.localScale = normalScale;
            GetComponent<SpriteRenderer>().color = Color.blue;
            isRounded = false;
        }
    }
}