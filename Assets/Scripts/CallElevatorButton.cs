using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallElevatorButton : MonoBehaviour
{
    public int buttonCallId;
    private void OnMouseDown()
    {
        EventClass.OpenFloorDoor?.Invoke(buttonCallId);
        EventClass.OpenElevatorDoor?.Invoke();
        EventClass.SetActualFloorLevel?.Invoke(buttonCallId);
    }

}
