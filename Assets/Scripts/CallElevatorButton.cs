using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallElevatorButton : MonoBehaviour
{
    public int buttonCallId;
    bool canCloseDoor=true;

    private void OnMouseDown()
    {
        
        if (canCloseDoor == true)
        {
            Floor.OpenFloorDoor?.Invoke(buttonCallId);
            Elevator.OpenElevatorDoor?.Invoke();
            Elevator.SetActualFloorLevel?.Invoke(buttonCallId);
            canCloseDoor = false;
            StartCoroutine(CloseDoor());
        }
        
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(5f);
        Floor.CloseFloorDoor?.Invoke(buttonCallId);
        Elevator.CloseElevatorDoor?.Invoke();
        canCloseDoor = true;
    }

}
