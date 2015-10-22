using UnityEngine;
using System.Collections;
using SpriteTile;

public class LevelGenScript : MonoBehaviour
{



    public int width = 100;
    public int height = 100;
    // how long many times can placing a room down fail
    public int numFails = 100;
    public int maxRooms = 50;
    public int padding = 1;

    public int minRoomHeight = 5;
    public int maxRoomHeight = 20;
    public int minRoomWidth = 5;
    public int maxRoomWidth = 20;
    public Material mat;
    int wallTile = 1;
    int floorTile = 2;


    public TextAsset myLevel;
    // Use this for initialization
    void Start()
    {
        Tile.SetCamera();
        Tile.NewLevel(new Int2(width, height), 0, 4, 0, LayerLock.None);
       // Tile.SetTileMaterial(mat);
        // Tile.LoadLevel(myLevel);
        //  createRoom(5, 5);

        //createBorder();

        StartCoroutine(GenerateRandomLevel());
        
    }

    IEnumerator GenerateRandomLevel()
    {



        int rooms = 0;
        int fails = 0;
        while(fails < numFails && rooms < maxRooms)
        {

            float failPercent =  1 - ((float) fails / (float) numFails );
           // Debug.Log(failPercent);
            float maxW = minRoomWidth + maxRoomWidth * failPercent;
            float maxH = minRoomHeight+ maxRoomHeight * failPercent;

            int maxWidthInt = Mathf.CeilToInt(maxW);
            int maxHeightInt = Mathf.CeilToInt(maxH);
            //if(maxWidthInt < minRoomWidth)
            //{
            //    maxWidthInt = minRoomWidth;
            //}
            //if(maxHeightInt < minRoomHeight)
            //{
            //    maxHeightInt = minRoomHeight;
            //}

            

            if (CreateRoom(Random.Range(minRoomWidth, maxWidthInt), Random.Range(minRoomHeight, maxHeightInt)))
            {
                rooms++;
                
                yield return new WaitForSeconds(.01f);
            }
            else
            {
                fails++;
            }
        }
        Debug.Log("rooms Created" + rooms);
        Debug.Log("times Failed" + fails);

        yield return new WaitForSeconds(.01f);
        createBorder();
        yield return new WaitForSeconds(.01f);
        fillEmpty();
    }


    bool CreateRoom(int w, int h)
    {
        int startX = Random.Range(0, width  - w);
        int startY = Random.Range(0, height - h);
        return CreateBox(startX, startY, w, h);
    }


    bool CreateBox(int startX, int startY, int w, int h)
    {

        if (DoesCollide(startX, startY, w, h))
        {           
            return false;
        }
        
        for (int x = startX; x < startX + w; x++)
        {
            for (int y = startY; y < startY + h; y++)
            {

                if (x == startX || x == startX + w - 1 || y == startY || y == startY + h - 1)
                {
                    // Debug.Log("hat");
                    Tile.SetTile(new Int2(x, y), 0, wallTile, true);

                }
                else
                {
                    if (Tile.GetTile(new Int2(x, y)).tile != wallTile)
                    {
                        //Debug.Log("setting floor tile");
                        Tile.SetTile(new Int2(x, y), 0, floorTile, false);

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

            Tile.SetTile(new Int2(randX, randY), 0, floorTile, false);

            break;

        }


        return true;

    }

    bool DoesCollide(int startX, int startY, int w, int h)
    {
        if(startX <= 0 +padding || startY <= 0 +padding || startX + w + padding >=width || startY + h + padding >= height)
        {
            return true;

        }
        for (int x = startX - padding; x < startX + w + padding; x++)
        {
            for (int y = startY -padding; y < startY + h + padding; y++)
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

    void createBorder()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
              
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    Tile.SetTile(new Int2(x, y), 0, wallTile, true);
                }
            }

        }
    }

    void fillEmpty()
    {

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if(Tile.GetTile(new Int2(x, y)).tile == -1)
                Tile.SetTile(new Int2(x, y), 0, floorTile, false);
            }

        }

    }

}
