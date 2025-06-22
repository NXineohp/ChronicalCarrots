using UnityEngine;

public class Armi : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveSpeed_Ball = 2f;
    public float boostSpeed = 10f;
    public float boostDuration = 0.5f;
    public float rollingForce = 10f;

    public float requiredMoveDistance = 1f;

    private Rigidbody2D rb;
    private bool isRounded = false;
    private bool isBoosting = false;
    private float boostTimer = 0f;
    private Vector2 boostStartPosition;
    private bool canBoost = false;

    private int lastMoveDirection = 0;

    private bool isOnHalfpipe = false;
    private Vector2 respawnPoint;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boostStartPosition = transform.position;
        respawnPoint = transform.position;
        animator = GetComponent<Animator>();
    }

    public bool getIsRounded() => isRounded;

    void Update()
    {
        float moveX = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;

        // Flip Armi left/right
        if (moveX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0); // Look left
        }
        else if (moveX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0f, 0); // Look right
        }

        // Boost countdown
        if (isBoosting)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0f)
            {
                isBoosting = false;
            }
        }

        // Check for boosting possibility
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

        // Switch to rolling form
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isRounded)
        {
            isRounded = true;

            if (canBoost)
            {
                isBoosting = true;
                boostTimer = boostDuration;
            }

            canBoost = false;
            lastMoveDirection = 0;
        }
        else if (!Input.GetKey(KeyCode.DownArrow) && isRounded)
        {
            isRounded = false;
        }

        // Set animator states
        SetAnimBool("isRolling", isRounded);
        SetAnimBool("isWalking", !isRounded && moveX != 0f);
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
                Vector2 slideForce = new Vector2(moveX * rollingForce, 0f);
                rb.AddForce(slideForce, ForceMode2D.Force);
            }
            else
            {
                float currentSpeed = isBoosting ? boostSpeed : moveSpeed_Ball;
                rb.linearVelocity = new Vector2(moveX * currentSpeed, rb.linearVelocity.y);
            }
        }
        else
        {
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

    public void SetAnimBool(string paramName, bool value)
    {
        if (animator != null)
        {
            animator.SetBool(paramName, value);
        }
    }

    public void SetRespawnPoint(Vector2 point)
    {
        respawnPoint = point;
    }

    public void Respawn()
    {
        transform.position = respawnPoint;
        rb.linearVelocity = Vector2.zero;
        isRounded = false;
        isBoosting = false;
        boostTimer = 0f;
        canBoost = false;
        lastMoveDirection = 0;

        // Reset Animator
        SetAnimBool("isRolling", false);
        SetAnimBool("isWalking", false);
    }

    public void Die()
    {
        Respawn();
    }
}
