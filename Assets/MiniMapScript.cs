using UnityEngine;
using System.Collections;

using SpriteTile;

public class MiniMapScript : MonoBehaviour {

    public Camera miniMapCamera;
	// Use this for initialization
	void Start () {
        //InitMap(500, 500, null);
        Tile.SetCamera(miniMapCamera);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void InitMap(int width, int height, LevelGeneratorScript.TileType[,] tiles)
    {


       // if(tiles)
        //Tile.EraseLevel();
        Tile.NewLevel(new Int2(width, height), 0, 4f, 0.0f, LayerLock.None);
        Tile.SetLayerPosition(transform.position);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                

                switch (tiles[x, y])
                {
                    case LevelGeneratorScript.TileType.VOID:
                        Tile.SetTile(new Int2(x, y), 0, 5, true);
                        break;
                    case LevelGeneratorScript.TileType.ASTEROID:
                        Tile.SetTile(new Int2(x, y), 0, 7, true);
                        break;
                    case LevelGeneratorScript.TileType.ENEMY:
                        Tile.SetTile(new Int2(x, y), 0, 8, true);
                        break;
                    case LevelGeneratorScript.TileType.BOSS:
                        Tile.SetTile(new Int2(x, y), 0, 6, true);
                        break;
                    case LevelGeneratorScript.TileType.PLAYER:
                        Tile.SetTile(new Int2(x, y), 0, 10, true);
                        break;
                    case LevelGeneratorScript.TileType.SUN:
                        Tile.SetTile(new Int2(x, y), 0, 13, true);
                        break;
                    case LevelGeneratorScript.TileType.STATION:
                        Tile.SetTile(new Int2(x, y), 0, 12, true);
                        break;
                    case LevelGeneratorScript.TileType.PLAYER_STATION:
                        Tile.SetTile(new Int2(x, y), 0, 9, true);
                        break;
                    case LevelGeneratorScript.TileType.PORTAL:
                        Tile.SetTile(new Int2(x, y), 0, 11, true);
                        break;
                    default:
                        break;
                }
            }
        }


        // tell it where to make the map
        // tell it what the map looks like
        // tell it where to point the camera
        // tell the camear the offset....

    }
}
