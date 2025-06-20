using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
    [Header("Bewegung")]
    public Vector3 startPosition;
    public Vector3 endPosition;
    public float speed = 2f;

    [Header("Pause")]
    public float waitTime = 1f;

    private Vector3 currentTarget;
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
                Vector3 oldPos = transform.position;
                Vector3 newPos = Vector2.MoveTowards(oldPos, currentTarget, speed * Time.deltaTime);
                Vector3 movement = newPos - oldPos;

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
                riders.Add(collision.transform);
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hasi") || collision.gameObject.CompareTag("Armi"))
        {
            riders.Remove(collision.transform);
        }
    }
}
