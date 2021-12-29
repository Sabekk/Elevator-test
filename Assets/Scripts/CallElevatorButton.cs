using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallElevatorButton : MonoBehaviour
{
    public int buttonCallId;
    public Elevator elevator;

    private void OnMouseDown()
    {
        //Floor.OpenFloorDoor?.Invoke(buttonCallId);
        //Elevator.OpenElevatorDoor?.Invoke();
        //Elevator.SetActualFloorLevel?.Invoke(buttonCallId);
        elevator.OpenCurretDoors?.Invoke();
        //StartCoroutine(CloseDoor());
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(5f);
        //Floor.CloseFloorDoor?.Invoke(buttonCallId);
        //Elevator.CloseElevatorDoor?.Invoke();
    }

}
