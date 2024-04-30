using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public GameObject roomObject;
    public Vector3 worldSpacePosition;
    public bool isExplored;
    public Dictionary<string, Room> neighbors = new Dictionary<string, Room>();
    public string roomTag;
}
