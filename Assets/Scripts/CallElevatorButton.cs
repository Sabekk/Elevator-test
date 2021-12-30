using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallElevatorButton : MonoBehaviour
{
    public int buttonCallId;
    public Elevator elevator;

    private void OnMouseDown()
    {
        elevator.CallElevatorToCurretFloor(buttonCallId);
    }
}
