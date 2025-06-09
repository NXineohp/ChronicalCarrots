using UnityEngine;

public class Portal_Small : MonoBehaviour
{
    public Portal_Small connectedPortal; // Verknüpftes Gegenportal
    private bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTeleport) return;

        // Optional: Du kannst hier bestimmte Tags prüfen (z. B. nur "Player")
        if (connectedPortal != null)
        {
            // Starte Teleport
            connectedPortal.DisableTeleportTemporarily();

            // Versetze Objekt zu verbundenem Portal
            other.transform.position = connectedPortal.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Erst nach Verlassen wieder teleportierbar
        canTeleport = true;
    }

    public void DisableTeleportTemporarily()
    {
        canTeleport = false;
    }
}
