using UnityEngine;

public class Killables : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hasi") || other.CompareTag("Armi"))
        {
            // Finde beide Spieler im Spiel
            PlayerWASD hasi = GameObject.FindWithTag("Hasi")?.GetComponent<PlayerWASD>();
            Armi armi = GameObject.FindWithTag("Armi")?.GetComponent<Armi>();

            // Beide sterben lassen, wenn vorhanden
            if (hasi != null)
            {
                hasi.Die();
                Debug.Log("Hasi died");
            }

            if (armi != null)
            {
                armi.Die();
                Debug.Log("Armi died");
            }
        }
    }
}
