using UnityEngine;
using System.Collections;
using SpriteTile;
using Game.Events;



public class LevelGenScript : MonoBehaviour
{
  


    [SerializeField]    Transform levelTransform;
    [SerializeField] GameObject Boss;

    [SerializeField]
    Transform playerRespawn;
    
    Int2 startPoint = new Int2(-1,-1);
    Int2 endPoint = new Int2(-1,-1);

    [SerializeField]    TextAsset bossArena;


    public TextAsset myLevel;
    // Use this for initialization
    void Start()
    {
        Tile.SetCamera();    
        Tile.LoadLevel(myLevel);

        PlayerController[] players = FindObjectsOfType<PlayerController>();

        foreach (PlayerController player in players)
        {
            player.gameObject.transform.position = playerRespawn.position;
        }
    }

    public IEnumerator GenerateRandomLevel(LevelGenInfo info)
    {
        //Tile.NewLevel(new Int2(width, height), 0, 4, 0, LayerLock.None);

        Debug.Log("Genarating a random Room");
        int rooms = 0;
        int fails = 0;
        while(fails < info.numFails && rooms < info.maxRooms)
        {

            float failPercent =  1 - ((float) fails / (float) info.numFails );
           // Debug.Log(failPercent);
            float maxW = info.minRoomWidth + info.maxRoomWidth * failPercent;
            float maxH = info.minRoomHeight+ info.maxRoomHeight * failPercent;

            int maxWidthInt = Mathf.CeilToInt(maxW);
            int maxHeightInt = Mathf.CeilToInt(maxH);
            

            if (CreateRoom(Random.Range(info.minRoomWidth, maxWidthInt), Random.Range(info.minRoomHeight, maxHeightInt),info))
            {
                rooms++;
                
                yield return new WaitForSeconds(.0f);
            }
            else
            {
                fails++;
                yield return new WaitForSeconds(.0f) ;
            }
        }
        Debug.Log("rooms Created" + rooms);
        Debug.Log("times Failed" + fails);

        yield return new WaitForSeconds(.1f);
        createBorder(info);
        yield return new WaitForSeconds(.01f);
        //fillEmpty();

        LevelFinishedLoadingEventArgs args = new LevelFinishedLoadingEventArgs();
        args.startPos = Tile.MapToWorldPosition(startPoint);
        args.endPos = Tile.MapToWorldPosition(endPoint);
        Debug.Log("startpos"+args.startPos);
        Debug.Log("end pos"+args.endPos);

        GameEventSystem.PublishEvent(typeof(LevelLoadedSubscriber), args);
    }


    bool CreateRoom(int w, int h,LevelGenInfo info)
    {
       // Debug.Log("Trying to create a room");
        int startX = Random.Range(info.startX, info.startX + info.levelWidth  - w);
        int startY = Random.Range(info.startY, info.startY + info.levelHeight - h);
        return CreateBox(startX, startY, w, h,info);
    }


    bool CreateBox(int startX, int startY, int w, int h,LevelGenInfo info)
    {
        
        if (DoesCollide(startX, startY, w, h,info))
        {
            //Debug.Log("The room Collieded With Something");
            return false;
        }
        
        for (int x = startX; x < startX + w; x++)
        {
            for (int y = startY; y < startY + h; y++)
            {

                if (x == startX || x == startX + w - 1 || y == startY || y == startY + h - 1)
                {
                    // Debug.Log("hat");
                    Tile.SetTile(new Int2(x, y), 0, info.wallTile, true);

                }
                else
                {
                    if (Tile.GetTile(new Int2(x, y)).tile != info.wallTile)
                    {
                        //Debug.Log("setting floor tile");
                        Tile.SetTile(new Int2(x, y), 0, info.floorTile, false);
                        if(startPoint.x == -1 )
                        {                          
                            // the start point will be the first room generated.
                            startPoint = new Int2(startX + w / 2, startY + h / 2);
                            Debug.Log("Setting Start Point : " + startPoint);
                           
                        }
                         // the endpoint will be the last room generated
                         endPoint= new Int2(startX + w / 2, startY + h / 2);
                            
                        
                    }
                }

            }
        }
       
        // generates a door, or  a hole really
        while (true)
        {

            int randX = 0;
            int randY = 0;

            int horizontal = Random.Range(0, 2);
            int side = Random.Range(0, 2);
            if (horizontal == 0)
            {
                randX = Random.Range(startX + 1, startX + w - 1);               
                if (side == 0)
                    randY = startY;
                else
                    randY = startY + h -1;
            }
            else
            {
                randY = Random.Range(startY + 1, startY + h - 1);
                if (side == 0)
                    randX = startX;                    
                else
                    randX = startX + w -1;
            }

            Tile.SetTile(new Int2(randX, randY), 0, info.floorTile, false);

            break;

        }


        return true;

    }

    bool DoesCollide(int startX, int startY, int w, int h,LevelGenInfo info)
    {
       // Debug.Log("StartX:" + startX + " info.startX:" + info.startX);
       // Debug.Log("StartY:" + startY + " info.startY:" + info.startY);
        


        if (startX <= info.startX + info.padding
            || startY <= info.startY + info.padding
            || startX + w + info.padding >= info.levelWidth +  info.startX
            || startY + h + info.padding >= info.levelHeight + info.startY)
        {
           // Debug.Log("hit the edge of the map");
            return true;

        }        

        for (int x = startX - info.padding; x < startX + w + info.padding; x++)
        {
            for (int y = startY - info.padding; y < startY + h + info.padding; y++)
            {
                // if a tile is not null then something is there...
                if (Tile.GetTile(new Int2(x, y)).tile != -1)
                {
                    return true;
                }

            }
        }
        return false;
    }

    void createBorder(LevelGenInfo info)
    {

        for (int x = info.startX; x < info.startX +info.levelWidth; x++)
        {
            for (int y = info.startY; y < info.startY +  info.levelHeight; y++)
            {
              
                if (x == info.startX || x == info.levelWidth + info.startX - 1 || y == info.startY || y == info.levelHeight - 1 + info.startY)
                {
                    Tile.SetTile(new Int2(x, y), 0, info.wallTile, true);
                }
            }

        }
    }

    void fillEmpty(LevelGenInfo info)
    {

        for (int x = info.startX; x < + info.startX +  info.levelWidth; x++)
        {
            for (int y = info.startY; y < info.startY +  info.levelHeight; y++)
            {
                if(Tile.GetTile(new Int2(x, y)).tile == -1)
                Tile.SetTile(new Int2(x, y), 0, info.floorTile, false);
            }

        }

    }


    
    public IEnumerator AddRandomLevelToCurrent(LevelGenInfo info)
    {
        //StartCoroutine(GenerateRandomLevel(info));
        StartCoroutine(CreateBoxLevel(info));
        yield return new WaitForSeconds(0);

        
    }

    public void GenerateLevel(LevelGenInfo info) {
        switch (info.levelType)
        {
            case LevelGenInfo.LevelType.BOSS:
                StartCoroutine(CreateBossArena(info));
                break;
            case LevelGenInfo.LevelType.ASTEROIRD:
                break;
            default:
                break;
        }
    }



    IEnumerator CreateBoxLevel(LevelGenInfo info)
    {
        //LoadBossArena(info);

        LevelFinishedLoadingEventArgs args = new LevelFinishedLoadingEventArgs();
        args.startPos = Tile.MapToWorldPosition(startPoint);
        args.endPos = Tile.MapToWorldPosition(endPoint);
        //createBorder(info);
        GameEventSystem.PublishEvent(typeof(LevelLoadedSubscriber), args);

        yield return new WaitForSeconds(0);
    }

    IEnumerator CreateBossArena(LevelGenInfo info) {

        MapData mapBlock = Tile.LoadMapBlock(bossArena.bytes);
        Tile.SetMapBlock(new Int2(info.startX, 0), mapBlock);
        startPoint = new Int2(mapBlock.mapSize.x / 2 + info.startX,  info.startY + 5);
        //endPoint= new Int2(info.levelWidth + info.startX -5 , info.levelHeight  + info.startY -5);
        SpawnBoss(info ,mapBlock);


        LevelFinishedLoadingEventArgs args = new LevelFinishedLoadingEventArgs();
        args.startPos = Tile.MapToWorldPosition(startPoint);
        //args.endPos = Tile.MapToWorldPosition(endPoint);
        //createBorder(info);
        GameEventSystem.PublishEvent(typeof(LevelLoadedSubscriber), args);

        yield return new WaitForSeconds(0);
    }
     
   void SpawnBoss(LevelGenInfo info, MapData mapBlock)
    {
        Int2 bossLocation = new Int2(info.startX + mapBlock.mapSize.x /2, info.startY + mapBlock.mapSize.y/2);
        Vector3 spawnLoc = Tile.MapToWorldPosition(bossLocation);
        GameObject go = Instantiate(Boss, spawnLoc, Quaternion.identity) as GameObject;
        go.transform.SetParent(levelTransform);
    }

}
