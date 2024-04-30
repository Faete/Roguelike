using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public Savedata savedata;

    void Start(){
        string json = File.ReadAllText(Application.persistentDataPath + "/savedata.json");
        if(File.Exists(Application.persistentDataPath + "/savedata.json")) savedata = JsonUtility.FromJson<Savedata>(json);
        playerTransform.SendMessage("Load", savedata);
        mapGen = GetComponent<MapGen>();
        currentLevel = savedata.level;
        rooms = mapGen.GenerateRooms(enemyRoomCounts[0], enemyRoomPrefabs[0].elements, currentLevel == maxLevel - 1);
        mapGen.PlaceRooms(rooms);
    }

    public void NextLevel(){
        ++savedata.level;
        Save();
        ++currentLevel;
        mapGen.CleanUp();
        playerTransform.position = Vector3.zero;
        rooms = mapGen.GenerateRooms(enemyRoomCounts[currentLevel], enemyRoomPrefabs[currentLevel].elements, currentLevel == maxLevel - 1);
        mapGen.PlaceRooms(rooms);
    }

    public void Save(){
        playerTransform.SendMessage("Save", savedata);
        File.WriteAllText(Application.persistentDataPath + "/savedata.json", JsonUtility.ToJson(savedata));
    }
}
