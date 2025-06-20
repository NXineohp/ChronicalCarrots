using UnityEngine;

public class Button_Elevator : _Button_Parent
{
    public Elevator elevator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hasi") || other.CompareTag("Armi"))
        {
            elevator.StartElevator();
            isActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hasi") || other.CompareTag("Armi"))
        {
            elevator.StopAndReturn();
            isActivated = false;
        }
    }
}
