using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject breakablePrefab;  // Das Prefab von Breakable, das du instanziieren möchtest

    void Start()
    {
        Debug.Log("BUTTON STARTED");
    }

    // Wird aufgerufen, wenn eine Kollision oder Trigger-Interaktion stattfindet
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Überprüfe, ob der Button mit einem anderen Objekt kollidiert ist und der Tag stimmt
        if (collision.gameObject.CompareTag("Armi") || collision.gameObject.CompareTag("Hasi"))
        {
            Debug.Log("BUTTON BTUTTON BUTTON");

            // Überprüfen, ob bereits ein Breakable-Objekt im Spiel existiert
            if (GameObject.Find("Breakable") == null)
            {
                // Die spezifische Position, an der das Breakable instanziiert werden soll
                Vector2 spawnPosition = new Vector2(-2.46f, -0.9f);

                // Instanziiere das Breakable an der angegebenen Position
                Instantiate(breakablePrefab, spawnPosition, Quaternion.identity);
                Debug.Log("Breakable-Instanz erstellt an Position: " + spawnPosition);
            }
            else
            {
                Debug.Log("Breakable existiert bereits im Spiel!");
            }
        }
    }
}
