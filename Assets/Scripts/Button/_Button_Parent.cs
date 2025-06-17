using UnityEngine;

public class _Button_Parent : MonoBehaviour
{
    private float activeAngle = 180f;
    private float inactiveAngle = 0f;

    // für Kind-Klassen zugänglich
    protected bool isActivated = false;

    private bool lastState = false;

    protected virtual void Update()
    {
        if (isActivated != lastState)
        {
            RotateSelf(isActivated ? activeAngle : inactiveAngle);
            lastState = isActivated;
        }
    }

    private void RotateSelf(float angle)
    {
        Vector3 currentEuler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentEuler.x, angle, currentEuler.z);
    }
}
