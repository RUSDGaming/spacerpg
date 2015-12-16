using UnityEngine;
using System.Collections;
using System;

public class PlayerHomeLevelGen : LevelGeneratorScript {

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void Init()
    {
        base.Init();

        MakeMap();
        StartCoroutine(GenerateMap());
    }

    public override void MakeMap()
    {

        LoadTileInUniqueSpace(TileType.PLAYER_STATION);
        LoadTileInUniqueSpace(TileType.PLAYER);
        LoadTileInUniqueSpace(TileType.PORTAL);
        for (int x = 0; x < width ; x++)
        {
            for (int y = 0; y < height ; y++)
            {
                if(tiles[x,y] == TileType.VOID)
                {
                    tiles[x, y] = TileType.ASTEROID;
                }
            }
        }
    }

   

}
