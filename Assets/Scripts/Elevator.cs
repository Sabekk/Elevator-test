using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public int actualFloor;
    public Color actualFloorColor;
    public Color standardColor;

    public Animator elevatorAnimator;

    public List<ElevatorButton> buttons = new List<ElevatorButton>();

    void Start()
    {
        EventClass.OpenElevatorDoor += OpenElevatorDoors;
        EventClass.CloseElevatorDoor += CloseElevatorDoors;
        EventClass.SetActualFloorLevel += UpdateActualFloorLevel;
        actualFloor = 0;
    }

    private void OnDestroy()
    {
        EventClass.OpenElevatorDoor -= OpenElevatorDoors;
        EventClass.CloseElevatorDoor -= CloseElevatorDoors;
        EventClass.SetActualFloorLevel -= UpdateActualFloorLevel;
    }

    public void OpenElevatorDoors()
    {
        elevatorAnimator.SetBool("OpenDoor", true);
        elevatorAnimator.SetBool("CloseDoor", false);
        
    }

    public void CloseElevatorDoors()
    {
        elevatorAnimator.SetBool("OpenDoor", false);
        elevatorAnimator.SetBool("CloseDoor", true);
    }

    public void SetElevatorDoorsClosed()
    {
        elevatorAnimator.SetBool("OpenDoor", false);
        elevatorAnimator.SetBool("CloseDoor", false);
    }

    public void UpdateActualFloorLevel(int id)
    {
        buttons[actualFloor].SetActualFloor(false, standardColor);
        actualFloor = id;
        buttons[actualFloor].SetActualFloor(true, actualFloorColor);
    }

}
