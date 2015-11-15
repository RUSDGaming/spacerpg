using UnityEngine;
using System.Collections;
using Game.Events;
using System;

public class FireWorld : MonoBehaviour, PortalSubscriber {

    // Use this for initialization
    void Start () {
        GameEventSystem.RegisterSubScriber(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HandleEvent(GameEventArgs args)
    {
        if (args.GetType() == typeof(PortalEventArgs))
        {
            HandlePortalEvent(args);

        }
    }

    private void HandlePortalEvent(GameEventArgs args)
    {
        PortalEventArgs argz = (PortalEventArgs)args;

        

    }
}
