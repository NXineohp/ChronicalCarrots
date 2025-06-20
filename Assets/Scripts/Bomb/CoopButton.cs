using UnityEngine;

public class CoopButton : MonoBehaviour
{
    public enum ButtonType { Hasi, Armi }
    public ButtonType buttonType;

    [HideInInspector]
    public bool isCorrectlyPressed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((buttonType == ButtonType.Hasi && other.CompareTag("Hasi")) ||
            (buttonType == ButtonType.Armi && other.CompareTag("Armi")))
        {
            isCorrectlyPressed = true;
            CheckBothButtons();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((buttonType == ButtonType.Hasi && other.CompareTag("Hasi")) ||
            (buttonType == ButtonType.Armi && other.CompareTag("Armi")))
        {
            isCorrectlyPressed = false;
        }
    }

    private void CheckBothButtons()
    {
        CoopButton[] buttons = FindObjectsOfType<CoopButton>();
        bool allPressed = true;

        foreach (var btn in buttons)
        {
            if (!btn.isCorrectlyPressed)
            {
                allPressed = false;
                break;
            }
        }

        if (allPressed)
        {
            CountdownTimer timer = FindFirstObjectByType<CountdownTimer>();
            if (timer != null) timer.PauseTimer();
        }
    }
}
