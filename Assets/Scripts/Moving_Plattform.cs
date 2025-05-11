using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
    [Header("Bewegung")]
    public Vector2 startPosition;
    public Vector2 endPosition;
    public float speed = 2f;

    [Header("Pause")]
    public float waitTime = 1f;

    private Vector2 currentTarget;
    private bool isMoving = true;

    private HashSet<Transform> riders = new HashSet<Transform>();

    private void Start()
    {
        transform.position = startPosition;
        currentTarget = endPosition;
        StartCoroutine(MovePlatform());
    }

    private IEnumerator MovePlatform()
    {
        while (true)
        {
            if (isMoving)
            {
                Vector2 oldPos = transform.position;
                Vector2 newPos = Vector2.MoveTowards(oldPos, currentTarget, speed * Time.deltaTime);
                Vector2 movement = newPos - oldPos;

                transform.position = newPos;

                // Alle Mitfahrer bewegen
                foreach (Transform rider in riders)
                {
                    rider.position += (Vector3)movement;
                }

                if (Vector2.Distance(newPos, currentTarget) < 0.01f)
                {
                    isMoving = false;
                    yield return new WaitForSeconds(waitTime);
                    currentTarget = (currentTarget == endPosition) ? startPosition : endPosition;
                    isMoving = true;
                }
            }

            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hasi") || collision.gameObject.CompareTag("Armi"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                Debug.Log("Rider detected: " + collision.transform.name);
                riders.Add(collision.transform);
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hasi") || collision.gameObject.CompareTag("Armi"))
        {
            Debug.Log("Rider exited: " + collision.transform.name);
            riders.Remove(collision.transform);
        }
    }
}
