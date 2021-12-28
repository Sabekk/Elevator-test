using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    public Color actualFloorColor;
    public Color standardColor;

    GameObject selectedButton;

    public List<GameObject> buttons = new List<GameObject>();

    void Start()
    {
        selectedButton = null;
    }

}
