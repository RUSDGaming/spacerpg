using UnityEngine;
using System.Collections;

public class LevelGenInfo : MonoBehaviour {

    public int startX = 99;
    public int startY = 0;

    public int levelWidth = 100;
    public int levelHeight = 100;
    
    public int numFails = 100;
    public int maxRooms = 50;
    public int padding = 1;

    public int minRoomHeight = 5;
    public int maxRoomHeight = 20;
    public int minRoomWidth = 5;
    public int maxRoomWidth = 20;
    public Material mat;
    public int wallTile = 1;
    public int floorTile = 2;

}
