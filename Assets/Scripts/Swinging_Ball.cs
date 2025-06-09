using UnityEngine;

public class Swinging_Ball : MonoBehaviour
{
    [Header("Swing Settings")]
    public Transform pivot;
    public float swingAngle = 45f; // Maximale Ausschlagswinkel (±)
    public float swingSpeed = 1f;  // Geschwindigkeit der Schwingung

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float angle = Mathf.Sin((Time.time - startTime) * swingSpeed) * swingAngle;
        pivot.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
