using UnityEngine;

public class SpotlightEffect : MonoBehaviour
{
    public Transform player;              // Spieler (Hasi oder Armi)
    public Material spotlightMat;         // EIGENE Material-Instanz!
    public Camera cam;                    // Main Camera

    public float visibleRadius = 0.3f;    // Kleiner Lichtkreis
    public float hiddenRadius = 999f;     // Kein Effekt
    public float lerpSpeed = 2f;

    private bool deactive = false;
    private float currentRadius;

    public void SetActive(bool value)
    {
        deactive = value;
    }

    void Start()
    {
        SetActive(true);
        currentRadius = hiddenRadius;
        spotlightMat.SetFloat("_Radius", currentRadius);

        // Anfangsposition in Bildmitte setzen (falls Spieler noch nicht sichtbar)
        spotlightMat.SetVector("_Center", new Vector4(0.5f, 0.5f, 0, 0));
    }

    void Update()
    {
        if (player == null || cam == null || spotlightMat == null) return;

        // Spielerposition in Viewport-Koordinaten
        Vector3 viewPos = cam.WorldToViewportPoint(player.position);
        spotlightMat.SetVector("_Center", new Vector4(viewPos.x, viewPos.y, 0, 0));

        // Radius interpolieren
        float target = deactive ? visibleRadius : hiddenRadius;
        currentRadius = Mathf.Lerp(currentRadius, target, Time.deltaTime * lerpSpeed);
        spotlightMat.SetFloat("_Radius", currentRadius);

        //// Beispiel: Taste H schaltet den Effekt
        //if (Input.GetKeyDown(KeyCode.H))
        //    deactive = !deactive;
    }
}
