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

            //Debug.Log("isNotActual"+id);
            //EventClass.CloseFloorDoor?.Invoke(id);
            //EventClass.CloseElevatorDoor?.Invoke();
        }
        else
        {
            EventClass.OpenFloorDoor?.Invoke(id);
            EventClass.OpenElevatorDoor?.Invoke();
            StartCoroutine(CloseDoors());
        }
        EventClass.SetActualFloorLevel?.Invoke(id);
    }


    IEnumerator CloseDoors()
    {
        yield return new WaitForSeconds(5f);
        EventClass.CloseFloorDoor?.Invoke(id);
        EventClass.CloseElevatorDoor?.Invoke();
    }


    public void SetActualFloor(bool isActual, Color newButtonColor)
    {
        isActualFloor = isActual;
        buttonRenderer.materials[0].color = newButtonColor;
    }
}
