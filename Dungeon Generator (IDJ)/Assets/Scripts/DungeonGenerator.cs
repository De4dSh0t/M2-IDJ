﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        DrawSquare(0, 0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Draws a square of ground tiles, with a specified size
    /// </summary>
    private void DrawSquare(int x, int y, int size)
    {
        for (int tileX = x; tileX < x + size; tileX++)
        {
            for (int tileY = y; tileY < y + size; tileY++)
            {
                Vector3Int pos = new Vector3Int(tileX, tileY, 0);
                groundMap.SetTile(pos, ground);
            }
        }
    }
}