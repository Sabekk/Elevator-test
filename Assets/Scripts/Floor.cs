using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Floor : MonoBehaviour
{
    public int floorLevel;
    public Animator floorAnimator;

    private void Start()
    {
        EventClass.OpenFloorDoor += OnClickOpenElevator;
        EventClass.CloseFloorDoor += OnClickCloseElevator;
        //OpenFloorDoor += OnClickCallElevatorButton;
    }
    private void OnDestroy()
    {
        EventClass.OpenFloorDoor += OnClickOpenElevator;
        EventClass.CloseFloorDoor += OnClickCloseElevator;
        //OpenFloorDoor -= OnClickCallElevatorButton;
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


}
