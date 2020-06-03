using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomGenerator : MonoBehaviour
{
    public static Tile ground;
    public static Tile topWall;
    public static Tile bottomWall;
    public static Tilemap groundMap;
    public static Tilemap wallMap;
    public static int nEntrances = 1;
    public static int entranceWidth = 3;

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
        List<int> possibleDirections = new List<int>() {1, 2, 3, 4}; //Allows no repetition of directions when generating entrances
        int currentX = x;
        int currentY = y;
        
        for (int i = 0; i < nEntrances; i++)
        {
            int direction = possibleDirections[Random.Range(0, possibleDirections.Count)];
            
            if (direction == 1) //Left
            {
                if (entranceWidth == ySize - 2) //Forces the entrance to be positioned between the two pillars
                {
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(x - 1, ++y, 0);
                        groundMap.SetTile(pos, ground);
                    }
                }
                else if (entranceWidth < ySize - 2)
                {
                    int eY = Random.Range(y + entranceWidth, ySize - entranceWidth); //Chooses the nº of "entranceWidth" blocks to use as a entrance (avoids "pillars")
                    
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(x - 1, eY++, 0);
                        groundMap.SetTile(pos, ground);
                    }
                }
                else
                {
                    Debug.Log("Entrance width is greater than the distance between the pillars.");
                }

                possibleDirections.Remove(1);
            }
            if (direction == 2) //Right
            {
                if (entranceWidth == ySize - 2) //Forces the entrance to be positioned between the two pillars
                {
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(x + xSize, ++y, 0);
                        groundMap.SetTile(pos, ground);
                    }
                }
                else if (entranceWidth < ySize - 2)
                {
                    int eY = Random.Range(y + entranceWidth, ySize - entranceWidth); //Chooses the nº of "entranceWidth" blocks to use as a entrance (avoids "pillars")
                    
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(x + xSize, eY++, 0);
                        groundMap.SetTile(pos, ground);
                    }
                }
                else
                {
                    Debug.Log("Entrance width is greater than the distance between the pillars.");
                }
                
                possibleDirections.Remove(2);
            }
            if (direction == 3) //Top
            {
                if (entranceWidth == xSize - 2) //Forces the entrance to be positioned between the two pillars
                {
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(++x, y + ySize, 0);
                        groundMap.SetTile(pos, ground);
                    }
                }
                else if (entranceWidth < xSize - 2)
                {
                    int eX = Random.Range(x + entranceWidth, xSize - entranceWidth); //Chooses the nº of "entranceWidth" blocks to use as a entrance (avoids "pillars")
                    
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(eX++, y + ySize, 0);
                        groundMap.SetTile(pos, ground);
                    }
                }
                else
                {
                    Debug.Log("Entrance width is greater than the distance between the pillars.");
                }
                
                possibleDirections.Remove(3);
            }
            if (direction == 4) //Bottom
            {
                if (entranceWidth == xSize - 2) //Forces the entrance to be positioned between the two pillars
                {
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(++x, y - 1, 0);
                        groundMap.SetTile(pos, ground);
                    }
                }
                else if (entranceWidth < xSize - 2)
                {
                    int eX = Random.Range(x + entranceWidth, xSize - entranceWidth); //Chooses the nº of "entranceWidth" blocks to use as a entrance (avoids "pillars")
                    
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(eX++, y - 1, 0);
                        groundMap.SetTile(pos, ground);
                    }
                }
                else
                {
                    Debug.Log("Entrance width is greater than the distance between the pillars.");
                }
                
                possibleDirections.Remove(4);
            }
        }
    }
}
