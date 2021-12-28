using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Floor : MonoBehaviour
{
    Action OpenFloorDoor;

    public int floorLevel;
    public Animator floorAnimator;

    private void Start()
    {
        EventClass.OpenFloorDoor += OnClickCallElevatorButton;
        //OpenFloorDoor += OnClickCallElevatorButton;
    }
    private void OnDestroy()
    {
        EventClass.OpenFloorDoor += OnClickCallElevatorButton;
        //OpenFloorDoor -= OnClickCallElevatorButton;
    }

    public void OnClickCallElevatorButton()
    {
        floorAnimator.SetBool("OpenDoor", true);
    }

}
