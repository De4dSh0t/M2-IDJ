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
    [SerializeField] private int maxRoomXSize;
    [SerializeField] private int maxRoomYSize;
    [SerializeField] private int entranceWidth;
    [SerializeField] private int pathLength;
    [SerializeField] private int corridorDeviationRate; //Controls how much the corridor deviates
    private int removePossibleEntrance; //This will prevent the entrances from being repeated on the same side.
    private int minRoomXSize;
    private int minRoomYSize;

    // Start is called before the first frame update
    void Start()
    {
        //To avoid entrances overlaping room pillars
        minRoomXSize = entranceWidth + 2;
        minRoomYSize = entranceWidth + 2;
        
        //Recursively generates the rooms
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
        room.entranceWidth = entranceWidth;
        room.possibleDirections.Remove(removePossibleEntrance); 
        room.RectangularRoom(x, y, xSize, ySize, nEntrances);
        GeneratePath(room, room.entranceWidth, pathLength);
    }

    /// <summary>
    /// Generates a square to be used when generating path/corridor
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void CorridorSquare(int x, int y)
    {
        for (int tileX = x; tileX < x + entranceWidth; tileX++)
        {
            for (int tileY = y; tileY < y + entranceWidth; tileY++)
            {
                Vector3Int pos = new Vector3Int(tileX, tileY, 0);
                groundMap.SetTile(pos, ground);
            }
        }
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
        int xSize = 0;
        int ySize = 0;
        List<int> possibleTurnDirection = new List<int>();
        int iPathLength = pathLength; //Controls how many iterations can be done
        
        foreach (var entrances in room.entrancePosition)
        {
            direction = entrances.Key; //Entrance Direction
            xEntrance = entrances.Value[0];
            yEntrance = entrances.Value[1];
            int nEntrances = Random.Range(1, 4);
            int tileX = xEntrance;
            int tileY = yEntrance;
            bool first = true; //Used when direction is 2 (right) and 3 (up), because the first tile should be a block ahead
            xSize = Random.Range(minRoomXSize, maxRoomXSize);
            ySize = Random.Range(minRoomYSize, maxRoomYSize);
            
            if (maxNumPaths <= 0)
            {
                nEntrances = 0;
            }

            for (int i = 0; i < pathLength; i++)
            {
                switch (direction)
                {
                    case 1: //Left
                    {
                        possibleTurnDirection = new List<int>() {1, 3, 4};

                        tileX -= entranceWidth;
                        CorridorSquare(tileX, tileY);
                        
                        break;
                    }
                    case 2: //Right
                    {
                        possibleTurnDirection = new List<int>() {2, 3, 4};

                        if (first)
                        {
                            tileX += 1;
                            CorridorSquare(tileX, tileY);
                            first = false;
                        }
                        else
                        {
                            tileX += entranceWidth;
                            CorridorSquare(tileX, tileY);
                        }

                        break;
                    }
                    case 3: //Up
                    {
                        possibleTurnDirection = new List<int>() {1, 2, 3};

                        if (first)
                        {
                            tileY += 1;
                            CorridorSquare(tileX, tileY);
                            first = false;
                        }
                        else
                        {
                            tileY += entranceWidth;
                            CorridorSquare(tileX, tileY);
                        }
                        
                        break;
                    }
                    case 4: //Down
                    {
                        possibleTurnDirection = new List<int>() {1, 2, 4};

                        tileY -= entranceWidth;
                        CorridorSquare(tileX, tileY);
                        
                        break;
                    }
                }
                
                direction = possibleTurnDirection[Random.Range(0, possibleTurnDirection.Count)];

                if (i == pathLength - 1 && maxNumPaths > 0) //Last iteration
                {
                    GenerateRoom(tileX - xSize, tileY - ySize, xSize, ySize, nEntrances);
                }
            }

            maxNumPaths--;
        }
    }
}