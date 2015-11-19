using UnityEngine;
using System.Collections;

using Game.Interfaces;
using System;

using System.Linq;
using SpriteTile;
using Game.Events;

public class GameLogic : MonoBehaviour ,  PortalSubscriber, LevelLoadedSubscriber, HomeSubscriber{

    public Transform playerRespawn;
    //[SerializeField]  Transform generatedLevel;
    public LevelGenScript levelGen;

   // public GameObject portal;

    //LevelGenInfo levelInfo;

    bool levelLoaded = false;



    [SerializeField]    LevelGeneratorScript region0;
    [SerializeField]    LevelGeneratorScript region1;
    LevelGeneratorScript currentlyLoadedRegion;

	void Start () {
        GameEventSystem.RegisterSubScriber(this);
        region0.Init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void HandleEvent(GameEventArgs args)
    {
        if(args.GetType() == typeof( PortalEventArgs))
        {
            HandlePortalEvent(args);
           
        }

        if(args.GetType() == typeof(LevelFinishedLoadingEventArgs))
        {
            HandleLevelLoadedEvent(args);

        }
        if(args.GetType() == typeof(PlayerHomeEventArgs))
        {
            HandleHomeEvent(args);
        }
        
    }

    void HandleLevelLoadedEvent(GameEventArgs args) {
        LevelFinishedLoadingEventArgs argz = (LevelFinishedLoadingEventArgs)args;

        // TODO load players into a global array so this is faster... 
        PlayerController[] players = FindObjectsOfType<PlayerController>();

       // GameObject portalInstance = (GameObject)Instantiate(portal, argz.endPos, Quaternion.identity);

        foreach (PlayerController player in players)
        {
            player.transform.position = argz.startPos;
        }
        levelLoaded = true;
    }

    void HandlePortalEvent(GameEventArgs args)
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();

        PortalEventArgs argz = (PortalEventArgs)args;

        if (argz.region)
        {
            argz.region.Init();
            currentlyLoadedRegion = argz.region;
        }        
    }

    void HandleHomeEvent(GameEventArgs args)
    {
        if (!levelLoaded)
        {
            return;
        }
        PlayerController[] players = FindObjectsOfType<PlayerController>();
        foreach (PlayerController player in players)
        {
            player.gameObject.transform.position = region0.playerSpawnPosition;
        }

        levelLoaded = false;
        Debug.Log("Destroy everything previously loadded in that other scene");
        currentlyLoadedRegion.CleanRegion();
    }


    //private void DestroyLevel()
    //{
    //    PortalScript[] portals = FindObjectsOfType<PortalScript>();
    //    var query = from portal in portals where portal.id == -1 select portal;
        
    //    foreach(PortalScript p in query)
    //    {
    //        GameObject.Destroy(p.gameObject);
    //    }


        
        
    //    Int2 corner1 = new Int2(levelInfo.startX, levelInfo.startY);
    //    Int2 corner2 = new Int2(levelInfo.startX + levelInfo.levelWidth, levelInfo.startY + levelInfo.levelHeight);
    //    Tile.DeleteTileBlock(corner1, corner2);
        
    //     foreach(Transform child in generatedLevel)
    //    {
    //        GameObject.Destroy(child.gameObject);
    //    }
    //}
}
