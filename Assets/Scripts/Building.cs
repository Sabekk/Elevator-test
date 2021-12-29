using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int actualFloor;
    public List<Floor> floors = new List<Floor>();

    private void Start()
    {
        EventClass.OpenFloorDoor += OnClickOpenElevator;
        EventClass.CloseFloorDoor += OnClickCloseElevator;
        EventClass.SetActualFloorLevel += UpdateActualFloorLevel;
        actualFloor = 0;
    }
    private void OnDestroy()
    {
        EventClass.OpenFloorDoor += OnClickOpenElevator;
        EventClass.CloseFloorDoor += OnClickCloseElevator;
        EventClass.SetActualFloorLevel -= UpdateActualFloorLevel;
    }



    public void UpdateActualFloorLevel(int id)
    {
        floors[actualFloor].OnClickCloseElevator();
        actualFloor = id;
    }

    public void OnClickOpenElevator(int floorId)
    {
        if(actualFloor== floorId)
        {
            floors[actualFloor].OnClickOpenElevator();
        }
    }

    public void OnClickCloseElevator(int floorId)
    {
        if (actualFloor == floorId)
        {
            floors[actualFloor].OnClickCloseElevator();
        }
    }

}
