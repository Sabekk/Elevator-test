using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Animator elevatorAnimator;

    public List<Floor> floors;

    Dictionary<int, Floor> floorsDict;

    public int actualFloor;
    public int nextFloor;
    public bool isReady = true;
    public bool doorsClosed = true;

    public Color actualFloorColor;
    public Color standardColor;

    public Action<int> GoToNextLevel;
    public Action OpenCurretDoors;

    public List<ElevatorButton> buttons = new List<ElevatorButton>();


    private void Awake()
    {
        floorsDict = new Dictionary<int, Floor>(floors.Count);

        foreach (var floor in floors)
        {
            floorsDict.Add(floor.floorLevel, floor);
        }

        floors = null;
    }

    private void Start()
    {
        GoToNextLevel += GoToLevel;
        OpenCurretDoors += OpenCalledElevator;
    }

    private void OnDestroy()
    {
        GoToNextLevel -= GoToLevel;
        OpenCurretDoors -= OpenCalledElevator;
    }


    public void GoToLevel(int level)
    {
        Debug.Log("Test1");
        if ((!isReady) || !CanGoToLevel(level))
        {
            return;
        }

        nextFloor = level;
        StopAllCoroutines();
        //elevatorAnimator.Rebind();

        StartCoroutine(ElevatorCycle());
    }

    public bool CanGoToLevel(int level)
    {
        if (actualFloor == level)
        {
            return false;
        }

        if (!floorsDict.ContainsKey(level))
        {
            return false;
        }

        return true;
    }

    IEnumerator ElevatorCycle()
    {
        Debug.Log("Test2");
        isReady = false;

        if (doorsClosed == false)
        {
            yield return CloseDoors();
            yield return new WaitForSeconds(4f);
            yield return MoveToTargetLevel();
            yield return OpenDoors();
        }
        else
        {
            yield return new WaitForSeconds(1f);
            yield return MoveToTargetLevel();
            yield return OpenDoors();
        }

    }

    IEnumerator CloseDoors()
    {
        // Symuluje czas zamykania drzwi
        yield return new WaitForSeconds(1);

        //door.SetActive(true);
        if (elevatorAnimator.GetBool("CloseDoor") == false)
        {
            elevatorAnimator.SetBool("CloseDoor", true);
            elevatorAnimator.SetBool("OpenDoor", false);
            floorsDict[actualFloor].CloseDoor();
        }

    }

    IEnumerator MoveToTargetLevel()
    {
        bool isMoving = true;
        bool directionUp;
        float moveSpeed = Time.deltaTime * 2;

        if (floorsDict[nextFloor].transform.position.y > transform.position.y + 1)
        {
            directionUp = true;
        }
        else
        {
            directionUp = false;
        }

        while (isMoving)
        {
            transform.position += directionUp ? transform.up * moveSpeed
                                                        : -transform.up * moveSpeed;


            if (directionUp && floorsDict[nextFloor].transform.position.y <= transform.position.y)
            {
                isMoving = false;
            }

            if (!directionUp && floorsDict[nextFloor].transform.position.y >= transform.position.y)
            {
                isMoving = false;
            }
            yield return null;

            actualFloor = nextFloor;
        }
    }

    IEnumerator OpenDoors()
    {
        // Symuluje czas otwierania drzwi
        yield return new WaitForSeconds(1f);

        OpenCurretDooors();
        yield return new WaitForSeconds(2f);
        isReady = true;
        Debug.Log("Test4");

        yield return new WaitForSeconds(3f);

        CloseDoorsInCurretLevel();


    }

    public void OpenCurretDooors()
    {
        elevatorAnimator.SetBool("OpenDoor", true);
        elevatorAnimator.SetBool("CloseDoor", false);
        floorsDict[actualFloor].OpenDoor();
    }

    public void OpenCalledElevator()
    {
        StartCoroutine(OpenDoors());
    }

    public void OpenDoorsInCurretLevel()
    {
        //if (elevatorAnimator.GetBool("EmergencyOpen") == false)
        //{
            doorsClosed = false;
            floorsDict[actualFloor].OpenDoorsInCurretLevel();
            elevatorAnimator.SetBool("CloseDoor", false);
            elevatorAnimator.SetBool("OpenDoor", true);
        //}
    }

    public void CloseDoorsInCurretLevel()
    {
        floorsDict[actualFloor].CloseDoorsInCurretLevel();
        elevatorAnimator.SetBool("OpenDoor", false);
        elevatorAnimator.SetBool("CloseDoor", true);
    }

    public void SetDoorsClosedInCurretLevel()
    {
        doorsClosed = true;
        floorsDict[actualFloor].SetDoorsClosedInCurretLevel();
        elevatorAnimator.SetBool("OpenDoor", false);
        elevatorAnimator.SetBool("CloseDoor", false);
    }

    public void EmergencyOpenElevator()
    {
        if (elevatorAnimator.GetBool("CloseDoor") == true)
        {
            isReady = false;
            //elevatorAnimator.SetBool("OpenDoor", false);
            elevatorAnimator.SetBool("CloseDoor", false);
            elevatorAnimator.SetBool("OpenDoor", true);

            floorsDict[actualFloor].EmergencyOpenElevator();
            StartCoroutine(StopEmergency());
        }
    }

    IEnumerator StopEmergency()
    {
        yield return new WaitForSeconds(2f);
        elevatorAnimator.SetBool("EmergencyOpen", false);
        isReady = true;
        yield return new WaitForSeconds(4f);
        yield return CloseDoors();
        
    }

}
