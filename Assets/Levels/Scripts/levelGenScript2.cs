using UnityEngine;
using System.Collections;
using SpriteTile;

public class levelGenScript2 : MonoBehaviour
{



    Vector2 startPos;
    [SerializeField]
    Transform levelParent;


    public int width;
    public int height;
    float chunkSize = 80f;
    float halfChunk = 40f;

    enum TileType
    {
        VOID, ASTEROID, ENEMY, BOSS, PLAYER, SUN
    }

    TileType[,] tiles;

    [SerializeField]
    MapEdgeManager mapEdgeManager;
    [SerializeField]
    RandomTileScript voidSpace;
    [SerializeField]
    AsteroidTileScript asteroidSpaceTile;
    [SerializeField]
    EnemyTileScript enemySpaceTile;
    [SerializeField]
    BossTileScript bossSpaceTile;
    [SerializeField]
    PlayerSpawnTileScript playerSpawnTile;
    [SerializeField]
    SunTileScript sunSpawnTile;

    // Use this for initialization
    void Start()
    {
        halfChunk = chunkSize / 2;
        tiles = new TileType[width, height];
        // set cameras to track the player
        // set the cameras max, min, tracking axis, startPos

        mapEdgeManager.mapStart = new Vector2(0, 0);
        mapEdgeManager.mapWidth = width;
        mapEdgeManager.mapHeight = height;
        mapEdgeManager.orthographicCamSize = chunkSize / 2;
        mapEdgeManager.SetUpCameras();
        mapEdgeManager.SetUpBorders();

        makeMap();
        StartCoroutine(GenerateMap());
    }

    public void makeMap()
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
                    Debug.Log("Player Tile");
                    break;
                case TileType.BOSS:
                    tiles[randX, randY] = TileType.BOSS;
                    current = TileType.SUN;
                    Debug.Log("Boss Tile");
                    break;
                case TileType.SUN:
                    tiles[randX, randY] = TileType.SUN;
                    current = TileType.VOID;
                    Debug.Log("Sun");
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

                int randomNum = Random.Range(0, 11);
                if (randomNum > 9)
                {
                    // 10 % 
                    tiles[x, y] = TileType.ENEMY;   
                }
                else if (randomNum > 5)
                {
                    // 40 % 
                    tiles[x, y] = TileType.ASTEROID;
                }

            }
        }



    }

    IEnumerator GenerateMap()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                switch (tiles[x,y])
                {
                    case TileType.VOID:
                        GenerateVoidSpace(x, y);
                        break;
                    case TileType.ASTEROID:
                        GenerateAsteroidSpace(x, y);
                        break;
                    case TileType.ENEMY:
                        GenerateEnemySpace(x, y);
                        break;
                    case TileType.BOSS:
                        GenerateBossSpace(x, y);
                        break;
                    case TileType.PLAYER:
                        GeneratePlayerSpawnSpace(x, y);
                        break;
                    case TileType.SUN:
                        GenerateSunSpace(x, y);
                        break;
                    default:
                        break;
                }
                yield return new WaitForEndOfFrame();
            }
        } 
    }

    #region Tile Generatetors

    void GenerateVoidSpace(int x, int y)
    {
        RandomTileScript rst = Instantiate(voidSpace);
        rst.transform.position = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        rst.width = chunkSize;
        rst.height = chunkSize;
        rst.transform.SetParent(levelParent);
        rst.init();
    }

    void GenerateEnemySpace(int x, int y)
    {
        EnemyTileScript est = Instantiate(enemySpaceTile);
        est.transform.position = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        est.height = chunkSize;
        est.width = chunkSize;
        est.transform.SetParent(levelParent);
        est.init();

    }

    void GenerateAsteroidSpace(int x, int y)
    {
        AsteroidTileScript ast = Instantiate(asteroidSpaceTile);
        ast.transform.position = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        ast.width = chunkSize;
        ast.height = chunkSize;
        ast.transform.SetParent(levelParent);
        ast.init();
    }

    void GenerateBossSpace(int x, int y)
    {
        BossTileScript ts = Instantiate(bossSpaceTile);
        ts.transform.position = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        ts.width = chunkSize;
        ts.height = chunkSize;
        ts.transform.SetParent(levelParent);
        ts.init();

    }

    void GeneratePlayerSpawnSpace(int x, int y)
    {
        PlayerSpawnTileScript ts = Instantiate(playerSpawnTile);
        ts.transform.position = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        ts.width = chunkSize;
        ts.height = chunkSize;
        ts.transform.SetParent(levelParent);
        ts.init();
    }

    void GenerateSunSpace(int x, int y)
    {
        SunTileScript ts = Instantiate(sunSpawnTile);
        ts.transform.position = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        ts.width = chunkSize;
        ts.height = chunkSize;
        ts.transform.SetParent(levelParent);
        ts.init();
    }

    #endregion



}
