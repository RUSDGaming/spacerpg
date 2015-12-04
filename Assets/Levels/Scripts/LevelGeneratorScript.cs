using UnityEngine;
using System.Collections;

using Game.Events;
using System;

public abstract class LevelGeneratorScript : MonoBehaviour
{

    public enum TileType
    {
        VOID, ASTEROID, ENEMY, BOSS, PLAYER, SUN, STATION, PLAYER_STATION, PORTAL
    }

    public TileType[,] tiles;
    public GameObject[,] tileGameObjects;

    // Vector2 startPos;

    #region TilePrefabs
    [SerializeField]
    protected MapEdgeManager mapEdgeManager;
    [SerializeField]
    protected RandomTileScript voidSpace;
    [SerializeField]
    protected AsteroidTileScript asteroidSpaceTile;
    [SerializeField]
    protected EnemyTileScript enemySpaceTile;
    [SerializeField]
    protected BossTileScript bossSpaceTile;
    [SerializeField]
    protected PlayerSpawnTileScript playerSpawnTile;
    [SerializeField]
    protected SunTileScript sunSpawnTile;
    [SerializeField]
    protected SpaceStationTileScript spaceStationTile;
    [SerializeField]
    protected PlayerSpaceStationTileScript playerSpaceStationTile;
    [SerializeField]
    protected PortalTileScript portalTile;

    #endregion

    public int maxAsteroids = 20;
    public int maxEnemies = 10;
    public Transform levelParent;

    public Vector3 playerSpawnPosition;

    protected float chunkSize = 80f;
    protected float halfChunk = 40f;
    public  int width = 5;
    public int height = 5;


    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CleanRegion()
    {
        foreach (Transform item in levelParent)
        {
            Destroy(item.gameObject);

        }
        
    }

    public virtual void Init()
    {
        

        halfChunk = chunkSize / 2;
        tiles = new TileType[width, height];
        tileGameObjects = new GameObject[width, height];

        // set cameras to track the player
        // set the cameras max, min, tracking axis, startPos
        mapEdgeManager.mapStart = transform.position;
        mapEdgeManager.mapWidth = width;
        mapEdgeManager.mapHeight = height;
        mapEdgeManager.orthographicCamSize = chunkSize / 2;
        mapEdgeManager.SetUpCameras();
        mapEdgeManager.SetUpBorders();

    }
    public abstract void MakeMap();

    protected IEnumerator GenerateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                switch (tiles[x, y])
                {
                    case TileType.VOID:
                        GenerateVoidSpace(x, y);
                        break;
                    case TileType.ASTEROID:
                        GenerateAsteroidSpace(x, y, maxAsteroids);
                        break;
                    case TileType.ENEMY:
                        GenerateEnemySpace(x, y, maxEnemies);
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
                    case TileType.STATION:
                        GenerateStationSpace(x, y);
                        break;
                    case TileType.PLAYER_STATION:
                        GeneratePlayerStationSpace(x, y);
                        break;
                    case TileType.PORTAL:
                        GeneratePortalSpace(x, y);
                        break;
                    default:
                        break;
                }
                yield return new WaitForEndOfFrame();
            }
        }

        
        DoneLoadingMap();
    }

    public void DoneLoadingMap()
    {
        LevelFinishedLoadingEventArgs args = new LevelFinishedLoadingEventArgs();        
        args.startPos = playerSpawnPosition;
        args.region = this;

        GameEventSystem.PublishEvent(typeof(LevelLoadedSubscriber), args);

    }

    /// <summary>
    /// This will load a tile of the passed type into the array,
    ///  this should be called before you fill out the rest of the map
    /// </summary>
    /// <param name="tile"></param>
    protected void LoadTileInUniqueSpace( TileType tile)
    {
        if(width < 3 || height < 3)
        {
            Debug.LogError("Your level is to small make the width or height > 3");
            return;
        }
        int randX = UnityEngine.Random.Range(1, width - 1);
        int randY = UnityEngine.Random.Range(1, height - 1);
        while (true)
        {

            randX = UnityEngine.Random.Range(1, width - 1);
            randY = UnityEngine.Random.Range(1, height - 1);

            if (tiles[randX, randY] == TileType.VOID)
               break;
          
        }
        tiles[randX, randY] = tile;

    }

    #region Tile Generatetors

    protected void GenerateVoidSpace(int x, int y)
    {
        RandomTileScript ts = Instantiate(voidSpace);
        ts.transform.SetParent(levelParent);
        ts.transform.localPosition = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        ts.width = chunkSize;
        ts.height = chunkSize;
        ts.x = x;
        ts.y = y;
        ts.levelGeneratorScript = this;
        ts.init();
        tileGameObjects[x, y] = ts.gameObject;
        ts.gameObject.SetActive(false);
    }

    protected void GenerateEnemySpace(int x, int y, int maxEnemies)
    {
        EnemyTileScript ts = Instantiate(enemySpaceTile);
        ts.transform.SetParent(levelParent);
        ts.transform.localPosition = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        ts.height = chunkSize;
        ts.width = chunkSize;
        ts.numEnemies = maxEnemies;
        ts.x = x;
        ts.y = y;
        ts.init();
        ts.levelGeneratorScript = this;
        tileGameObjects[x, y] = ts.gameObject;
        ts.gameObject.SetActive(false);

    }

    protected void GenerateAsteroidSpace(int x, int y, int maxAsteroids)
    {
        AsteroidTileScript ts = Instantiate(asteroidSpaceTile);
        ts.transform.SetParent(levelParent);
        ts.transform.localPosition = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        ts.width = chunkSize;
        ts.height = chunkSize;
        ts.numAsteroids = maxAsteroids;
        ts.x = x;
        ts.y = y;
        ts.levelGeneratorScript = this;
        ts.init();
        tileGameObjects[x, y] = ts.gameObject;
        ts.gameObject.SetActive(false);
    }

    protected void GenerateBossSpace(int x, int y)
    {
        BossTileScript ts = Instantiate(bossSpaceTile);
        ts.transform.SetParent(levelParent);
        ts.transform.localPosition = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        ts.width = chunkSize;
        ts.height = chunkSize;
        ts.x = x;
        ts.y = y;
        ts.levelGeneratorScript = this;
        ts.init();
        tileGameObjects[x, y] = ts.gameObject;
        ts.gameObject.SetActive(false);

    }

    protected void GeneratePlayerSpawnSpace(int x, int y)
    {
        PlayerSpawnTileScript ts = Instantiate(playerSpawnTile);
        ts.transform.SetParent(levelParent);
        ts.transform.localPosition = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        ts.width = chunkSize;
        ts.height = chunkSize;
        ts.x = x;
        ts.y = y;
        ts.levelGeneratorScript = this;
        ts.init();
        tileGameObjects[x, y] = ts.gameObject;
    }

    protected void GenerateSunSpace(int x, int y)
    {
        SunTileScript ts = Instantiate(sunSpawnTile);
        ts.transform.SetParent(levelParent);
        ts.transform.localPosition = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        ts.width = chunkSize;
        ts.height = chunkSize;
        ts.x = x;
        ts.y = y;
        ts.levelGeneratorScript = this;
        ts.init();
        tileGameObjects[x, y] = ts.gameObject;
        ts.gameObject.SetActive(false);
    }

    protected void GenerateStationSpace(int x, int y)
    {
        SpaceStationTileScript ts = Instantiate(spaceStationTile);
        ts.transform.SetParent(levelParent);
        ts.transform.localPosition = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        ts.width = chunkSize;
        ts.height = chunkSize;
        ts.x = x;
        ts.y = y;
        ts.levelGeneratorScript = this;
        ts.init();
        tileGameObjects[x, y] = ts.gameObject;
        ts.gameObject.SetActive(false);
    }

    protected void GeneratePlayerStationSpace(int x, int y)
    {
        PlayerSpaceStationTileScript ts = Instantiate(playerSpaceStationTile);
        ts.transform.SetParent(levelParent);
        ts.transform.localPosition = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        ts.width = chunkSize;
        ts.height = chunkSize;
        ts.x = x;
        ts.y = y;
        ts.levelGeneratorScript = this;
        ts.init();
        tileGameObjects[x, y] = ts.gameObject;
        ts.gameObject.SetActive(false);
    }

    protected void GeneratePortalSpace(int x, int y)
    {
        PortalTileScript ts = Instantiate(portalTile);
        ts.transform.SetParent(levelParent);
        ts.transform.localPosition = new Vector2(x * chunkSize + halfChunk, y * chunkSize + halfChunk);
        ts.width = chunkSize;
        ts.height = chunkSize;
        ts.x = x;
        ts.y = y;
        ts.levelGeneratorScript = this;
        ts.init();
        tileGameObjects[x, y] = ts.gameObject;
        ts.gameObject.SetActive(false);
    }

    #endregion

    public void PlayerEnteredTile(int x, int y)
    {
      //  Debug.Log("Player entered tile x: " + x + " y: " + y);
        int xStart = x -3;
        int yStart = y - 3;
        int xEnd = x + 3;
        int yEnd = y + 3;
        for (int i = xStart; i <= xEnd; i++)
        {
            for (int j = yStart; j <= yEnd; j++)
            {


                        int tempI = i;
                        int tempJ = j;
                        if (i >= width)
                            tempI = i - width;
                        if (i < 0)
                            tempI  = i + width;
                        if (j >= height)
                            tempJ = j - height;
                        if (j < 0)
                            tempJ = j + height;

                try
                {
                    
                    if(width > 5 && ( i == xStart || j == yStart || j == yEnd || i == xEnd))
                    {
                        
                       // Debug.Log("xstart: " + xStart + " x:" + tempI + " ystart: " + yStart + " y: " + tempJ);
                        tileGameObjects[tempI, tempJ].SetActive(false);

                    }
                    else
                    {
                        tileGameObjects[tempI, tempJ].SetActive(true);
                    }
                }catch(Exception e)
                {
                    
                    Debug.LogError("temp I : "+tempI + " temp J : " + tempJ + " " + e);
                }
            }

        }

    }
}
