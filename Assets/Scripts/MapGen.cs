using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGen : MonoBehaviour
{
    [SerializeField] Tilemap floorTilemap;
    [SerializeField] Tilemap wallTilemap;
    [SerializeField] Tilemap doorTileMap;
    [SerializeField] Tile floorTile;
    [SerializeField] Tile wallTile;
    [SerializeField] Tile doorTile;
}
