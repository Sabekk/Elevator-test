using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallElevatorButton : MonoBehaviour
{
    private void OnMouseDown()
    {

        EventClass.OpenFloorDoor?.Invoke();
    }

}
