using UnityEngine;
using System.Collections;

using Game.Interfaces;
using System;

using System.Linq;

using Game.Events;

public class GameLogic : MonoBehaviour ,  PortalSubscriber, LevelLoadedSubscriber{

    public LevelGenScript levelGen;

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
            PortalEventArgs pArgs = (PortalEventArgs) args;
            StartCoroutine(levelGen.AddRandomLevelToCurrent(pArgs.info));
            levelLoaded = true;
        }

        if(args.GetType() == typeof(LevelFinishedLoadingEventArgs))
        {
            LevelFinishedLoadingEventArgs argz = (LevelFinishedLoadingEventArgs)args;

            PlayerController[] players = FindObjectsOfType<PlayerController>();

            foreach (PlayerController player in players)
            {
                player.gameObject.transform.position = argz.startPos;
            }

        }
    }
}
