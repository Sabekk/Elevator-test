using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Floor : MonoBehaviour
{
    public int floorLevel;
    public Animator floorAnimator;
    public float height;

    private void Start()
    {
        height = this.gameObject.transform.position.y;
    }

    public void CloseDoor()
    {
        floorAnimator.SetBool("CloseDoor", true);
        floorAnimator.SetBool("OpenDoor", false);
    }
    public void OpenDoor()
    {
        floorAnimator.SetBool("OpenDoor", true);
        floorAnimator.SetBool("CloseDoor", false);
    }

    public void CloseDoorsInCurretLevel()
    {
        floorAnimator.SetBool("CloseDoor", true);
        floorAnimator.SetBool("OpenDoor", false);
    }

    public void OpenDoorsInCurretLevel()
    {
        floorAnimator.SetBool("OpenDoor", true);
        floorAnimator.SetBool("CloseDoor", false);
    }

    public void SetDoorsClosedInCurretLevel()
    {
        floorAnimator.SetBool("OpenDoor", false);
        floorAnimator.SetBool("CloseDoor", false);
    }

    public void EmergencyOpenElevator()
    {
        //floorAnimator.SetBool("OpenDoor", false);
        floorAnimator.SetBool("CloseDoor", false);
        floorAnimator.SetBool("OpenDoor", true);

        StartCoroutine(StopEmergency());
    }

    IEnumerator StopEmergency()
    {
        yield return new WaitForSeconds(3f);
        floorAnimator.SetBool("EmergencyOpen", false);
    }

}
