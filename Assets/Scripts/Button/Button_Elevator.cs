using UnityEngine;

public class Button_Elevator : MonoBehaviour
{
    public Elevator elevator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hasi"))
        {
            elevator.StartElevator();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hasi"))
        {
            elevator.StopAndReturn();
        }
    }
}
