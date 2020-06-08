using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Room
{
    public Tile ground;
    public Tile topWall;
    public Tile bottomWall;
    public Tilemap groundMap;
    public Tilemap wallMap;
    public int entranceWidth = 3;
    public Dictionary<int, int[]> entrancePosition; //Dictionary(direction, int[] {xEntrance, yEntrance}). Saves entrance direction and first tile position
    public List<int> possibleDirections = new List<int>() {1, 2, 3, 4}; //Allows no repetition of directions when generating entrances

    /// <summary>
    /// Generates a rectangular room, depending on the ratio and size
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="xSize"></param>
    /// <param name="ySize"></param>
    public void RectangularRoom(int x, int y, int xSize, int ySize, int nEntrances)
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
        GenerateEntrance(x, y, xSize, ySize, nEntrances);
    }

    /// <summary>
    /// Chooses tiles/positions to generate the specified number of entrances of a room
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="xSize"></param>
    /// <param name="ySize"></param>
    private void GenerateEntrance(int x, int y, int xSize, int ySize, int nEntrances)
    {
        entrancePosition = new Dictionary<int, int[]>();
        int currentX = x;
        int currentY = y;

        for (int i = 0; i < nEntrances; i++)
        {
            int direction = possibleDirections[Random.Range(0, possibleDirections.Count)];
            int entranceX = 0;
            int entranceY = 0;
            
            if (direction == 1) //Left
            {
                Vector3Int[] positions = new Vector3Int[entranceWidth]; //Saves all tiles position of the current entrance
                
                if (entranceWidth == ySize - 2) //Forces the entrance to be positioned between the two pillars
                {
                    entranceX = currentX - 1;
                    entranceY = currentY + 1;
                    entrancePosition.Add(1, new []{entranceX, entranceY});
                    
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(currentX - 1, ++currentY, 0);
                        groundMap.SetTile(pos, ground);
                        positions[j] = pos;
                    }
                }
                else if (entranceWidth < ySize - 2)
                {
                    int eY = Random.Range(currentY + entranceWidth, (ySize + currentY) - entranceWidth); //Chooses the nº of "entranceWidth" blocks to use as a entrance (avoids "pillars")
                    entranceX = currentX - 1;
                    entranceY = eY;
                    entrancePosition.Add(1, new []{entranceX, entranceY});
                    
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
            }
            if (direction == 2) //Right
            {
                Vector3Int[] positions = new Vector3Int[entranceWidth]; //Saves all tiles position of the current entrance
                
                if (entranceWidth == ySize - 2) //Forces the entrance to be positioned between the two pillars
                {
                    entranceX = currentX + xSize;
                    entranceY = currentY + 1;
                    entrancePosition.Add(2, new []{entranceX, entranceY});
                    
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(currentX + xSize, ++currentY, 0);
                        groundMap.SetTile(pos, ground);
                        positions[j] = pos;
                    }
                }
                else if (entranceWidth < ySize - 2)
                {
                    int eY = Random.Range(currentY + entranceWidth, (ySize + currentY) - entranceWidth); //Chooses the nº of "entranceWidth" blocks to use as a entrance (avoids "pillars")
                    entranceX = currentX + xSize;
                    entranceY = eY;
                    entrancePosition.Add(2, new []{entranceX, entranceY});
                    
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
            }
            if (direction == 3) //Top
            {
                Vector3Int[] positions = new Vector3Int[entranceWidth]; //Saves all tiles position of the current entrance
                
                if (entranceWidth == xSize - 2) //Forces the entrance to be positioned between the two pillars
                {
                    entranceX = currentX + 1;
                    entranceY = currentY + ySize;
                    entrancePosition.Add(3, new []{entranceX, entranceY});
                    
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(++currentX, currentY + ySize, 0);
                        groundMap.SetTile(pos, ground);
                        positions[j] = pos;
                    }
                }
                else if (entranceWidth < xSize - 2)
                {
                    int eX = Random.Range(currentX + entranceWidth, (xSize + currentX) - entranceWidth); //Chooses the nº of "entranceWidth" blocks to use as a entrance (avoids "pillars")
                    entranceX = eX;
                    entranceY = currentY + ySize;
                    entrancePosition.Add(3, new []{entranceX, entranceY});
                    
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
            }
            if (direction == 4) //Bottom
            {
                Vector3Int[] positions = new Vector3Int[entranceWidth]; //Saves all tiles position of the current entrance
                
                if (entranceWidth == xSize - 2) //Forces the entrance to be positioned between the two pillars
                {
                    entranceX = currentX + 1;
                    entranceY = currentY - 1;
                    entrancePosition.Add(4, new []{entranceX, entranceY});
                    
                    for (int j = 0; j < entranceWidth; j++)
                    {
                        Vector3Int pos = new Vector3Int(++currentX, currentY - 1, 0);
                        groundMap.SetTile(pos, ground);
                        positions[j] = pos;
                    }
                }
                else if (entranceWidth < xSize - 2)
                {
                    int eX = Random.Range(currentX + entranceWidth, (xSize + currentX) - entranceWidth); //Chooses the nº of "entranceWidth" blocks to use as a entrance (avoids "pillars")
                    entranceX = eX;
                    entranceY = currentY - 1;
                    entrancePosition.Add(4, new []{entranceX, entranceY});
                    
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
            }

            currentX = x;
            currentY = y;
        }
    }
}
