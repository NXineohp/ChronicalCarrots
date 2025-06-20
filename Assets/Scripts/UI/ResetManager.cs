using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public Transform hasi;
    public Transform armi;

    private Vector3 hasiStartPos;
    private Vector3 armiStartPos;

    private void Start()
    {
        if (hasi != null) hasiStartPos = hasi.position;
        if (armi != null) armiStartPos = armi.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (hasi != null) hasi.position = hasiStartPos;
            if (armi != null) armi.position = armiStartPos;
        }
    }

    public void ResetGame()
    {
        if (hasi != null)
        {
            hasi.position = hasiStartPos;
            var rb = hasi.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;
        }

        if (armi != null)
        {
            armi.position = armiStartPos;
            var rb = armi.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;
        }

        // Timer zur�cksetzen
        FindObjectOfType<CountdownTimer>().ResetTimer();
    }

}
