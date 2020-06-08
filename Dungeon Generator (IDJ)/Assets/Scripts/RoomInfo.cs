using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to define room info (in the inspector)
/// </summary>
[Serializable]
public class RoomInfo
{
    public string name;
    public int x;
    public int y;
    public int xSize;
    public int ySize;
    [Range(1, 4)] public int nEntrances;
}