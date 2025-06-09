using UnityEngine;
using System.Collections;

public class PortToAnotherPortal : MonoBehaviour
{
    [Tooltip("The target portal to teleport to")]
    public Transform targetPortal;

    [Tooltip("Cooldown time to prevent instant re-teleportation")]
    public float teleportCooldown = 0.5f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Hasi") || other.CompareTag("Armi")) && targetPortal != null)
        {
            StartCoroutine(Teleport(other.transform));
        }
    }

    private IEnumerator Teleport(Transform player)
    {
        // Move the player to the target portal's position
        player.position = targetPortal.position;

        // Wait to avoid instant re-entry
        yield return new WaitForSeconds(teleportCooldown);
    }
}
