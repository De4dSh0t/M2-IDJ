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
            GenerateRoom(room.x, room.y, room.xSize, room.ySize, room.nEntrances);
        }
    }

    /// <summary>
    /// Defines the tiles to be used and Generates the room
    /// </summary>
    private void GenerateRoom(int x, int y, int xSize, int ySize, int nEntrances)
    {
        Room room = new Room
        {
            ground = ground,
            topWall = topWall,
            bottomWall = bottomWall,
            groundMap = groundMap,
            wallMap = wallMap
        };
        room.RectangularRoom(x, y, xSize, ySize, nEntrances);
        GeneratePath(room, room.entranceWidth, 20);
    }

    private void GeneratePath(Room room, int entranceWidth, int pathLength)
    {
        int xEntrance = 0;
        int yEntrance = 0;
        int direction = 0;
        foreach (var kvp in room.entrancePosition)
        {
            direction = kvp.Key;
            xEntrance = kvp.Value[0];
            yEntrance = kvp.Value[1];
            if (direction == 1) //Left
            {
                for (int tileX = xEntrance; tileX > xEntrance - pathLength; tileX--)
                {
                    for (int tileY = yEntrance; tileY < yEntrance + entranceWidth; tileY++)
                    {
                        Vector3Int pos = new Vector3Int(tileX, tileY, 0);
                        groundMap.SetTile(pos, ground);
                    }
                }
            }
            if (direction == 2) //Right
            {
                for (int tileX = xEntrance; tileX < xEntrance + pathLength; tileX++)
                {
                    for (int tileY = yEntrance; tileY < yEntrance + entranceWidth; tileY++)
                    {
                        Vector3Int pos = new Vector3Int(tileX, tileY, 0);
                        groundMap.SetTile(pos, ground);
                    }
                }
            }
            if (direction == 3) //Up
            {
                for (int tileX = xEntrance; tileX < xEntrance + entranceWidth; tileX++)
                {
                    for (int tileY = yEntrance; tileY < yEntrance + pathLength; tileY++)
                    {
                        Vector3Int pos = new Vector3Int(tileX, tileY, 0);
                        groundMap.SetTile(pos, ground);
                    }
                }
            }
            if (direction == 4) //Down
            {
                for (int tileX = xEntrance; tileX < xEntrance + entranceWidth; tileX++)
                {
                    for (int tileY = yEntrance; tileY > yEntrance - pathLength; tileY--)
                    {
                        Vector3Int pos = new Vector3Int(tileX, tileY, 0);
                        groundMap.SetTile(pos, ground);
                    }
                }
            }
        }
    }
}
