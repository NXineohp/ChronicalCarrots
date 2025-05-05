using UnityEngine;

public class CrawlablePassThrough : MonoBehaviour
{
    private Collider2D solidCollider;

    private void Start()
    {
        // Den festen Collider vom Crawlable-Parent holen
        solidCollider = transform.parent.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Armi"))
        {
            Physics2D.IgnoreCollision(solidCollider, other, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Armi"))
        {
            Physics2D.IgnoreCollision(solidCollider, other, false);
        }
    }
}