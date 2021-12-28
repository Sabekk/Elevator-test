using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    public Color actualFloorColor;
    public Color standardColor;

    public Animator elevatorAnimator;


    GameObject selectedButton;

    public List<GameObject> buttons = new List<GameObject>();

    void Start()
    {
        EventClass.OnepElevatorDoor += OpenElevatorDoors;
        selectedButton = null;
    }

    public void OpenElevatorDoors()
    {
        elevatorAnimator.SetBool("OpenDoor", true);
    }
}
