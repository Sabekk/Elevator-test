using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public int id;

    public bool isActualFloor = false;

    public MeshRenderer buttonRenderer;

    private void OnMouseDown()
    {
        if (isActualFloor == false)
        {

            EventClass.PrepareElevator?.Invoke(id);
        }
        else
        {
            EventClass.CheckDoorsToOpen?.Invoke(id);
        }
        //EventClass.SetActualFloorLevel?.Invoke(id);
    }

    public void SetActualFloor(bool isActual, Color newButtonColor)
    {
        isActualFloor = isActual;
        buttonRenderer.materials[0].color = newButtonColor;
    }
}
