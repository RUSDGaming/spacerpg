using UnityEngine;
using System.Collections;

using Game.Interfaces;
using System;

using System.Linq;

using Game.Events;

public class GameLogic : MonoBehaviour ,  PortalSubscriber, LevelLoadedSubscriber{

    public Transform playerRespawn;
    public LevelGenScript levelGen;

    public GameObject portal;

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

            PlayerController[] players = FindObjectsOfType<PlayerController>();

            PortalEventArgs argz = (PortalEventArgs) args;

            if(argz.portalId == -1)
            {
                foreach (PlayerController player in players)
                {
                    player.gameObject.transform.position = playerRespawn.position;
                }

                levelLoaded = false;
                Debug.Log("Destroy everything previously loadded in that other scene");
            }
            else
            {


            StartCoroutine(levelGen.AddRandomLevelToCurrent(argz.info));
            levelLoaded = true;
            }
        }

        if(args.GetType() == typeof(LevelFinishedLoadingEventArgs))
        {
            LevelFinishedLoadingEventArgs argz = (LevelFinishedLoadingEventArgs)args;

            PlayerController[] players = FindObjectsOfType<PlayerController>();

            GameObject portalInstance = (GameObject) Instantiate(portal,argz.endPos,Quaternion.identity);

            foreach (PlayerController player in players)
            {
                player.gameObject.transform.position = argz.startPos;
            }

        }
    }
}
