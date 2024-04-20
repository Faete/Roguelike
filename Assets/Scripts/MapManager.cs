using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private MapGen mapGen;
    public int currentLevel;
    public int maxLevel;
    [SerializeField] List<GameObjectList> enemyRoomPrefabs;
    [SerializeField] List<int> enemyRoomCounts;
    [SerializeField] Transform playerTransform;
    public List<RoomMaker> rooms;

    void Start(){
        mapGen = GetComponent<MapGen>();
        currentLevel = 0;
        rooms = mapGen.GenerateRooms(enemyRoomCounts[0], enemyRoomPrefabs[0].elements, currentLevel == maxLevel);
        mapGen.PlaceRooms(rooms);
    }

    public void NextLevel(){
        ++currentLevel;
        mapGen.CleanUp();
        rooms = mapGen.GenerateRooms(enemyRoomCounts[currentLevel], enemyRoomPrefabs[currentLevel].elements, currentLevel == maxLevel);
        mapGen.PlaceRooms(rooms);
        playerTransform.position = Vector3.zero;
    }
}
