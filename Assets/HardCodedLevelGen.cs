using UnityEngine;
using System.Collections;
using System;

public class HardCodedLevelGen : LevelGeneratorScript {

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public override void Init()
    {
        width = 11;
        height = 11;

        base.Init();        
        MakeMap();
        StartCoroutine(GenerateMap());
    }

    public override void MakeMap()
    {
        tiles[0, 0] = TileType.ASTEROID;
        tiles[0, 1] = TileType.ASTEROID;
        tiles[0, 2] = TileType.ASTEROID;
        tiles[0, 3] = TileType.ASTEROID;
        tiles[0, 4] = TileType.ASTEROID;
        tiles[0, 5] = TileType.ASTEROID;
        tiles[0, 6] = TileType.ASTEROID;
        tiles[0, 7] = TileType.ASTEROID;
        tiles[0, 8] = TileType.ASTEROID;
        tiles[0, 9] = TileType.ASTEROID;
        tiles[0, 10] = TileType.ASTEROID;

        tiles[1, 0] = TileType.ASTEROID;
        tiles[1, 1] = TileType.ENEMY;
        tiles[1, 2] = TileType.VOID;
        tiles[1, 3] = TileType.ENEMY;
        tiles[1, 4] = TileType.ASTEROID;
        tiles[1, 5] = TileType.ASTEROID;
        tiles[1, 6] = TileType.ASTEROID;
        tiles[1, 7] = TileType.ENEMY;
        tiles[1, 8] = TileType.VOID;
        tiles[1, 9] = TileType.ENEMY;
        tiles[1, 10] = TileType.ASTEROID;

        tiles[2, 0] = TileType.ASTEROID;
        tiles[2, 1] = TileType.VOID;
        tiles[2, 2] = TileType.STATION;
        tiles[2, 3] = TileType.VOID;
        tiles[2, 4] = TileType.ASTEROID;
        tiles[2, 5] = TileType.ENEMY;
        tiles[2, 6] = TileType.ASTEROID;
        tiles[2, 7] = TileType.VOID;
        tiles[2, 8] = TileType.STATION;
        tiles[2, 9] = TileType.VOID;
        tiles[2, 10] = TileType.ASTEROID;

        tiles[3, 0] = TileType.ASTEROID;
        tiles[3, 1] = TileType.ENEMY;
        tiles[3, 2] = TileType.VOID;
        tiles[3, 3] = TileType.ENEMY;
        tiles[3, 4] = TileType.ASTEROID;
        tiles[3, 5] = TileType.ASTEROID;
        tiles[3, 6] = TileType.ASTEROID;
        tiles[3, 7] = TileType.ENEMY;
        tiles[3, 8] = TileType.VOID;
        tiles[3, 9] = TileType.ENEMY;
        tiles[3, 10] = TileType.ASTEROID;

        tiles[4, 0] = TileType.ASTEROID;
        tiles[4, 1] = TileType.ASTEROID;
        tiles[4, 2] = TileType.ASTEROID;
        tiles[4, 3] = TileType.ASTEROID;
        tiles[4, 4] = TileType.VOID;
        tiles[4, 5] = TileType.VOID;
        tiles[4, 6] = TileType.VOID;
        tiles[4, 7] = TileType.ASTEROID;
        tiles[4, 8] = TileType.ASTEROID;
        tiles[4, 9] = TileType.ASTEROID;
        tiles[4, 10] = TileType.ASTEROID;

        tiles[5, 0] = TileType.ASTEROID;
        tiles[5, 1] = TileType.ASTEROID;
        tiles[5, 2] = TileType.ENEMY;
        tiles[5, 3] = TileType.ASTEROID;
        tiles[5, 4] = TileType.VOID;
        tiles[5, 5] = TileType.SUN;
        tiles[5, 6] = TileType.VOID;
        tiles[5, 7] = TileType.BOSS;
        tiles[5, 8] = TileType.ENEMY;
        tiles[5, 9] = TileType.ASTEROID;
        tiles[5, 10] = TileType.PLAYER;

        tiles[6, 0] = TileType.ASTEROID;
        tiles[6, 1] = TileType.ASTEROID;
        tiles[6, 2] = TileType.ASTEROID;
        tiles[6, 3] = TileType.ASTEROID;
        tiles[6, 4] = TileType.VOID;
        tiles[6, 5] = TileType.VOID;
        tiles[6, 6] = TileType.VOID;
        tiles[6, 7] = TileType.ASTEROID;
        tiles[6, 8] = TileType.ASTEROID;
        tiles[6, 9] = TileType.ASTEROID;
        tiles[6, 10] = TileType.ASTEROID;

        tiles[7, 0] = TileType.ASTEROID;
        tiles[7, 1] = TileType.ENEMY;
        tiles[7, 2] = TileType.VOID;
        tiles[7, 3] = TileType.ENEMY;
        tiles[7, 4] = TileType.ASTEROID;
        tiles[7, 5] = TileType.ASTEROID;
        tiles[7, 6] = TileType.ASTEROID;
        tiles[7, 7] = TileType.ENEMY;
        tiles[7, 8] = TileType.VOID;
        tiles[7, 9] = TileType.ENEMY;
        tiles[7, 10] = TileType.ASTEROID;

        tiles[8, 0] = TileType.ASTEROID;
        tiles[8, 1] = TileType.VOID;
        tiles[8, 2] = TileType.STATION;
        tiles[8, 3] = TileType.VOID;
        tiles[8, 4] = TileType.ASTEROID;
        tiles[8, 5] = TileType.ENEMY;
        tiles[8, 6] = TileType.ASTEROID;
        tiles[8, 7] = TileType.VOID;
        tiles[8, 8] = TileType.STATION;
        tiles[8, 9] = TileType.VOID;
        tiles[8, 10] = TileType.ASTEROID;

        tiles[9, 0] = TileType.ASTEROID;
        tiles[9, 1] = TileType.ENEMY;
        tiles[9, 2] = TileType.VOID;
        tiles[9, 3] = TileType.ENEMY;
        tiles[9, 4] = TileType.ASTEROID;
        tiles[9, 5] = TileType.ASTEROID;
        tiles[9, 6] = TileType.ASTEROID;
        tiles[9, 7] = TileType.ENEMY;
        tiles[9, 8] = TileType.VOID;
        tiles[9, 9] = TileType.ENEMY;
        tiles[9, 10] = TileType.ASTEROID;

        tiles[10, 0] = TileType.ASTEROID;
        tiles[10, 1] = TileType.ASTEROID;
        tiles[10, 2] = TileType.ASTEROID;
        tiles[10, 3] = TileType.ASTEROID;
        tiles[10, 4] = TileType.ASTEROID;
        tiles[10, 5] = TileType.ASTEROID;
        tiles[10, 6] = TileType.ASTEROID;
        tiles[10, 7] = TileType.ASTEROID;
        tiles[10, 8] = TileType.ASTEROID;
        tiles[10, 9] = TileType.ASTEROID;
        tiles[10, 10] = TileType.ASTEROID;


    }
}
