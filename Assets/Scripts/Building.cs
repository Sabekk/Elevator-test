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
        float upTmp = 0.4f * Time.deltaTime;

        while (isMoving)
        {
            elevator.transform.position += transform.up * Time.deltaTime;
            //elevator.transform.Translate(0, 0.1f, 0);

            if (floors[floorId].transform.position.y <= elevator.transform.position.y)
            {
                isMoving = false;
            }
            yield return null;
        }
        
    }

}
