using UnityEngine;

public class Breakable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Armi"))
        {
            Armi armi = collision.gameObject.GetComponent<Armi>();
            if (armi != null && armi.getIsRounded()) // Zugriff über Methode (siehe unten)
            {
                Destroy(gameObject);
            }
        }
    }
}