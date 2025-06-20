using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float startTimeInSeconds = 120f; // z. B. 2 Minuten
    public TextMeshProUGUI timerText;

    private float currentTime;

    private void Start()
    {
        currentTime = startTimeInSeconds;
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if (currentTime < 0)
                currentTime = 0;

            if (currentTime == 0)
            {
                ResetManager resetManager = FindFirstObjectByType<ResetManager>();
                resetManager?.ResetGame();
            }


            UpdateTimerDisplay();
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
        UpdateTimerDisplay();
    }

    public void PauseTimer()
    {
        enabled = false; // ⬅️ Stoppt Update() komplett
    }
}
