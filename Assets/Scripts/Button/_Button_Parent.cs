using UnityEngine;

public class _Button_Parent : MonoBehaviour
{
    private float activeAngle = 180f;
    private float inactiveAngle = 0f;
    public bool bloack_rotation = false;

    public AudioSource audioSource;

    [Tooltip("Maximale Abspieldauer des Sounds in Sekunden (0 = unbeschränkt)")]
    public float maxSoundDuration = 0f;

    protected bool isActivated = false;
    private bool lastState = false;

    protected virtual void Update()
    {
        if (isActivated != lastState)
        {
            lastState = isActivated;

            if (isActivated && audioSource != null)
            {
                audioSource.Play();

                // Wenn eine max. Dauer gesetzt ist (> 0), nach Zeit stoppen
                if (maxSoundDuration > 0f)
                {
                    CancelInvoke(nameof(StopSound)); // Sicherheitsmaßnahme
                    Invoke(nameof(StopSound), maxSoundDuration);
                }
            }

            if (bloack_rotation)
                return;

            RotateSelf(isActivated ? activeAngle : inactiveAngle);
        }
    }

    private void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void RotateSelf(float angle)
    {
        Vector3 currentEuler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(currentEuler.x, angle, currentEuler.z);
    }
}
