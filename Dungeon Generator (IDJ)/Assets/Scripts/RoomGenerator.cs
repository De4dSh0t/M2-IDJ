using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class RoomGenerator : MonoBehaviour
{
    public static Tile ground;
    public static Tile topWall;
    public static Tile bottomWall;
    public static Tilemap groundMap;
    public static Tilemap wallMap;
    public static int nEntrances = 2;
    public static int entranceWidth = 3;
    public static List<Vector3Int[]> entrancesPositions; //Saves all Vector3Int (positions) of the entrances tiles

    /// <summary>
    /// Generates a rectangular room, depending on the ratio and size
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="xSize"></param>
    /// <param name="ySize"></param>
    public static void RectangularRoom(int x, int y, int xSize, int ySize)
    {
        //Generates floor tiles
        for (int tileX = x; tileX < x + xSize; tileX++)
        {
            for (int tileY = y; tileY < y + ySize; tileY++)
            {
                Vector3Int pos = new Vector3Int(tileX, tileY, 0);
                groundMap.SetTile(pos, ground);
            }
        }
        
        //Select and generate entrances
        GenerateEntrance(x, y, xSize, ySize);
    }

    /// <summary>
    /// Chooses tiles/positions to generate the specified number of entrances of a room
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="xSize"></param>
    /// <param name="ySize"></param>
    private static void GenerateEntrance(int x, int y, int xSize, int ySize)
    {
        entrancesPositions = new List<Vector3Int[]>(); //Saves all Vector3Int (positions) of the entrances tiles
        List<int> possibleDirections = new List<int>() {1, 2, 3, 4}; //Allows no repetition of directions when generating entrances
        int currentX = x;
        int currentY = y;
        
        for (int i = 0; i < nEntrances; i++)
        {
            int direction = possibleDirections[Random.Range(0, possibleDirections.Count)];
            
            if (direction == 1) //Left
            {
                Vector3Int[] positions = new Vector3Int[entranceWidth]; //Saves all tiles position of the current entrance
                
                if (entranceWidth == ySize - 2) //Forces the entrance to be positioned between the two pillars
                {
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(currentX - 1, ++currentY, 0);
                        groundMap.SetTile(pos, ground);
                        positions[j] = pos;
                    }
                }
                else if (entranceWidth < ySize - 2)
                {
                    int eY = Random.Range(currentY + entranceWidth, ySize - entranceWidth); //Chooses the nº of "entranceWidth" blocks to use as a entrance (avoids "pillars")
                    
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(currentX - 1, eY++, 0);
                        groundMap.SetTile(pos, ground);
                        positions[j] = pos;
                    }
                }
                else
                {
                    Debug.Log("Entrance width is greater than the distance between the pillars.");
                }

                possibleDirections.Remove(1);
                entrancesPositions.Add(positions);
            }
            if (direction == 2) //Right
            {
                Vector3Int[] positions = new Vector3Int[entranceWidth]; //Saves all tiles position of the current entrance
                
                if (entranceWidth == ySize - 2) //Forces the entrance to be positioned between the two pillars
                {
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(currentX + xSize, ++currentY, 0);
                        groundMap.SetTile(pos, ground);
                        positions[j] = pos;
                    }
                }
                else if (entranceWidth < ySize - 2)
                {
                    int eY = Random.Range(currentY + entranceWidth, ySize - entranceWidth); //Chooses the nº of "entranceWidth" blocks to use as a entrance (avoids "pillars")
                    
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(currentX + xSize, eY++, 0);
                        groundMap.SetTile(pos, ground);
                        positions[j] = pos;
                    }
                }
                else
                {
                    Debug.Log("Entrance width is greater than the distance between the pillars.");
                }
                
                possibleDirections.Remove(2);
                entrancesPositions.Add(positions);
            }
            if (direction == 3) //Top
            {
                Vector3Int[] positions = new Vector3Int[entranceWidth]; //Saves all tiles position of the current entrance
                
                if (entranceWidth == xSize - 2) //Forces the entrance to be positioned between the two pillars
                {
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(++currentX, currentY + ySize, 0);
                        groundMap.SetTile(pos, ground);
                        positions[j] = pos;
                    }
                }
                else if (entranceWidth < xSize - 2)
                {
                    int eX = Random.Range(currentX + entranceWidth, xSize - entranceWidth); //Chooses the nº of "entranceWidth" blocks to use as a entrance (avoids "pillars")
                    
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(eX++, currentY + ySize, 0);
                        groundMap.SetTile(pos, ground);
                        positions[j] = pos;
                    }
                }
                else
                {
                    Debug.Log("Entrance width is greater than the distance between the pillars.");
                }
                
                possibleDirections.Remove(3);
                entrancesPositions.Add(positions);
            }
            if (direction == 4) //Bottom
            {
                Vector3Int[] positions = new Vector3Int[entranceWidth]; //Saves all tiles position of the current entrance
                
                if (entranceWidth == xSize - 2) //Forces the entrance to be positioned between the two pillars
                {
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(++currentX, currentY - 1, 0);
                        groundMap.SetTile(pos, ground);
                        positions[j] = pos;
                    }
                }
                else if (entranceWidth < xSize - 2)
                {
                    int eX = Random.Range(currentX + entranceWidth, xSize - entranceWidth); //Chooses the nº of "entranceWidth" blocks to use as a entrance (avoids "pillars")
                    
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(eX++, currentY - 1, 0);
                        groundMap.SetTile(pos, ground);
                        positions[j] = pos;
                    }
                }
                else
                {
                    Debug.Log("Entrance width is greater than the distance between the pillars.");
                }
                
                possibleDirections.Remove(4);
                entrancesPositions.Add(positions);
            }

            currentX = x;
            currentY = y;
        }
    }
}
