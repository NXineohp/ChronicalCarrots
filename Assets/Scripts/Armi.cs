using UnityEngine;

public class Armi : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveSpeed_Ball = 2f;
    public float boostSpeed = 10f;
    public float boostDuration = 0.5f;
    public float rollingForce = 10f; // Force for sliding on halfpipe

    public float requiredMoveDistance = 1f;

    private Rigidbody2D rb;
    private Vector3 normalScale;
    private bool isRounded = false;
    private bool isBoosting = false;
    private float boostTimer = 0f;
    private Vector2 boostStartPosition;
    private bool canBoost = false;

    private int lastMoveDirection = 0; // -1 = left, 1 = right

    private bool isOnHalfpipe = false; // Is player currently on a halfpipe?

    public Sprite roundSprite;
    public Sprite normalSprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        normalScale = transform.localScale;
        boostStartPosition = transform.position;
    }

    public bool getIsRounded()
    {
        return isRounded;
    }

    void Update()
    {
        float moveX = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;

        // Boost timer countdown
        if (isBoosting)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0f)
            {
                isBoosting = false;
            }
        }

        // Only check movement if not rounded
        if (!isRounded)
        {
            if (moveX != 0f)
            {
                if (moveX != lastMoveDirection)
                {
                    boostStartPosition = transform.position;
                    lastMoveDirection = (int)moveX;
                    canBoost = false;
                }

                float distanceMoved = Mathf.Abs(transform.position.x - boostStartPosition.x);
                if (distanceMoved >= requiredMoveDistance)
                {
                    canBoost = true;
                }
            }
            else
            {
                boostStartPosition = transform.position;
                lastMoveDirection = 0;
                canBoost = false;
            }
        }

        // Switching to rolling shape
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!isRounded)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                GetComponent<SpriteRenderer>().color = Color.green;
                GetComponent<SpriteRenderer>().sprite = roundSprite;
                isRounded = true;

                if (canBoost)
                {
                    isBoosting = true;
                    boostTimer = boostDuration;
                }

                canBoost = false;
                lastMoveDirection = 0;
            }
        }
        else if (!Input.GetKey(KeyCode.DownArrow) && isRounded)
        {
            transform.localScale = normalScale;
            GetComponent<SpriteRenderer>().color = Color.blue;
            GetComponent<SpriteRenderer>().sprite = normalSprite;
            isRounded = false;
        }
    }

    void FixedUpdate()
    {
        float moveX = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;

        if (isRounded)
        {
            if (isOnHalfpipe && Input.GetKey(KeyCode.DownArrow))
            {
                // Sliding on halfpipe when DownArrow pressed
                Vector2 slideForce = new Vector2(moveX * rollingForce, 0f);
                rb.AddForce(slideForce, ForceMode2D.Force);
            }
            else
            {
                // Normal rolling movement (ball form but controllable)
                float currentSpeed = isBoosting ? boostSpeed : moveSpeed_Ball;
                rb.linearVelocity = new Vector2(moveX * currentSpeed, rb.linearVelocity.y);
            }
        }
        else
        {
            // Normal walking
            rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("halfpipe"))
        {
            isOnHalfpipe = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("halfpipe"))
        {
            isOnHalfpipe = false;
        }
    }
}
