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
        //EventClass.OpenFloorDoor += OnClickOpenElevator;
        //EventClass.CloseFloorDoor += OnClickCloseElevator;
        //OpenFloorDoor += OnClickCallElevatorButton;
    }
    private void OnDestroy()
    {
        //EventClass.OpenFloorDoor += OnClickOpenElevator;
        //EventClass.CloseFloorDoor += OnClickCloseElevator;
        //OpenFloorDoor -= OnClickCallElevatorButton;
    }

    public void OnClickOpenElevator()
    {

        floorAnimator.SetBool("OpenDoor", true);
    }

    public void OnClickCloseElevator()
    {

            floorAnimator.SetBool("CloseDoor", true);
    }


    public void SetElevatorDoorsClosed()
    {
        floorAnimator.SetBool("OpenDoor", false);
        floorAnimator.SetBool("CloseDoor", false);
    }


}
