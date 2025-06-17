using UnityEngine;

public class ElevatorTrigger : _Button_Parent
{
    public ElevateUp elevator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hasi"))
        {
            elevator.StartElevator();
            isActivated = true;
        }
    }
}
