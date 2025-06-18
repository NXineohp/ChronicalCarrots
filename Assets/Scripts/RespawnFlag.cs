using UnityEngine;

public class RespawnFlag : MonoBehaviour
{
    [Header("Spawnpunkte zum Setzen (optional)")]
    public Vector2? hasiSpawnPointOverride = null;
    public Vector2? armiSpawnPointOverride = null;

    [Header("Active Flag Texture")]
    public Sprite flagTexture = null;

    private bool hasiSet = false;
    private bool armiSet = false;
    private SpriteRenderer flag;

    private void Start()
    {
        Transform flagSkin = transform.Find("Square");
        if (flagSkin != null)
        {
            flag = flagSkin.GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogWarning("FlagHead nicht gefunden! Stelle sicher, dass es ein Child-Objekt namens 'FlagHead' gibt.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 basePosition = transform.position;

        if (other.CompareTag("Hasi") && !hasiSet)
        {
            PlayerWASD player = other.GetComponent<PlayerWASD>();
            if (player != null)
            {
                Vector2 spawnPoint = hasiSpawnPointOverride ?? basePosition + Vector2.left * 0.15f;
                player.SetRespawnPoint(spawnPoint);
                hasiSet = true;
                Debug.Log($"Hasi Spawnpoint gesetzt: {spawnPoint}");
                UpdateFlagColor();
            }
        }
        else if (other.CompareTag("Armi") && !armiSet)
        {
            Armi player = other.GetComponent<Armi>();
            if (player != null)
            {
                Vector2 spawnPoint = armiSpawnPointOverride ?? basePosition + Vector2.right * 0.15f;
                player.SetRespawnPoint(spawnPoint);
                armiSet = true;
                Debug.Log($"Armi Spawnpoint gesetzt: {spawnPoint}");
                UpdateFlagColor();
            }
        }
    }

    private void UpdateFlagColor()
    {
        if (flag == null) return;

        if (hasiSet || armiSet)
        {
            flag.sprite = flagTexture;
        }
    }
}
