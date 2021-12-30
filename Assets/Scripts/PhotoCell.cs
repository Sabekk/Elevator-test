using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCell : MonoBehaviour
{
    public Elevator elevator;
    public bool playerInPhotocell;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            elevator.EmergencyOpenElevator();
        }
    }
}
