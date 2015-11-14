using UnityEngine;
using System.Collections;
using System;
using SpriteTile;


public class HallwayLevelGenerator : MonoBehaviour {


    [SerializeField]
    int levelWidth;
    [SerializeField]
    int levelHeight;

    int wallTile = 1;

    float tileScale = 4f;

    public Transform player;

    



	// Use this for initialization
	void Start () {
        GenerateLevel();
        player.position = new Vector3(levelWidth / 2 * tileScale, 10 * tileScale);
	}

    private void GenerateLevel()
    {
        Tile.SetCamera();
        Tile.NewLevel(new Int2(levelWidth, levelHeight), 0, tileScale, 0.0f, LayerLock.None);


        for( int x  = 0; x < levelWidth; x++)
        {
            Tile.SetTile(new Int2(x, 0),0,wallTile,true);
        }
        for (int x = 0; x < levelWidth; x++)
        {
            Tile.SetTile(new Int2(x, levelHeight -1), 0, wallTile, true);
        }
        for (int y = 0; y < levelHeight; y++)
        {
            Tile.SetTile(new Int2(0, y), 0, wallTile, true);
        }
        for (int y = 0; y < levelHeight ; y++)
        {
            Tile.SetTile(new Int2(levelWidth - 1, y), 0, wallTile, true);
        }

    }
}
