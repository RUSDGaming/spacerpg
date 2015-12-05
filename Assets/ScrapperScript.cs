﻿using UnityEngine;
using System.Collections;

public class ScrapperScript : MonoBehaviour {

    [SerializeField]    UISlot2 scrapSlot;

    [HideInInspector]    public SaveGameInfo saveGameInfo;

    ControlSwitcher switcher;
	// Use this for initialization
	void Start () {
        switcher = GetComponent<ControlSwitcher>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    
    public void ScrapItem()
    {
        Item i = scrapSlot.GetItem();
        if (i)
        {
            saveGameInfo.money += i.scrapValue;
            scrapSlot.SetItem(null);
            Destroy(i);
            switcher.reloadShipStats(false);
            // TODO reload Player Stats
        }
    }
}
