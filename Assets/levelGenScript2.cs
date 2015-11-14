using UnityEngine;
using System.Collections;
using SpriteTile;

public class levelGenScript2 : MonoBehaviour {



    Vector2 startPos;
    

    
    public int width;
    public int height;
    float chunkSize = 80f;

    [SerializeField]    MapEdgeManager mapEdgeManager;
    [SerializeField]    AsteroidTileScript asteroidSpaceTile;
    [SerializeField]    EnemyTileScript enemySpaceTile;

	// Use this for initialization
	void Start () {
        // set cameras to track the player
        // set the cameras max, min, tracking axis, startPos

        mapEdgeManager.mapStart = new Vector2(0, 0);
        mapEdgeManager.mapWidth = width;
        mapEdgeManager.mapHeight = height;
        mapEdgeManager.orthographicCamSize = chunkSize / 2;
        mapEdgeManager.SetUpCameras();
        mapEdgeManager.SetUpBorders();

        GenerateMap();
	}

    void GenerateMap()
    {
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(x == 0 || x == width-1 || y == 0 || y == height-1)
                {
                    AsteroidTileScript ast =  Instantiate(asteroidSpaceTile);
                    ast.transform.position = new Vector2(x * chunkSize, y * chunkSize);
                    ast.width = chunkSize;
                    ast.height = chunkSize;
                    //ast.levelParent = levelParentTransform;
                    ast.init();
                }
                else if (Random.Range(0f,1f) > 0.5f)
                {
                   //Debug.Log("waaaaah11111");
                    EnemyTileScript est = Instantiate(enemySpaceTile);
                    est.transform.position = new Vector2(x * chunkSize, y * chunkSize);
                    est.height = chunkSize;
                    est.width = chunkSize;
                    est.init();
                }
            }

        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
