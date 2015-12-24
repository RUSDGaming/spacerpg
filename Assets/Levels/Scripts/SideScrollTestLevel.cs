using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class SideScrollTestLevel : MonoBehaviour
{

    [SerializeField]    BackPortalScript backPortal;

    [SerializeField]    GameObject staticAsteroid;
    public int staticAsteroidChance = 10;
    [SerializeField]    GameObject enemy;
    public int enemyChance = 0;
    [SerializeField]    BaseShip movingAsteroid;

    [SerializeField]    GameObject wall;
    [SerializeField]    Transform DeleterWall;
    [SerializeField]    Transform backWall;

    [SerializeField]    int levels = 20;
    [SerializeField]    int currentLevel = 0;
    int waveWidth = 32;

    public SaveGameInfo saveGameInfo;

    BoxCollider2D collider;

    [SerializeField]    float deltaYWall = 48;


    Vector2  initColliderOffset;
    
    // Use this for initialization
    void Awake()
    {
        saveGameInfo = new SaveGameInfo();
         
        collider = GetComponent<BoxCollider2D>();
        initColliderOffset = collider.offset;

    }

    void Start() { }
  

    public void FixedUpdate()
    {
       // Debug.Log("wtf");
        Vector2 lerpTo = (Vector2) transform.position + collider.offset;
        lerpTo.x -= waveWidth * 6.5f;
        backWall.position = Vector3.Lerp(backWall.position, lerpTo, Time.fixedDeltaTime  * .5f);
    }

    public void ClearLevel()
    {

        foreach(Transform child in transform)
        { 
            Destroy(child.gameObject);
        }
    }
    public void Init()
    {
        ClearLevel();
        collider.offset = initColliderOffset;
        Vector2 lerpTo = (Vector2)transform.position + collider.offset;
        lerpTo.x -= waveWidth * 6.5f;
        currentLevel = 0;
        backWall.position = lerpTo;
        BackWall();
        CreateSpawnSpot();
        CreateSpawnSpot();
        CreateSpawnSpot();
        CreateSpawnSpot();



        //CreateEnemyWave();
        //CreateEnemyWave();
        //CreateEnemyWave();
        CreateWave();
        CreateWave();
        CreateWave();
        CreateWave();
        CreateWave();
        CreateWave();
        CreateWave();
        
        
    }

    public void CreateSpawnSpot()
    {
        currentLevel++;
        CreateWall();
    }
    public void CreateEnemyWave()
    {
        currentLevel++;

        GameObject go = Instantiate(enemy);
        go.transform.position = new Vector3(transform.position.x + currentLevel * waveWidth, transform.position.y, transform.position.z);


        GameObject go2 = Instantiate(enemy);
        go2.transform.position = new Vector3(transform.position.x + currentLevel * waveWidth, transform.position.y + 8, transform.position.z);


        GameObject go3 = Instantiate(enemy);
        go3.transform.position = new Vector3(transform.position.x + currentLevel * waveWidth, transform.position.y - 8, transform.position.z);

        CreateWall();

    }
    void AsteroidWave(int num, float deltay)
    {
        currentLevel++;
        BaseShip assteroid = Instantiate(movingAsteroid);
        assteroid.transform.position = new Vector3(transform.position.x + currentLevel * waveWidth, transform.position.y, transform.position.z);
        assteroid.transform.SetParent(transform);
        assteroid.SetActualStats(saveGameInfo,true);
        CreateWall();
    }

    void VerticalWave(int num, float deltaY)
    {

        float spaceBetween = deltaY * 2 / (num+1);
       // Debug.Log(spaceBetween);
        currentLevel++;

        for (int i = 1; i <= num; i++)
        {
            GameObject go = Instantiate(enemy);
            go.transform.position = new Vector3(transform.position.x + currentLevel * waveWidth, 
                transform.position.y - deltaY +  i * spaceBetween  ,
                transform.position.z);
            go.transform.SetParent(this.transform);
        }

        CreateWall();

    }


    public void CreateWall()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject wall1 = Instantiate(wall);
            wall1.transform.position = new Vector3(transform.position.x + currentLevel * waveWidth + i * 8, transform.position.y - deltaYWall, transform.position.z);
            wall1.transform.SetParent(this.transform);
            GameObject wall2 = Instantiate(wall);
            wall2.transform.position = new Vector3(transform.position.x + currentLevel * waveWidth + i * 8, transform.position.y + deltaYWall, transform.position.z);
            wall2.transform.SetParent(this.transform);
        }
        collider.offset = new Vector2(transform.position.x + currentLevel * waveWidth - 12 * waveWidth, collider.offset.y);

    }

    public void portalWave()
    {
        currentLevel++;
        backPortal.gameObject.SetActive(true);
        backPortal.transform.position = new Vector3(transform.position.x + currentLevel * waveWidth,transform.position.y,transform.position.z);
        CreateWall();
    }

    public void RandomAsteroid()
    {
        GameObject go = Instantiate(staticAsteroid);
        float randY = UnityEngine.Random.Range(-deltaYWall + 5, deltaYWall - 5);
        float randX = UnityEngine.Random.Range(-16, 16);
        go.transform.position = new Vector3(transform.position.x + currentLevel * waveWidth + randX, transform.position.y + randY,transform.position.z);
        go.transform.SetParent(transform);
    }
    public void RandomEnemy()
    {
        GameObject go = Instantiate(enemy);
        float randY = UnityEngine.Random.Range(-deltaYWall + 5, deltaYWall - 5);
        float randX = UnityEngine.Random.Range(-16, 16);
        go.transform.position = new Vector3(transform.position.x + currentLevel * waveWidth + randX, transform.position.y + randY, transform.position.z);
        go.transform.SetParent(transform);
    }

    public void finalWave()
    {
       // portalWave();

        for (int i = 0; i < 13; i++)
        {
            GameObject wall1 = Instantiate(wall);
            wall1.transform.position = new Vector3(transform.position.x + 32 + (currentLevel ) * waveWidth , transform.position.y - deltaYWall + 8* i, transform.position.z);
            wall1.transform.SetParent(this.transform);
            
        }
    }
    void BackWall()
    {
        for (int i = 0; i < 13; i++)
        {
            GameObject wall1 = Instantiate(wall);
            wall1.transform.position = new Vector3(transform.position.x  + (currentLevel) * waveWidth, transform.position.y - deltaYWall + 8 * i, transform.position.z);
            wall1.transform.SetParent(this.transform);

        }
    }


    public void CreateWave()
    {
        currentLevel++;
        List<Action> pot = new List<Action>() ; 

        for(int i = 0; i < staticAsteroidChance; i++)
        {
            pot.Add(RandomAsteroid);
        }
        for (int i = 0; i < enemyChance; i++)
        {
            pot.Add(RandomEnemy);
        }


        int randomSpawn = UnityEngine.Random.Range(0,pot.Count);

        for (int i = 0; i < 10  ; i++)
        {
            int rand = UnityEngine.Random.Range(0, pot.Count);
            pot[rand].Invoke();            
            
        }
        enemyChance++;
        //staticAsteroidChance++;
        CreateWall();



    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //CreateEnemyWave();
            if(currentLevel < levels)
            {
                CreateWave();
                //VerticalWave(currentLevel / 3, deltaYWall - 2);
            }else if (!backPortal.gameObject.activeInHierarchy)
            {
                portalWave();
            }
            else
            {
                finalWave();
            }
            //  collider.offset = new Vector2(collider.offset.x + 16, collider.offset.y);
           
        }
    }
}
