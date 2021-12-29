using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public List<Floor> floors = new List<Floor>();
    public GameObject elevator;

    private void Start()
    {
        EventClass.MoveElevator += MoveElevatorToOtherLevel;
    }
    private void OnDestroy()
    {
        EventClass.MoveElevator -= MoveElevatorToOtherLevel;
    }

    public void MoveElevatorToOtherLevel(int floorId)
    {
        StartCoroutine(MoveElevator(floorId));

    }

    IEnumerator MoveElevator(int floorId)
    {
        bool isMoving = true;
        bool directionUp;

        float moveSpeed = Time.deltaTime * 2;

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

        yield return new WaitForSeconds(2f);
        EventClass.OpenElevatorDoor?.Invoke();
        EventClass.OpenFloorDoor?.Invoke(floorId);


    }

}
