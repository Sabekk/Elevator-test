using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventClass
{
    public static Action<int> OpenFloorDoor;
    public static Action OpenElevatorDoor;

    public static Action<int> CloseFloorDoor;
    public static Action CloseElevatorDoor;

    public static Action<int> SetActualFloorLevel;

    public static Action<int> PrepareElevator;
    public static Action<int> MoveElevator;
}
