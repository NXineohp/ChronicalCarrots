using UnityEngine;

public class Button_Bridge : _Button_Parent
{
    public bool isPressed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hasi") || collision.CompareTag("Armi"))
        {
            isPressed = true;
            Debug.Log($"{gameObject.name} wurde gedrückt.");
            isActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Hasi") || collision.CompareTag("Armi"))
        {
            isPressed = false;
            Debug.Log($"{gameObject.name} wurde losgelassen.");
            isActivated = false;
        }
    }
}