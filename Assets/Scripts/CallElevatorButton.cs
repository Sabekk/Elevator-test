using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallElevatorButton : MonoBehaviour
{
    public int buttonCallId;
    bool canCloseDoor=true;

    private void OnMouseDown()
    {
        
        if (canCloseDoor == true)
        {
            EventClass.OpenFloorDoor?.Invoke(buttonCallId);
            EventClass.OpenElevatorDoor?.Invoke();
            EventClass.SetActualFloorLevel?.Invoke(buttonCallId);
            canCloseDoor = false;
            StartCoroutine(CloseDoor());
        }
        
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(5f);
        EventClass.CloseFloorDoor?.Invoke(buttonCallId);
        EventClass.CloseElevatorDoor?.Invoke();
        canCloseDoor = true;
    }

}
