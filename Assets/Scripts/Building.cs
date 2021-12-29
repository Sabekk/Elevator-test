using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public List<Floor> floors = new List<Floor>();
    public Elevator elevator;

    public static Action<int> CheckDoorsToOpen;

    private void Start()
    {
        Elevator.MoveElevator += MoveElevatorToOtherLevel;
        CheckDoorsToOpen += OpenCurretDoors;
    }
    private void OnDestroy()
    {
        Elevator.MoveElevator -= MoveElevatorToOtherLevel;
        CheckDoorsToOpen -= OpenCurretDoors;
    }

    public void MoveElevatorToOtherLevel(int floorId)
    {
        StartCoroutine(MoveElevator(floorId));
        Elevator.SetActualFloorLevel?.Invoke(floorId);
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

        elevator.isReady = true;

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
        Elevator.OpenElevatorDoor?.Invoke();
        Floor.OpenFloorDoor?.Invoke(id);
        yield return new WaitForSeconds(5f);
        CloseCurretDoors(id);
    }

    void CloseCurretDoors(int id)
    {
        Elevator.CloseElevatorDoor?.Invoke();
        Floor.CloseFloorDoor?.Invoke(id);
    }


}
