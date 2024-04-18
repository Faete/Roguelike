using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public GameObject roomObject;
    public List<GameObject> enemies = new List<GameObject>();
    public int roomNumber;
    public Vector3 worldSpacePosition;
    public bool explored = false;
    public Dictionary<string, Room> neighbors = new Dictionary<string, Room>();
    public string roomTag;
}
