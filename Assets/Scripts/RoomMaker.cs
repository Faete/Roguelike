using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMaker
{
    public GameObject roomObject;
    public Vector3 worldSpacePosition;
    public bool isExplored;
    public Dictionary<string, RoomMaker> neighbors = new Dictionary<string, RoomMaker>();
    public string roomTag;
}
