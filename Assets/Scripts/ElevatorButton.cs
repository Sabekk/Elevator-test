using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    public int id;

    public bool isActualFloor = false;

    public MeshRenderer buttonRenderer;
    public Elevator elevator;

    private void OnMouseDown()
    {
        elevator.GoToNextLevel?.Invoke(id);
    }

    public void SetActualFloor(bool isActual, Color newButtonColor)
    {
        isActualFloor = isActual;
        buttonRenderer.materials[0].color = newButtonColor;
    }
}
