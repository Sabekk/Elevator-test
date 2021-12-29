using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public int actualFloor;

    public bool doorsClosed;
    public bool isMoving;
    public bool isReady;

    public Color actualFloorColor;
    public Color standardColor;

    public static Action OnNotReady;

    public static Action OpenElevatorDoor;
    public static Action CloseElevatorDoor;
    public static Action<int> PrepareElevator;
    public static Action<int> MoveElevator;
    public static Action<int> SetActualFloorLevel;

    public Animator elevatorAnimator;

    public List<ElevatorButton> buttons = new List<ElevatorButton>();

    void Start()
    {
        OpenElevatorDoor += OpenElevatorDoors;
        CloseElevatorDoor += CloseElevatorDoors;
        SetActualFloorLevel += UpdateActualFloorLevel;
        PrepareElevator += PrepareToMove;
        OnNotReady += IsNotReady;
        actualFloor = 0;
    }

    private void OnDestroy()
    {
        OpenElevatorDoor -= OpenElevatorDoors;
        CloseElevatorDoor -= CloseElevatorDoors;
        SetActualFloorLevel -= UpdateActualFloorLevel;
        PrepareElevator -= PrepareToMove;
        OnNotReady -= IsNotReady;
    }

    public void OpenElevatorDoors()
    {
        if (!isMoving)
        {
            elevatorAnimator.SetBool("OpenDoor", true);
            elevatorAnimator.SetBool("CloseDoor", false);
        }
    }

    public void CloseElevatorDoors()
    {
        elevatorAnimator.SetBool("OpenDoor", false);
        elevatorAnimator.SetBool("CloseDoor", true);
        isReady = true;
    }

    public void SetElevatorDoorsClosed()
    {
        elevatorAnimator.SetBool("OpenDoor", false);
        elevatorAnimator.SetBool("CloseDoor", false);
    }

    public void PrepareToMove(int id)
    {
        if (!isMoving && isReady == true)
        {
            StartCoroutine(CloseDoorsAndRun(id));
        }
    }

    IEnumerator CloseDoorsAndRun(int id)
    {
        if (doorsClosed == false)
        {
            CloseElevatorDoor?.Invoke();
            Floor.CloseFloorDoor?.Invoke(actualFloor);
        }
        
        yield return new WaitForSeconds(4f);
        MoveElevator?.Invoke(id);
    }


    public void UpdateActualFloorLevel(int id)
    {
        buttons[actualFloor].SetActualFloor(false, standardColor);
        actualFloor = id;
        buttons[actualFloor].SetActualFloor(true, actualFloorColor);
    }

    public void SetDoorsOpen()
    {
        doorsClosed = false;
    }
    public void SetDoorsClosed()
    {
        doorsClosed = true;
    }

    public void IsNotReady()
    {
        isReady = false;
    }
}
