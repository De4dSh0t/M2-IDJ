using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private Tile ground;
    [SerializeField] private Tile topWall;
    [SerializeField] private Tile bottomWall;
    [SerializeField] private Tilemap groundMap;
    [SerializeField] private Tilemap wallMap;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateRooms();
    }

    /// <summary>
    /// Generates the number of predefined rooms
    /// </summary>
    private void GenerateRooms()
    {
        RoomGenerator.ground = ground;
        RoomGenerator.topWall = topWall;
        RoomGenerator.bottomWall = bottomWall;
        RoomGenerator.groundMap = groundMap;
        RoomGenerator.wallMap = wallMap;
        RoomGenerator.RectangularRoom(0, 0, 5, 5);
    }
}
