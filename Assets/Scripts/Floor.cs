using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Floor : MonoBehaviour
{
    public int floorLevel;
    public bool doorsClosed;
    public Animator floorAnimator;

    public static Action<int> OpenFloorDoor;
    public static Action<int> CloseFloorDoor;

    private void Start()
    {
        OpenFloorDoor += OnClickOpenElevator;
        CloseFloorDoor += OnClickCloseElevator;
    }
    private void OnDestroy()
    {
        OpenFloorDoor += OnClickOpenElevator;
        CloseFloorDoor += OnClickCloseElevator;
    }

    public void OnClickOpenElevator(int doorId)
    {
        if(floorLevel == doorId)
        floorAnimator.SetBool("OpenDoor", true);
    }

    public void OnClickCloseElevator(int doorId)
    {
        if (floorLevel == doorId)
            floorAnimator.SetBool("CloseDoor", true);
    }


    public void SetElevatorDoorsClosed()
    {
        floorAnimator.SetBool("OpenDoor", false);
        floorAnimator.SetBool("CloseDoor", false);
    }

    public void SetDoorsOpen()
    {
        doorsClosed = false;
    }
    public void SetDoorsClosed()
    {
        doorsClosed = true;
    }

}
