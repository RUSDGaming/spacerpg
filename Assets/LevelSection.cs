using UnityEngine;
using System.Collections;
using SpriteTile;
public class LevelSection : MonoBehaviour {

    int sectionWidth = 50;
    int sectionHeight = 50;

    int wallTile = 1;
    float tileScale = 4f;

    [SerializeField]
    public GameObject enemy;
    [SerializeField]
    int numEnimes;

    [SerializeField]
    GameObject asteroid;
    [SerializeField]
    int numAsteroids;

	// Use this for initialization
	void Start () {
        CreateBorder();
        SpawnEnemies();
        SpawnAsteroids();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void SpawnAsteroids()
    {
        for (int i = 0; i < numAsteroids; i++)
        {
           GameObject asteroidInstance = (GameObject) Instantiate(asteroid,
                new Vector3(Random.Range(tileScale, sectionWidth * tileScale - 4),
                            Random.Range(tileScale, sectionHeight * tileScale - 4)),
                Quaternion.identity);
            float randomScale = Random.Range(.5f, 2.5f);
            asteroidInstance.transform.localScale = new Vector3(randomScale,randomScale );
        }

    }

    private void SpawnEnemies()
    {

        for (int i = 0; i < numEnimes; i++){
            Instantiate(enemy, 
                new Vector3(Random.Range(tileScale, sectionWidth * tileScale - 4),
                            Random.Range(tileScale, sectionHeight * tileScale -1 )),
                Quaternion.identity);
        }

    }

    private void CreateBorder()
    {
        Tile.SetCamera();
        Tile.NewLevel(new Int2(sectionWidth, sectionHeight), 0, tileScale, 0.0f, LayerLock.None);


        for (int x = 0; x < sectionWidth; x++)
        {
            Tile.SetTile(new Int2(x, 0), 0, wallTile, true);
        }
        for (int x = 0; x < sectionWidth; x++)
        {
            Tile.SetTile(new Int2(x, sectionHeight - 1), 0, wallTile, true);
        }
        for (int y = 0; y < sectionHeight; y++)
        {
            Tile.SetTile(new Int2(0, y), 0, wallTile, true);
        }
        for (int y = 0; y < sectionHeight; y++)
        {
            Tile.SetTile(new Int2(sectionWidth - 1, y), 0, wallTile, true);
        }

    }
}
