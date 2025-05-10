using System.Collections;
using UnityEngine;

public class ElevateUp : MonoBehaviour
{
    private Rigidbody2D rb;

    public float moveSpeed;
    public float targetHeight;
    public float delayBeforeElevate = 2f;
    private float currentHeight;

    private Vector2 originalPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position; // Speichert die aktuelle Position ab
        currentHeight = transform.position.y;
    }

    public void StartElevator()
    {
        StartCoroutine(ElevateWithDelayUp());
    }

    private IEnumerator ElevateWithDelayUp()
    {
        yield return new WaitForSeconds(delayBeforeElevate); // Verzögerung abwarten, bevor die Bewegung startet

        while (currentHeight < originalPosition.y + targetHeight)
        {
            currentHeight = Mathf.MoveTowards(currentHeight, originalPosition.y + targetHeight, moveSpeed * Time.deltaTime);
            transform.position = new Vector2(transform.position.x, currentHeight);
            Debug.Log("Elevating: " + currentHeight);

            yield return null; // Solange die Zielhöhe noch nicht erreicht ist, warten
        }

        Debug.Log("Bewegung abgeschlossen!"); // Wenn das Ziel erreicht ist, beende die Coroutine
    }
}
