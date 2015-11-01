﻿using UnityEngine;
using System.Collections;
using Game.Interfaces;

public class ControlSwitcher : MonoBehaviour {

    [SerializeField]
    GameObject shipCore;

    [SerializeField]
    GameObject mainShip;

    [SerializeField]
    FollowTransform cameraFollower;

    [SerializeField]
    PlayerMenu menu;

    [SerializeField]
    PlayerStats playerStats;

	// Use this for initialization
	void Start () {
        if (mainShip.activeInHierarchy)
        {
            SwitchToMainShip();
        } else if (shipCore.activeInHierarchy)
        {         
            SwitchToShipCore();
           
        }


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SwitchToShipCore()
    {
        shipCore.SetActive(true);
        shipCore.transform.position = mainShip.transform.position;
        mainShip.SetActive(false);
        cameraFollower.followObject = shipCore.transform;
        menu.SetShip(shipCore);
        menu.playerController = shipCore.GetComponent<PlayerController>();
        shipCore.GetComponent<ShipCore>().SetActualStats(playerStats);
    }

    public void SwitchToMainShip()
    {
        mainShip.SetActive(true);
        mainShip.transform.position = shipCore.transform.position;
        shipCore.SetActive(false);
        cameraFollower.followObject = mainShip.transform;
        menu.playerController = mainShip.GetComponent<PlayerController>();
        menu.SetShip(mainShip);
        mainShip.GetComponent<Ship>().SetActualStats(playerStats);
    }

    public void reloadShipStats()
    {
        Debug.Log("reloading shipSttast");
        shipCore.GetComponent<ShipCore>().SetActualStats(playerStats);
        mainShip.GetComponent<Ship>().SetActualStats(playerStats);
    }

}
