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

    /// <summary>
    /// Generates a rectangular room, depending on the ratio and size
    /// </summary>
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
        for (int i = 0; i < nEntrances; i++)
        {
            int direction = Random.Range(1, 4); //1:Left | 2:Right | 3:Top | 4:Bottom
            
            if (direction == 1) //Left
            {
                int eY = Random.Range(y + 1, ySize - 1);
                Vector3Int pos = new Vector3Int(x - 1, eY, 0);
                groundMap.SetTile(pos, ground);
            }
            if (direction == 2) //Right
            {
                int eY = Random.Range(y + 1, ySize - 1);
                Vector3Int pos = new Vector3Int(xSize, eY, 0);
                groundMap.SetTile(pos, ground);
            }
            if (direction == 3) //Top
            {
                int eX = Random.Range(x + 1, xSize - 1);
                Vector3Int pos = new Vector3Int(eX, ySize, 0);
                groundMap.SetTile(pos, ground);
            }
            if (direction == 4) //Bottom
            {
                int eX = Random.Range(x + 1, xSize - 1);
                Vector3Int pos = new Vector3Int(eX, y - 1, 0);
                groundMap.SetTile(pos, ground);
            }
        }
    }
}
