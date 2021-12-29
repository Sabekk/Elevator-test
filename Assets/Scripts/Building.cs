using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public List<Floor> floors = new List<Floor>();
    public Elevator elevator;

    private void Start()
    {
        EventClass.MoveElevator += MoveElevatorToOtherLevel;
        EventClass.CheckDoorsToOpen += OpenCurretDoors;
    }
    private void OnDestroy()
    {
        EventClass.MoveElevator -= MoveElevatorToOtherLevel;
        EventClass.CheckDoorsToOpen -= OpenCurretDoors;
    }

    public void MoveElevatorToOtherLevel(int floorId)
    {
        if (elevator.isMoving == false)
        {
            StartCoroutine(MoveElevator(floorId));
            EventClass.SetActualFloorLevel?.Invoke(floorId);
        }     
    }

    IEnumerator MoveElevator(int floorId)
    {
        bool isMoving = true;
        bool directionUp;
        float moveSpeed = Time.deltaTime * 2;

        elevator.isMoving = isMoving;

        if (floors[floorId].transform.position.y > elevator.transform.position.y + 1)
        {
            directionUp = true;
        }
        else
        {
            directionUp = false;
        }

        while (isMoving)
        {
            elevator.transform.position += directionUp ? transform.up * moveSpeed
                                                        : -transform.up * moveSpeed;


            if (directionUp && floors[floorId].transform.position.y <= elevator.transform.position.y)
            {
                isMoving = false;
            }

            if (!directionUp && floors[floorId].transform.position.y >= elevator.transform.position.y)
            {
                isMoving = false;
            }
            yield return null;
        }

        elevator.isMoving = isMoving;

        yield return new WaitForSeconds(2f);
        OpenCurretDoors(floorId);
        yield return new WaitForSeconds(5f);
        CloseCurretDoors(floorId);
    }

    void OpenCurretDoors(int id)
    {
        if (elevator.isMoving == false && elevator.doorsClosed == true)
        {
            StartCoroutine(OpenDoors(id));
        }
    }

    IEnumerator OpenDoors(int id)
    {
        EventClass.OpenElevatorDoor?.Invoke();
        EventClass.OpenFloorDoor?.Invoke(id);
        yield return new WaitForSeconds(5f);
        CloseCurretDoors(id);
    }

    void CloseCurretDoors(int id)
    {
        EventClass.CloseElevatorDoor?.Invoke();
        EventClass.CloseFloorDoor?.Invoke(id);
    }


}
