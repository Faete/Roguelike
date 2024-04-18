using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGen : MonoBehaviour
{
    [SerializeField] GameObject startingRoomPrefab;
    [SerializeField] List<GameObject> enemyRoomPrefabs;
    [SerializeField] GameObject fountainRoomPrefab;
    [SerializeField] GameObject upgradeRoomPrefab;
    [SerializeField] GameObject bossRoomPrefab;
    [SerializeField] Transform gridTransform;

    [SerializeField] int numEnemyRooms;
    public List<Room> roomList;

    void Start(){
        roomList = GenerateRooms();
    }

    List<Room> GenerateRooms(){
        List<Room> rooms = new List<Room>();

        transform.position = Vector3.zero;
        string[] directions = {"Up", "Down", "Left", "Right"};
        Dictionary<string, Vector3> dirToWorld = new Dictionary<string, Vector3>{
            {"Up", new Vector3(0f, 9f, 0f)},
            {"Right", new Vector3(17f, 0f, 0f)},
            {"Down", new Vector3(0f, -9f, 0f)},
            {"Left", new Vector3(-17f, 0f, 0f)}
        };
        Dictionary<Vector3, string> worldToDir = new Dictionary<Vector3, string>{
            {new Vector3(0f, 9f, 0f), "Up"},
            {new Vector3(17f, 0f, 0f), "Right"},
            {new Vector3(0f, -9f, 0f), "Down"},
            {new Vector3(-17f, 0f, 0f), "Left"}
        };

        // Starting room
        rooms.Add(new Room{
            roomObject = startingRoomPrefab,
            worldSpacePosition = new Vector3(0f, 0f, 0f),
            roomTag = "Start"
        });

        // Generate rest of rooms
        Room currentRoom = rooms[0];
        while(rooms.Count < numEnemyRooms + 4){
            string dir = directions[Random.Range(0, 4)];
            Vector3 newPos = currentRoom.worldSpacePosition + dirToWorld[dir];
            if(rooms.Exists(x => x.worldSpacePosition == newPos)){
                currentRoom = rooms.Find(x => x.worldSpacePosition == newPos);
                continue;
            }
            rooms.Add(new Room{
                roomObject = enemyRoomPrefabs[Random.Range(0, enemyRoomPrefabs.Count)],
                worldSpacePosition = newPos,
                roomTag = "Enemy"
            });
        }

        // Check neighbors for each room
        foreach(Room room in rooms){
            foreach(string dir in directions){
                if(rooms.Exists(x => x.worldSpacePosition == room.worldSpacePosition + dirToWorld[dir]))
                    room.neighbors.Add(dir, rooms.Find(x => x.worldSpacePosition == room.worldSpacePosition + dirToWorld[dir]));
            }
        }

        // Generate Special rooms
        List<Room> validRooms = rooms.FindAll(x => x.roomTag != "Start");
        validRooms.Sort((x,y) => x.neighbors.Count.CompareTo(y.neighbors.Count));
        validRooms[0].roomTag = "Boss";
        validRooms[0].roomObject = bossRoomPrefab;
        validRooms[1].roomTag = "Upgrade";
        validRooms[1].roomObject = upgradeRoomPrefab;
        validRooms[2].roomTag = "Fountain";
        validRooms[2].roomObject = fountainRoomPrefab;

        // Instantiate rooms
        foreach(Room room in rooms){
            Instantiate(room.roomObject, room.worldSpacePosition, Quaternion.identity, gridTransform);
        }
        return rooms;
    }
}
