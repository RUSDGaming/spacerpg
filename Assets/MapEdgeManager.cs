using UnityEngine;
using System.Collections;

public class MapEdgeManager : MonoBehaviour {


    public Transform trackingTarget;

    public Vector2 mapStart;

    public int mapWidth;
    public int mapHeight;
    public float orthographicCamSize;


    [SerializeField]    MapEdgeScript northCamera;
    [SerializeField]    MapEdgeScript southCamera;
    [SerializeField]    MapEdgeScript eastCamera;
    [SerializeField]    MapEdgeScript westCamera;


    [SerializeField]    MapEdgeScript northWestCam;
    [SerializeField]    MapEdgeScript northEastCam;
    [SerializeField]    MapEdgeScript southWestCam;
    [SerializeField]    MapEdgeScript southEastCam;


    [SerializeField]    WarperScript northBorder;
    [SerializeField]    WarperScript southBorder;
    [SerializeField]    WarperScript eastBorder;
    [SerializeField]    WarperScript westBorder;

    // Use this for initialization
    void Start () {
        //SetUpCameras();
        //SetUpBorders();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetUpBorders()
    {
        Vector2 maxPos = new Vector2(mapStart.x + mapWidth * orthographicCamSize*2, mapStart.y + mapHeight * orthographicCamSize*2);
        Vector2 minPos = new Vector2(mapStart.x , mapStart.y );

        northBorder.transform.position =  new Vector3( mapWidth * orthographicCamSize, mapHeight * orthographicCamSize * 2 + orthographicCamSize/2);
        northBorder.transform.localScale = new Vector3(orthographicCamSize * mapWidth *2, orthographicCamSize);
        northBorder.maxPos = maxPos;
        northBorder.minPos = minPos;

        southBorder.transform.position = new Vector3(mapWidth * orthographicCamSize, - orthographicCamSize /2);
        southBorder.transform.localScale = new Vector3(orthographicCamSize * mapWidth * 2, orthographicCamSize);
        southBorder.maxPos = maxPos;
        southBorder.minPos = minPos;

        eastBorder.transform.position = new Vector3(mapWidth * orthographicCamSize * 2 + orthographicCamSize /2 , mapHeight * orthographicCamSize );
        eastBorder.transform.localScale = new Vector3( orthographicCamSize, mapHeight * orthographicCamSize * 2);
        eastBorder.maxPos = maxPos;
        eastBorder.minPos = minPos;

        westBorder.transform.position = new Vector3(- orthographicCamSize / 2, mapHeight * orthographicCamSize);
        westBorder.transform.localScale = new Vector3(orthographicCamSize, mapHeight * orthographicCamSize * 2);
        westBorder.maxPos = maxPos;
        westBorder.minPos = minPos;

    }


    

    public void SetUpCameras()
    {
        // these are the max that  the plane that is showing what the camear is point at should be at...
        Vector2 maxPosPlane = new Vector2(mapStart.x + mapWidth * orthographicCamSize *2 + orthographicCamSize, mapStart.y + mapHeight * orthographicCamSize * 2 + orthographicCamSize);
        Vector2 minPosPlane = new Vector2(mapStart.x - orthographicCamSize, mapStart.y - orthographicCamSize);

        // max and min pos that the cam should be at
        Vector2 maxPosCam = new Vector2(mapStart.x + mapWidth * orthographicCamSize * 2 - orthographicCamSize, mapStart.y + mapHeight * orthographicCamSize * 2 - orthographicCamSize);
        Vector2 minPosCam = new Vector2(mapStart.x + orthographicCamSize, mapStart.y + orthographicCamSize);

        // the camp should be over the max tile
        // the tile should be out of bounds.. figure it out thrusday...

        Debug.Log(minPosCam);
        northCamera.init(trackingTarget, new Vector2(0, minPosCam.y), new Vector2(0, maxPosPlane.y), true,0,0);
        southCamera.init(trackingTarget, new Vector2(40, maxPosCam.y), new Vector2(0, minPosPlane.y), true,0,0);
        eastCamera.init(trackingTarget, new Vector2(minPosCam.x, 0), new Vector2(maxPosPlane.x, 0), false, 0, 0);
        westCamera.init(trackingTarget, new Vector2(maxPosCam.x, 40), new Vector2(minPosPlane.x,0), false, 0, 0);

        northWestCam.initCorner(new Vector3(maxPosCam.x, minPosCam.y,-1), new Vector3(minPosPlane.x, maxPosPlane.y,-5));
        northEastCam.initCorner(new Vector3(minPosCam.x, minPosCam.y,-1), new Vector3(maxPosPlane.x, maxPosPlane.y,-5));
        southWestCam.initCorner(new Vector3(maxPosCam.x, maxPosCam.y,-1), new Vector3(minPosPlane.x, minPosPlane.y,-5));
        southEastCam.initCorner(new Vector3(minPosCam.x, maxPosCam.y,-1), new Vector3(maxPosPlane.x, minPosPlane.y,-5));

    }
}

