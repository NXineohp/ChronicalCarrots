using UnityEngine;

public class SpotlightZoneTrigger : MonoBehaviour
{
    public SpotlightEffect spotlightEffect = null;
    public SpotlightEffect_Multi spotlightEffect_multi = null; // Optional für Multi-Player
    public bool variableSpotlight = true; // Variable Spotlight-Effekt

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hasi") || other.CompareTag("Armi"))
        {
            spotlightEffect?.SetActive(variableSpotlight);
            spotlightEffect_multi?.SetActive(variableSpotlight);
        }
    }
}
