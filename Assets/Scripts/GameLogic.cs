using UnityEngine;
using System.Collections;

using Game.Interfaces;
using System;

using System.Linq;
using SpriteTile;
using Game.Events;

public class GameLogic : MonoBehaviour ,  PortalSubscriber, LevelLoadedSubscriber{

    public Transform playerRespawn;
    public LevelGenScript levelGen;

    public GameObject portal;

    LevelGenInfo levelInfo;

    bool levelLoaded = false;
	// Use this for initialization
	void Start () {
        GameEventSystem.RegisterSubScriber(this);
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
    }

    void HandleLevelLoadedEvent(GameEventArgs args) {
        LevelFinishedLoadingEventArgs argz = (LevelFinishedLoadingEventArgs)args;

        PlayerController[] players = FindObjectsOfType<PlayerController>();

        GameObject portalInstance = (GameObject)Instantiate(portal, argz.endPos, Quaternion.identity);

        foreach (PlayerController player in players)
        {
            player.gameObject.transform.position = argz.startPos;
        }
    }

    void HandlePortalEvent(GameEventArgs args)
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();

        PortalEventArgs argz = (PortalEventArgs)args;

        if (argz.portalId == -1)
        {
            foreach (PlayerController player in players)
            {
                player.gameObject.transform.position = playerRespawn.position;
            }

            levelLoaded = false;
            Debug.Log("Destroy everything previously loadded in that other scene");
            DestroyLevel();
        }
        else
        {
            levelInfo = argz.info;
            StartCoroutine(levelGen.AddRandomLevelToCurrent(argz.info));
            levelLoaded = true;
        }
    }

    private void DestroyLevel()
    {
        PortalScript[] portals = FindObjectsOfType<PortalScript>();
        var query = from portal in portals where portal.id == -1 select portal;
        
        foreach(PortalScript p in query)
        {
            GameObject.Destroy(p.gameObject);
        }


        
        
        Int2 corner1 = new Int2(levelInfo.startX, levelInfo.startY);
        Int2 corner2 = new Int2(levelInfo.startX + levelInfo.levelWidth, levelInfo.startY + levelInfo.levelHeight);
        Tile.DeleteTileBlock(corner1, corner2);
        
         
    }
}
