using UnityEngine;

public class KillerBall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hasi"))
        {
            var player = collision.GetComponent<PlayerWASD>(); // oder dein Skriptname
            if (player != null)
            {
                player.Die();
                Debug.Log("Hasi wurde vom Pendel getroffen!");
            }
        }

        if (collision.CompareTag("Armi"))
        {
            var player = collision.GetComponent<Armi>(); // oder dein Skriptname
            if (player != null)
            {
                player.Die();
                Debug.Log("Hasi wurde vom Pendel getroffen!");
            }
        }
    }
}
