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
    [SerializeField] private RoomInfo[] roomInfo;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var room in roomInfo)
        {
            GenerateRoom(room.x, room.y, room.xSize, room.ySize);
        }
    }

    /// <summary>
    /// Defines the tiles to be used and Generates the room
    /// </summary>
    private void GenerateRoom(int x, int y, int xSize, int ySize)
    {
        RoomGenerator.ground = ground;
        RoomGenerator.topWall = topWall;
        RoomGenerator.bottomWall = bottomWall;
        RoomGenerator.groundMap = groundMap;
        RoomGenerator.wallMap = wallMap;
        RoomGenerator.RectangularRoom(x, y, xSize, ySize);
    }
}
