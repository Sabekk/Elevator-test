using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Animator elevatorAnimator;

    public List<Floor> floors;

    public List<AudioClip> clips;
    public AudioSource audioSource;

    Dictionary<int, Floor> floorsDict;

    public int actualFloor;
    public int nextFloor;
    public int calledFloor;
    public bool isReady = true;
    public bool doorsClosed = true;
    public bool isCalled = false;
    public bool isMoving = false;

    public Color actualFloorColor;
    public Color goingToFloorColor;
    public Color standardColor;

    public Action<int> GoToNextLevel;

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
    }

    private void OnDestroy()
    {
        GoToNextLevel -= GoToLevel;
    }


    public void GoToLevel(int level)
    {
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
            CloseDoorsInCurretLevel();
        }

    }

    IEnumerator MoveToTargetLevel()
    {
        isReady = false;
        yield return new WaitForSeconds(1f);
        isMoving = true;
        bool directionUp;
        float moveSpeed = Time.deltaTime * 3;

        buttons[actualFloor].buttonRenderer.materials[0].color = standardColor;
        buttons[nextFloor].buttonRenderer.materials[0].color = goingToFloorColor;

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
        }

        actualFloor = nextFloor;

        buttons[actualFloor].buttonRenderer.materials[0].color = actualFloorColor;

        System.Random randomClip = new System.Random();
        int randomInt = randomClip.Next(0, clips.Count);

        audioSource.clip = clips[randomInt];
        audioSource.PlayOneShot(audioSource.clip);
    }

    IEnumerator OpenDoors()
    {
        // Symuluje czas otwierania drzwi
        //yield return new WaitForSeconds(1f);

        OpenDoorsInCurretLevel();
        yield return new WaitForSeconds(2f);
        isReady = true;

        yield return new WaitForSeconds(3f);

        CloseDoorsInCurretLevel();

        yield return new WaitForSeconds(2f);

        if (isCalled == true)
        {
            isReady = false;
            nextFloor = calledFloor;
            isCalled = false;
            yield return ElevatorCycle();
            
        }
    }

    public void CallElevatorToCurretFloor(int called)
    {
        calledFloor = called;
        if(calledFloor == actualFloor && nextFloor == actualFloor && isReady && doorsClosed)
        {
            StartCoroutine(OpenDoors());
        }
        else
        { 
            if (isReady && !isMoving)
            {
                isReady = false;
                if (calledFloor != actualFloor)
                {
                    nextFloor = calledFloor;
                }
                
                StartCoroutine(ElevatorCycle());
            }
            else
            {
                if(nextFloor != calledFloor)
                {
                    isCalled = true;
                }
            }
        }
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
        doorsClosed = true;
        floorsDict[actualFloor].CloseDoorsInCurretLevel();
        elevatorAnimator.SetBool("OpenDoor", false);
        elevatorAnimator.SetBool("CloseDoor", true);
    }

    public void SetDoorsClosedInCurretLevel()
    { 
        floorsDict[actualFloor].SetDoorsClosedInCurretLevel();
        elevatorAnimator.SetBool("OpenDoor", false);
        elevatorAnimator.SetBool("CloseDoor", false);
    }

    public void EmergencyOpenElevator()
    {
        if (elevatorAnimator.GetBool("CloseDoor") == true)
        {
            isReady = false;
            StopAllCoroutines();
            //StartCoroutine(OpenDoors());
            StartCoroutine(StopEmergency());
        }
    }

    IEnumerator StopEmergency()
    {
        yield return OpenDoors();
        yield return new WaitForSeconds(2f);
        if (actualFloor != nextFloor)
        {
            yield return MoveToTargetLevel();
            yield return OpenDoors();
        }
    }

}
