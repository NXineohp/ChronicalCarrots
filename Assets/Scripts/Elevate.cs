using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{
    [Header("Bewegung")]
    public float moveSpeed = 2f;
    public float targetHeight = 5f;
    public float delayBeforeElevate = 1f;

    [Header("Trigger")]
    public bool isTriggerable = true;

    private Vector2 originalPosition;
    private float currentHeight;

    private Coroutine elevatorCoroutine = null;
    private bool isAtTop = false;
    private int armiCount = 0;

    private void Start()
    {
        originalPosition = transform.position;
        currentHeight = transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Armi"))
        {
            armiCount++;

            if (!isTriggerable)
            {
                if (!isAtTop)
                {
                    StartElevator();
                }
                // Wenn er oben ist, nichts tun – warten bis Armi runtergeht
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Armi"))
        {
            armiCount = Mathf.Max(armiCount - 1, 0);

            if (!isTriggerable && isAtTop && armiCount == 0)
            {
                StopAndReturn();
            }
        }
    }

    public void StartElevator()
    {
        Debug.Log("Elevator started");
        if (elevatorCoroutine != null)
            StopCoroutine(elevatorCoroutine);

        elevatorCoroutine = StartCoroutine(ElevateUp());
    }

    public void StopAndReturn()
    {
        Debug.Log("Elevator stopped and returning");
        if (elevatorCoroutine != null)
            StopCoroutine(elevatorCoroutine);

        elevatorCoroutine = StartCoroutine(ElevateDown());
    }

    private IEnumerator ElevateUp()
    {
        yield return new WaitForSeconds(delayBeforeElevate);

        while (currentHeight < originalPosition.y + targetHeight)
        {
            currentHeight = Mathf.MoveTowards(currentHeight, originalPosition.y + targetHeight, moveSpeed * Time.deltaTime);
            transform.position = new Vector2(transform.position.x, currentHeight);
            yield return null;
        }

        isAtTop = true;
        elevatorCoroutine = null;
    }

    private IEnumerator ElevateDown()
    {
        while (currentHeight > originalPosition.y)
        {
            currentHeight = Mathf.MoveTowards(currentHeight, originalPosition.y, moveSpeed * Time.deltaTime);
            transform.position = new Vector2(transform.position.x, currentHeight);
            yield return null;
        }

        isAtTop = false;
        elevatorCoroutine = null;
    }
}
