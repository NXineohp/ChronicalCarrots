using UnityEngine;

public class Bridge_Controller : MonoBehaviour
{
    [Header("Buttons")]
    public Button_Bridge button1;
    public Button_Bridge button2;

    [Header("Bridge Settings")]
    public Transform bridgePivot; // ? Jetzt der neue Drehpunkt
    private Quaternion startRotation;
    public Quaternion endRotation;
    public float rotationSpeed = 90f;

    private bool bridgeLowered = false;

    void Start()
    {
        if (bridgePivot != null)
        {
            startRotation = bridgePivot.rotation;
        }
    }

    void Update()
    {
        if (!bridgeLowered && button1.isPressed && button2.isPressed)
        {
            bridgeLowered = true;
            Debug.Log("Zugbrücke wird abgesenkt.");
        }

        if (bridgeLowered)
        {
            bridgePivot.rotation = Quaternion.RotateTowards(
                bridgePivot.rotation,
                endRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}
