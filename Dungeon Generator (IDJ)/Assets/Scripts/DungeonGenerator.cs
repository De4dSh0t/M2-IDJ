using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField] private Tile ground;
    [SerializeField] private Tile topWall;
    [SerializeField] private Tile bottomWall;
    [SerializeField] private Tilemap groundMap;
    [SerializeField] private Tilemap wallMap;
    [SerializeField] private int maxNumPaths;
    [SerializeField] private RoomInfo[] roomInfo;
    private int removePossibleEntrance; //This will prevent the entrances from being repeated on the same side.

    // Start is called before the first frame update
    void Start()
    {
        GenerateRoom(0, 0, 5, 5, 2);
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
        room.possibleDirections.Remove(removePossibleEntrance); 
        room.RectangularRoom(x, y, xSize, ySize, nEntrances);
        GeneratePath(room, room.entranceWidth, 20);
    }

    /// <summary>
    /// Generates path/corridor and Generates the next room
    /// </summary>
    /// <param name="room"></param>
    /// <param name="entranceWidth"></param>
    /// <param name="pathLength"></param>
    private void GeneratePath(Room room, int entranceWidth, int pathLength)
    {
        int xEntrance = 0;
        int yEntrance = 0;
        int direction = 0;
        
        foreach (var entrances in room.entrancePosition)
        {
            direction = entrances.Key;
            xEntrance = entrances.Value[0];
            yEntrance = entrances.Value[1];
            int nEntrances = Random.Range(0, 4);
            
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
                maxNumPaths -= nEntrances;
                if (maxNumPaths > 0)
                {
                    removePossibleEntrance = 2; //Because corridor comes from the right side of the next room
                    GenerateRoom(xEntrance - pathLength, yEntrance, 10, 10, nEntrances);
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
                maxNumPaths -= nEntrances;
                if (maxNumPaths > 0)
                {
                    removePossibleEntrance = 1; //Because corridor comes from the left side of the next room
                    GenerateRoom(xEntrance + pathLength, yEntrance, 10, 10, nEntrances);
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
                maxNumPaths -= nEntrances;
                if (maxNumPaths > 0)
                {
                    removePossibleEntrance = 4; //Because corridor comes from the bottom of the next room
                    GenerateRoom(xEntrance, yEntrance + pathLength, 10, 10, nEntrances);
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
                maxNumPaths -= nEntrances;
                if (maxNumPaths > 0)
                {
                    removePossibleEntrance = 3; //Because corridor comes from the top of the next room
                    GenerateRoom(xEntrance, yEntrance - pathLength, 10, 10, nEntrances);
                }
            }
            
            if (maxNumPaths <= 0)
            {
                nEntrances = 0;
            }
        }
    }
}