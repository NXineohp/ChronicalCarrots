using UnityEngine;

public class Button_ShowItem : _Button_Parent
{
    public GameObject itemToShow; // Das zu zeigende Objekt
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Hasi") || other.CompareTag("Armi"))
        {
            itemToShow.SetActive(true);
            triggered = true;
            isActivated = true;
        }
    }
}
