using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class CountdownTimer : MonoBehaviour
{
    public float startTimeInSeconds = 120f;
    public TextMeshProUGUI timerText;

    public GameObject explosionVideoObject;      // 🔥 RawImage + VideoPlayer drauf
    public VideoPlayer videoPlayer;              // 🎞️ Deine VideoPlayer-Komponente

    private float currentTime;
    private bool timerEnded = false;

    public AudioSource bombAlarmLoop;      // 🔁 Wird jede Sekunde ab 15s gespielt
    public AudioSource bombExplosionSound; // 💥 Explosion bei 0s

    private float lastBeepTime = 0f;       // ⏱ Für 1-Sekunden-Intervall

    private void Start()
    {
        currentTime = startTimeInSeconds;
        explosionVideoObject?.SetActive(false); // Stelle sicher, dass Video-Objekt zu Beginn deaktiviert ist
    }

    private void Update()
    {
        if (timerEnded) return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
                currentTime = 0;

            UpdateTimerDisplay();

            // 🔁 Ab 15 Sekunden → Alarm jede Sekunde
            if (currentTime <= 15f)
            {
                if (Time.time - lastBeepTime >= 1f) // alle 1 Sekunde
                {
                    lastBeepTime = Time.time;

                    if (bombAlarmLoop != null)
                    {
                        bombAlarmLoop.time = 0.15f; // Optional: Skip Start
                        bombAlarmLoop.Play();
                    }
                }
            }

            if (currentTime == 0)
            {
                timerEnded = true;

                // 💥 Explosion Sound abspielen
                if (bombExplosionSound != null)
                {
                    bombExplosionSound.Play();
                }

                StartCoroutine(PlayExplosionThenReset());
            }
        }
    }


    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void ResetTimer()
    {
        currentTime = startTimeInSeconds;
        timerEnded = false;
        UpdateTimerDisplay();
    }

    public void PauseTimer()
    {
        enabled = false;
    }

    private System.Collections.IEnumerator PlayExplosionThenReset()
    {
        explosionVideoObject.SetActive(true);
        videoPlayer.Play();

        yield return new WaitForSeconds((float)videoPlayer.length);

        explosionVideoObject.SetActive(false);

        ResetManager resetManager = FindFirstObjectByType<ResetManager>();
        resetManager?.ResetGame();

        ResetTimer(); // optional – falls Timer erneut starten soll
    }
}
