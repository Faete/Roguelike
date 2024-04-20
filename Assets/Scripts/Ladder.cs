using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private MapManager mapManager;

    void Start(){
        mapManager = FindObjectOfType<MapManager>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")) mapManager.NextLevel();
    }
}
