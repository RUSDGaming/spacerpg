using UnityEngine;
using System.Collections;

using Game.Interfaces;
using System;

using System.Linq;
using SpriteTile;
using Game.Events;

public class GameLogic : MonoBehaviour, PortalSubscriber, LevelLoadedSubscriber, HomeSubscriber
{

    public Transform playerRespawn;
    //[SerializeField]  Transform generatedLevel;
    public LevelGenScript levelGen;

    // public GameObject portal;

    //LevelGenInfo levelInfo;

    bool levelLoaded = false;


    [SerializeField]
    MiniMapScript miniMapScript;
    [SerializeField]
    MiniMapCameraScript miniMapCameraScript;
    [SerializeField]
    MapEdgeManager mapEdgeManagerScript;

    [SerializeField]
    LevelGeneratorScript region0;
    [SerializeField]
    LevelGeneratorScript region1;


    LevelGeneratorScript currentlyLoadedRegion;

    void Start()
    {
        GameEventSystem.RegisterSubScriber(this);
        region0.Init();
    }

    public void OnDestroy()
    {
        //GameEventSystem.UnRegisterSubscriber(this);
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void HandleEvent(GameEventArgs args)
    {
        if (args.GetType() == typeof(PortalEventArgs))
        {
            HandlePortalEvent(args);

        }

        if (args.GetType() == typeof(LevelFinishedLoadingEventArgs))
        {
            HandleLevelLoadedEvent(args);

        }
        if (args.GetType() == typeof(PlayerHomeEventArgs))
        {
            HandleHomeEvent(args);
        }

    }

    void HandleLevelLoadedEvent(GameEventArgs args)
    {
        LevelFinishedLoadingEventArgs argz = (LevelFinishedLoadingEventArgs)args;

        // TODO load players into a global array so this is faster... 
        PlayerController[] players = FindObjectsOfType<PlayerController>();

        //MiniMapCameraScript mmcs = miniMapScript.gameObject.GetComponentInChildren<MiniMapCameraScript>();

        if (miniMapCameraScript)
        {
            miniMapCameraScript.LevelOffset = argz.region.transform.position - miniMapScript.transform.position;
        }
        else
        {
            Debug.Log("wtf the camera script wasnt set yet or something....");
        }

        if (miniMapScript)
        {
            miniMapScript.InitMap(argz.region.width, argz.region.height, argz.region.tiles);
        }
        else
        {
            Debug.Log("Wtf the minimapScript wasnt set either.... something is wrong with te level");
        }

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


        MiniMapCameraScript mmcs = miniMapScript.gameObject.GetComponentInChildren<MiniMapCameraScript>();

        mmcs.LevelOffset = region0.transform.position - miniMapScript.transform.position;
        miniMapScript.InitMap(region0.width, region0.height, region0.tiles);


        region0.SoftInit();

        PlayerController[] players = FindObjectsOfType<PlayerController>();
        foreach (PlayerController player in players)
        {
            player.gameObject.transform.position = region0.playerSpawnPosition;
        }

        levelLoaded = false;
        Debug.Log("Destroy everything previously loadded in that other scene");
        if (currentlyLoadedRegion)
            currentlyLoadedRegion.CleanRegion();




    }


    public void TrackTarget(Transform transform)
    {
        miniMapCameraScript.tracking = transform;
        mapEdgeManagerScript.trackingTarget = transform;
        //Debug.Log("Tracking target");

    }
}
