using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        // Optional: Verhindern, dass mehrere Musik-Objekte entstehen
        GameObject[] musicObjs = GameObject.FindGameObjectsWithTag("Music");
        if (musicObjs.Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
