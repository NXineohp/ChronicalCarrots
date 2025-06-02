using UnityEngine;

public class SpotlightEffect : MonoBehaviour
{
    public Transform player;              // Der Spieler
    public Material spotlightMat;         // Dein Spotlight-Material
    public Camera cam;                    // Main Camera

    public float visibleRadius = 0.3f;    // Sichtbarer Kreis-Radius
    public float hiddenRadius = 2.0f;     // Kein Effekt
    public float lerpSpeed = 2f;

    private bool active = false;
    private float currentRadius;

    void Start()
    {
        currentRadius = hiddenRadius;
    }

    void Update()
    {
        // Spielerposition in Viewport umrechnen
        Vector3 viewPos = cam.WorldToViewportPoint(player.position);
        spotlightMat.SetVector("_Center", new Vector4(viewPos.x, viewPos.y, 0, 0));

        // Lerp Radius (sanfter Übergang)
        float target = active ? visibleRadius : hiddenRadius;
        currentRadius = Mathf.Lerp(currentRadius, target, Time.deltaTime * lerpSpeed);
        spotlightMat.SetFloat("_Radius", currentRadius);

        // Beispiel: Taste H schaltet den Effekt
        if (Input.GetKeyDown(KeyCode.H))
            active = !active;
    }
}
