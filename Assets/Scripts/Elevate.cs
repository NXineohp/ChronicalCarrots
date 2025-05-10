using UnityEngine;
using System.Collections;  // Für die Coroutine

public class Elevate : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed;
    public float targetHeight;
    public float delayBeforeElevate = 2f;
    private float currentHeight;

    private bool moveToOriginalPosition = false;

    private Vector2 originalPosition;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position; // Speichert die aktuelle Position ab
        currentHeight = transform.position.y;
        Debug.Log("Start-Methode ausgeführt!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Armi"))
        {
            if (moveToOriginalPosition == false)
            {
                StartCoroutine(ElevateWithDelayUp());
            }
            else
            {
                StartCoroutine(ElevateWithDelayDown());
            }
        }
    }

    // Coroutine für die Bewegung

    private IEnumerator ElevateWithDelayUp()
    {
        yield return new WaitForSeconds(delayBeforeElevate); // Verzögerung abwarten, bevor die Bewegung startet

        while (currentHeight < originalPosition.y + targetHeight)
        {
            currentHeight = Mathf.MoveTowards(currentHeight, originalPosition.y + targetHeight, moveSpeed * Time.deltaTime);
            transform.position = new Vector2(transform.position.x, currentHeight);
            Debug.Log("Elevating: " + currentHeight);

            yield return null; // Solange die Zielhöhe noch nicht erreicht ist, warten

            if (Mathf.Approximately(currentHeight, originalPosition.y + targetHeight)) { moveToOriginalPosition = true; break; }
        }

        Debug.Log("Bewegung abgeschlossen!"); // Wenn das Ziel erreicht ist, beende die Coroutine
    }

    private IEnumerator ElevateWithDelayDown()
    {
        yield return new WaitForSeconds(delayBeforeElevate);

        while (currentHeight > originalPosition.y)
        {
            currentHeight = Mathf.MoveTowards(currentHeight, originalPosition.y, moveSpeed * Time.deltaTime);
            transform.position = new Vector2(transform.position.x, currentHeight);
            Debug.Log("Elevating: " + currentHeight);

            yield return null;

            if (Mathf.Approximately(currentHeight, originalPosition.y)) { moveToOriginalPosition = false; break; }
        }
    }
}
