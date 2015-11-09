using UnityEngine;
using System.Collections;
using Game.Interfaces;

public class ControlSwitcher : MonoBehaviour {

    public    GameObject shipCore;
    public    GameObject mainShip;
    [SerializeField]    FollowTransform cameraFollower;
    [SerializeField]    PlayerMenu menu;
    [SerializeField]    PlayerStats playerStats;
    [SerializeField]    PlayerDetails playerDetails;

    [SerializeField]    ProgressBar health;
    [SerializeField]    ProgressBar shield;
    [SerializeField]    ProgressBar energy;

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
    Ship ship;
	// Update is called once per frame
	void Update () {

        health.setImage(ship.currentHealth, ship.maxHealth);
        energy.setImage(ship.currentEnergy, ship.maxEnergy);
        shield.setImage(ship.currentSheild, ship.maxSheild);
	
	}

    public void SwitchToShipCore()
    {
        
        shipCore.SetActive(true);
        shipCore.transform.position = mainShip.transform.position;
        mainShip.SetActive(false);
        cameraFollower.followObject = shipCore.transform;
        menu.SetShip(shipCore);
        shipCore.GetComponent<PlayerController>().disableInput = false;
        menu.playerController = shipCore.GetComponent<PlayerController>();
        shipCore.GetComponent<ShipCore>().SetActualStats(playerStats,true);
    }

    public void SwitchToMainShip()
    {
        mainShip.SetActive(true);
        mainShip.transform.position = shipCore.transform.position;
        shipCore.SetActive(false);
        cameraFollower.followObject = mainShip.transform;
        menu.playerController = mainShip.GetComponent<PlayerController>();
        menu.SetShip(mainShip);
        reloadShipStats(true);
        ship = mainShip.GetComponent<Ship>();
    }

    public void reloadShipStats(bool heal)
    {
        //Debug.Log("reloading shipSttast");
        shipCore.GetComponent<ShipCore>().SetActualStats(playerStats,heal);
        mainShip.GetComponent<Ship>().SetActualStats(playerStats,heal);
        playerDetails.ship = mainShip.GetComponent<Ship>();
        playerDetails.ReadPlayerStats();
    }

    public void SetMainShip(GameObject shipPrefab)
    {
        shipCore.transform.position = mainShip.transform.position;
        GameObject newShip =  Instantiate(shipPrefab, mainShip.transform.position, Quaternion.identity) as GameObject;
        
        // set the parent to nulll so the menu doesnt think its an inventory it should read from
        mainShip.transform.SetParent(null);
        Destroy(mainShip.gameObject);
        mainShip = newShip;

        newShip.transform.SetParent(this.transform);
        newShip.GetComponent<PlayerController>().disableInput = true;
        SwitchToMainShip();
        // this reloads all the invertorys of the new ship...
        menu.OpenMenu();
    }

}
