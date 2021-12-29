using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public int actualFloor;
    public bool doorsClosed;
    public Color actualFloorColor;
    public Color standardColor;

    public Animator elevatorAnimator;

    public List<ElevatorButton> buttons = new List<ElevatorButton>();

    void Start()
    {
        EventClass.OpenElevatorDoor += OpenElevatorDoors;
        EventClass.CloseElevatorDoor += CloseElevatorDoors;
        EventClass.SetActualFloorLevel += UpdateActualFloorLevel;
        EventClass.PrepareElevator += PrepareToMove;
        actualFloor = 0;
    }

    private void OnDestroy()
    {
        EventClass.OpenElevatorDoor -= OpenElevatorDoors;
        EventClass.CloseElevatorDoor -= CloseElevatorDoors;
        EventClass.SetActualFloorLevel -= UpdateActualFloorLevel;
        EventClass.PrepareElevator -= PrepareToMove;
    }

    public void OpenElevatorDoors()
    {
        doorsClosed = false;
        elevatorAnimator.SetBool("OpenDoor", true);
        elevatorAnimator.SetBool("CloseDoor", false);
        
    }

    public void CloseElevatorDoors()
    {
        doorsClosed = true;
        elevatorAnimator.SetBool("OpenDoor", false);
        elevatorAnimator.SetBool("CloseDoor", true);
    }

    public void SetElevatorDoorsClosed()
    {
        
        elevatorAnimator.SetBool("OpenDoor", false);
        elevatorAnimator.SetBool("CloseDoor", false);
    }

    public void PrepareToMove(int id)
    {
        if (doorsClosed == false)
        {
            StartCoroutine(CloseDoorsAndRun(id));
        }
        else
        {
            EventClass.MoveElevator?.Invoke(id);
        }
    }

    IEnumerator CloseDoorsAndRun(int id)
    {
        EventClass.CloseElevatorDoor?.Invoke();
        EventClass.CloseFloorDoor?.Invoke(id);
        yield return new WaitForSeconds(2f);
        EventClass.MoveElevator?.Invoke(id);
    }


    public void UpdateActualFloorLevel(int id)
    {
        buttons[actualFloor].SetActualFloor(false, standardColor);
        actualFloor = id;
        buttons[actualFloor].SetActualFloor(true, actualFloorColor);
    }

}
