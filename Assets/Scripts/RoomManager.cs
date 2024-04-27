using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    GameObject door;
    List<EnemyController> enemies = new List<EnemyController>();
    List<TurretController> turrets = new List<TurretController>();
    public bool isBossRoom;
    List<AIPath> paths;
    bool roomScanned = false;
    

    void Start(){
        enemies = GetComponentsInChildren<EnemyController>().ToList();
        turrets = GetComponentsInChildren<TurretController>().ToList();
        paths = GetComponentsInChildren<AIPath>().ToList();
        door = transform.GetChild(2).gameObject;
    }

    void Update(){
        enemies.RemoveAll(x => x == null);
        paths.RemoveAll(x => x == null);
        if(enemies.Count == 0) transform.GetChild(2).gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Camera.main.transform.position = transform.position;
            if(enemies.Count > 0 || isBossRoom){
                if(!roomScanned){
                    foreach(EnemyController enemy in enemies) enemy.enabled = true;
                    foreach(AIPath path in paths) path.enabled = true;
                    door.SetActive(true);
                    AstarPath.active.data.gridGraph.center = transform.position;
                    AstarPath.active.Scan();
                    roomScanned = true;
                }
            }
        }
    }
}
