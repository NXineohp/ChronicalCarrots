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

            if (currentTime == 0)
            {
                timerEnded = true;
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
