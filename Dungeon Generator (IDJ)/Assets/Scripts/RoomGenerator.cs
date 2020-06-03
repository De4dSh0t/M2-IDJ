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

    /// <summary>
    /// Generates a rectangular room, depending on the ratio and size
    /// </summary>
    public static void RectangularRoom(int x, int y, int xSize, int ySize)
    {
        for (int tileX = x; tileX < x + xSize; tileX++)
        {
            for (int tileY = y; tileY < y + ySize; tileY++)
            {
                Vector3Int pos = new Vector3Int(tileX, tileY, 0);
                groundMap.SetTile(pos, ground);
            }
        }
    }
}
