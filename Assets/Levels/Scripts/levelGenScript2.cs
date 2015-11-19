using UnityEngine;
using System.Collections;
using SpriteTile;

public class levelGenScript2 : LevelGeneratorScript
{
    


    public override void Init()
    {
        base.Init();

        
        
        MakeMap();
        StartCoroutine(GenerateMap());
    }


    public override void MakeMap()
    {

        TileType current = TileType.PLAYER;
        bool looping = true;
        while (looping)
        {
            int randX = Random.Range(1, width - 1);
            int randY = Random.Range(1, height - 1);

            if (tiles[randX, randY] != TileType.VOID)
                continue;

            switch (current)
            {
                case TileType.PLAYER:
                    tiles[randX, randY] = TileType.PLAYER;
                    current = TileType.BOSS;
                   // Debug.Log("Player Tile");
                    break;
                case TileType.BOSS:
                    tiles[randX, randY] = TileType.BOSS;
                    current = TileType.SUN;
                   // Debug.Log("Boss Tile");
                    break;
                case TileType.SUN:
                    tiles[randX, randY] = TileType.SUN;
                    current = TileType.VOID;
                    //Debug.Log("Sun");
                    break;
                default:
                    looping = false;
                    break;
            }
        }

        // allow the outer loop to be void spaces
        for (int x = 1; x < width-1; x++)
        {
            for (int y = 1; y < height-1; y++)
            {
                if (tiles[x, y] != TileType.VOID)
                    continue;

                int randomNum = Random.Range(0, 101);
                if (randomNum <= 100 && randomNum > 90)
                {
                    tiles[x, y] = TileType.STATION;
                    // 10 % 
                }else if (randomNum <= 90 && randomNum > 70 )
                {
                    // 20%
                    tiles[x, y] = TileType.ENEMY;   
                }
                else if (randomNum <= 70 && randomNum > 30)
                {
                    // 40 % 
                    tiles[x, y] = TileType.ASTEROID;
                }
            }
        }
    }


   



}
