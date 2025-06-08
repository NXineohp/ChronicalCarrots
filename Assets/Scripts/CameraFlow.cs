using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public float minX = -10f;
    public float maxX = 50f;
    public float minY = 0f;
    public float maxY = 100f;
    public float smoothSpeed = 5f;

    private Transform hasi;
    private Transform armi;

    void Start()
    {
        GameObject hasiObj = GameObject.FindGameObjectWithTag("Hasi");
        GameObject armiObj = GameObject.FindGameObjectWithTag("Armi");

        if (hasiObj != null) hasi = hasiObj.transform;
        if (armiObj != null) armi = armiObj.transform;

        if (hasi == null || armi == null)
        {
            Debug.LogError("Hasi oder Armi nicht gefunden! Stelle sicher, dass die Tags korrekt gesetzt sind.");
        }
    }

    void LateUpdate()
    {
        if (hasi == null || armi == null) return;

        // Mittelwert der X-Positionen
        float targetX = (hasi.position.x + armi.position.x) / 2f;
        float targetY = (hasi.position.y + armi.position.y) / 2f;

        // Begrenzung auf minX und maxX
        targetX = Mathf.Clamp(targetX, minX, maxX);
        targetY = Mathf.Clamp(targetY, minY, maxY);

        // Zielposition nur auf X-Achse
        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

        // Smooth Follow
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
