using UnityEngine;
using TMPro;
using UnityEngine.Video;
using System;

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

    private Color originalColor;
    private bool fullRangeSoundActivated = false;
    private float startRangeSound;

    private void Start()
    {
        currentTime = startTimeInSeconds;
        explosionVideoObject?.SetActive(false);
        originalColor = timerText.color; // 💾 Speichere Startfarbe
        startRangeSound = bombAlarmLoop.maxDistance;
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

            // 🔁 Tick-Intervall abhängig von Restzeit
            if (currentTime <= 30f)
            {
                timerText.color = Color.red;
                float tickInterval = currentTime <= 10f ? 0.5f : 1f;
                // 🔊 Ab 10 Sekunden → Tick ist im ganzen Level hörbar
                if (!fullRangeSoundActivated && currentTime <= 10f)
                {
                    fullRangeSoundActivated = true;

                    if (bombAlarmLoop != null)
                    {
                        bombAlarmLoop.maxDistance = 35f;
                        // Optional: falls Spatial Blend < 1, sicherstellen:
                        bombAlarmLoop.spatialBlend = 1f;
                    }
                }

                if (Time.time - lastBeepTime >= tickInterval)
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
        timerText.color = originalColor;
        fullRangeSoundActivated = false;

        if (bombAlarmLoop != null)
        {
            bombAlarmLoop.maxDistance = startRangeSound;     // oder dein Ausgangswert
            bombAlarmLoop.spatialBlend = 1f;    // bleibt 3D
        }
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
