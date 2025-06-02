using UnityEngine;

public class Killables : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hasi"))
        {
            PlayerWASD player = other.GetComponent<PlayerWASD>();
            if (player != null)
            {
                player.Die();
                Debug.Log("Hasi died");
            }
        }
        else if (other.CompareTag("Armi"))
        {
            Armi player = other.GetComponent<Armi>();
            if (player != null)
            {
                player.Die();
                Debug.Log("Armi died");
            }
        }
    }
}
