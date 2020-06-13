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
    [SerializeField] private int maxRoomGenerations; //Limits the number of generations of the Room generator. When it reaches the max, rooms will be generated with zero entrances
    [SerializeField] private int maxRoomXSize;
    [SerializeField] private int maxRoomYSize;
    [SerializeField] private int entranceWidth;
    [SerializeField] private int minPathLength;
    [SerializeField] private int maxPathLength;
    [SerializeField] private bool corridorDeviation;
    [SerializeField] private int corridorDeviationRate; //Controls at what percentage of the corridor it deviates. The higher the number, the less it deviates.
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
        GenerateRoom(0, 0, Random.Range(entranceWidth + 2, maxRoomXSize), Random.Range(entranceWidth + 2, maxRoomYSize), Random.Range(1, 5));
        
        //Generates the walls for all the empty cells next to the "groundMap" boundaries
        GenerateWalls();
    }

    /// <summary>
    /// Defines the tiles to be used and Generates the room
    /// </summary>
    private void GenerateRoom(int x, int y, int xSize, int ySize, int nEntrances)
    {
        //Initializes a new room
        Room room = new Room
        {
            ground = ground,
            groundMap = groundMap
        };
        room.entranceWidth = entranceWidth;
        room.possibleDirections.Remove(removePossibleEntrance); //This will prevent the next room to generate an entrance on the same side as the corridor
        
        //Generates the room (rectangular room)
        room.RectangularRoom(x, y, xSize, ySize, nEntrances);

        //Generates the path (for every entrance that this room has)
        int pathLength = Random.Range(minPathLength, maxPathLength);
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
        List<int> possibleTurnDirection = new List<int>();

        foreach (var entrances in room.entrancePosition)
        {
            int direction = entrances.Key; //Direction of the entrance
            int xEntrance = entrances.Value[0]; //First Entrance Tile (x)
            int yEntrance = entrances.Value[1]; //First Entrance Tile (y)
            int nEntrances = 0; //Number of entrances of the next room
            int tileX = xEntrance;
            int tileY = yEntrance;
            int xSize = Random.Range(minRoomXSize, maxRoomXSize);
            int ySize = Random.Range(minRoomYSize, maxRoomYSize);
            int deviationRate = pathLength * (corridorDeviationRate / 100); //Defines when to deviate
            bool first = true; //Used when direction is 2 (right) and 3 (up), because the first tile should be a block ahead

            //Chooses the number of entrances of the next room (using probability)
            float random = Random.value;
            if (random <= 0.15f)
            {
                nEntrances = 0;
            }
            else if(random > 0.15f)
            {
                nEntrances = Random.Range(1, 4);
            }

            //Generates the corridor/path (by spawning x number of "CorridorSquares()")
            for (int i = 0; i < pathLength; i++)
            {
                //Used to try another direction when a cell is in use
                List<int> tryDirection = new List<int>() {1, 2, 3, 4}; 
                
                //Secondary variable used to store the next tile position (in order to check if the cell next to it is in use)
                int checkTileX; 
                int checkTileY;
                
                switch (direction)
                {
                    case 1: //Left
                    {
                        possibleTurnDirection = new List<int>() {1, 3, 4};
                        checkTileX = tileX;
                        checkTileY = tileY;

                        if (groundMap.HasTile(new Vector3Int(checkTileX - entranceWidth, checkTileY, 0)) || 
                            groundMap.HasTile(new Vector3Int(checkTileX - entranceWidth, checkTileY + 1, 0))) //Check if the next cell (on the left side) has a tile
                        {
                            tryDirection.Remove(1);
                            goto default;
                        }

                        tileX -= entranceWidth;
                        CorridorSquare(tileX, tileY);
                        
                        break;
                    }
                    case 2: //Right
                    {
                        possibleTurnDirection = new List<int>() {2, 3, 4};

                        if (first)
                        {
                            checkTileX = tileX + 1;
                            checkTileY = tileY;
                            
                            if (groundMap.HasTile(new Vector3Int(checkTileX + entranceWidth, checkTileY, 0)) ||
                                groundMap.HasTile(new Vector3Int(checkTileX + entranceWidth, checkTileY + 1, 0))) //Check if the next cell (on the right side) has a tile
                            {
                                tryDirection.Remove(2);
                                goto default;
                            }
                            
                            tileX += 1;
                            CorridorSquare(tileX, tileY);
                            first = false;
                        }
                        else
                        {
                            checkTileX = tileX + entranceWidth;
                            checkTileY = tileY;
                            
                            if (groundMap.HasTile(new Vector3Int(checkTileX + entranceWidth, checkTileY, 0)) ||
                                groundMap.HasTile(new Vector3Int(checkTileX + entranceWidth, checkTileY + 1, 0))) //Check if the next cell (on the right side) has a tile
                            {
                                tryDirection.Remove(2);
                                goto default;
                            }
                            
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
                            checkTileX = tileX;
                            checkTileY = tileY + 1;
                            
                            if (groundMap.HasTile(new Vector3Int(checkTileX, checkTileY + entranceWidth, 0)) ||
                                groundMap.HasTile(new Vector3Int(checkTileX + 1, checkTileY + entranceWidth, 0))) //Check if the next cell (on the top) has a tile
                            {
                                tryDirection.Remove(3);
                                goto default;
                            }
                            
                            tileY += 1;
                            CorridorSquare(tileX, tileY);
                            first = false;
                        }
                        else
                        {
                            checkTileX = tileX - entranceWidth;
                            checkTileY = tileY;
                            
                            if (groundMap.HasTile(new Vector3Int(checkTileX, checkTileY + entranceWidth, 0)) ||
                                groundMap.HasTile(new Vector3Int(checkTileX + 1, checkTileY + entranceWidth, 0))) //Check if the next cell (on the top) has a tile
                            {
                                tryDirection.Remove(3);
                                goto default;
                            }
                            
                            tileY += entranceWidth;
                            CorridorSquare(tileX, tileY);
                        }
                        
                        break;
                    }
                    case 4: //Down
                    {
                        possibleTurnDirection = new List<int>() {1, 2, 4};
                        checkTileX = tileX;
                        checkTileY = tileY - entranceWidth;

                        if (groundMap.HasTile(new Vector3Int(checkTileX, checkTileY - entranceWidth, 0)) ||
                            groundMap.HasTile(new Vector3Int(checkTileX + 1, checkTileY - entranceWidth, 0))) //Check if the next cell (on the top) has a tile
                        {
                            tryDirection.Remove(4);
                            goto default;
                        }
                        
                        tileY -= entranceWidth;
                        CorridorSquare(tileX, tileY);
                        
                        break;
                    }
                    default: //Try another direction (when a cell is in use)
                    {
                        foreach (var index in tryDirection)
                        {
                            if (index == 1) goto case 1;
                            if (index == 2) goto case 2;
                            if (index == 3) goto case 3;
                            if (index == 4) goto case 4;
                        }
                        break;
                    }
                }
                
                if (corridorDeviation && deviationRate == i)
                {
                     direction = possibleTurnDirection[Random.Range(0, possibleTurnDirection.Count)];
                     deviationRate += deviationRate;
                }
            }

            if (maxRoomGenerations <= 0)
            {
                nEntrances = 0;
            }

            if(direction == 1)
            {
                removePossibleEntrance = 2; //Because corridor comes from the right side of the next room
                GenerateRoom(tileX - xSize, Random.Range(tileY - ySize + entranceWidth + 1, tileY - 1), xSize, ySize, nEntrances);
            }
            if(direction == 2) 
            {
                removePossibleEntrance = 1; //Because corridor comes from the left side of the next room
                GenerateRoom(tileX, Random.Range(tileY - ySize + entranceWidth + 1, tileY - entranceWidth), xSize, ySize, nEntrances);
            }
            if (direction == 3)
            {
                removePossibleEntrance = 4; //Because corridor comes from the bottom of the next room
                GenerateRoom(Random.Range(tileX - xSize + entranceWidth + 1, tileX - 1), tileY, xSize, ySize, nEntrances);
            }
            if(direction == 4) 
            {
                removePossibleEntrance = 3; //Because corridor comes from the top of the next room
                GenerateRoom(Random.Range(tileX - xSize + entranceWidth + 1, tileX - 1), tileY - ySize, xSize, ySize, nEntrances);
            }

            maxRoomGenerations--;
        }
    }
    
    /// <summary>
    /// Generates the walls, taking into account the "groundMap" boundaries
    /// </summary>
    private void GenerateWalls()
    {
        BoundsInt bounds = groundMap.cellBounds; //Saves the boundaries of a the "groundMap" tilemap
        
        for (int xMap = bounds.xMin - 1; xMap < bounds.xMax + 1; xMap++)
        {
            for (int yMap = bounds.yMin - 1; yMap < bounds.yMax + 1; yMap++)
            {
                Vector3Int currentPos = new Vector3Int(xMap, yMap, 0); //Vector3Int of the current position
                Vector3Int topPosition = new Vector3Int(xMap, yMap - 1, 0); //Vector3Int of the top position
                Vector3Int bottomPosition = new Vector3Int(xMap, yMap + 1, 0); //Vector3Int of the bottom position

                if (!groundMap.HasTile(currentPos)) //Checks if the cell ("tile") at the "currentPos" is empty (null)
                {
                    if (groundMap.HasTile(topPosition)) //Checks if the cell below is used (to spawn the "topWall" tile)
                    {
                        wallMap.SetTile(currentPos, topWall);
                    }
                    else if (groundMap.HasTile(bottomPosition)) //Checks if the cell above is used (to spawn the "bottomWall" tile)
                    {
                        wallMap.SetTile(currentPos, bottomWall);
                    }
                }
            }
        }
    }
}