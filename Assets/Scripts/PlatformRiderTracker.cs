using System.Collections.Generic;
using UnityEngine;

public class PlatformRiderTracker : MonoBehaviour
{
    public HashSet<Transform> riders = new HashSet<Transform>();

    public Vector3 LastPosition { get; private set; }

    private void Start()
    {
        LastPosition = transform.position;
    }

    private void Update()
    {
        LastPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hasi") || collision.gameObject.CompareTag("Armi"))
        {
            riders.Add(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hasi") || collision.gameObject.CompareTag("Armi"))
        {
            riders.Remove(collision.transform);
        }
    }

    public void MoveRiders(Vector3 movement)
    {
        foreach (Transform rider in riders)
        {
            rider.position += movement;
        }
    }
}
