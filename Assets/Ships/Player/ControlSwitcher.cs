using UnityEngine;
using System.Collections;
using Game.Interfaces;

public class ControlSwitcher : MonoBehaviour {

    public    GameObject shipCore;
    public    GameObject mainShip;
    [SerializeField]    FollowTransform cameraFollower;
    [SerializeField]    PlayerMenu menu;
    [SerializeField]    StatHolderScript statHolderScript;
    [SerializeField]    PlayerDetails playerDetails;

    [SerializeField]    PlayerEXPManager expManager;
    [SerializeField]    ProgressBar health;
    [SerializeField]    ProgressBar shield;
    [SerializeField]    ProgressBar energy;

    public SaveGameInfo playerStats;

    Ship ship;
    GameLogic logic;

	// Use this for initialization
	void Start () {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();

        expManager = GetComponent<PlayerEXPManager>();
        // TODO set the player id correctly
        string fileName = PlayerPrefs.GetString(LoadPannel.current);
        playerStats = SaveGameSystem.LoadGame(fileName) as SaveGameInfo;
        playerStats.playerId = 1;
        Debug.Log(playerStats.primaryType);
        statHolderScript.SaveGameInfo = playerStats;
        expManager.saveGameInfo = playerStats;


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

        if (!ship)
            return;

        health.setImage(ship.currentHealth, ship.maxHealth);
        energy.setImage(ship.currentEnergy, ship.maxEnergy);
        shield.setImage(ship.currentSheild, ship.maxSheild);	
	}

    public void SwitchToShipCore()
    {
        
        shipCore.SetActive(true);
        shipCore.transform.position = mainShip.transform.position;
        mainShip.SetActive(false);
        //cameraFollower.followObject = shipCore.transform;
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
      //  cameraFollower.followObject = mainShip.transform;
        menu.playerController = mainShip.GetComponent<PlayerController>();
        menu.SetShip(mainShip);
        reloadShipStats(true);
        ship = mainShip.GetComponent<Ship>();
        logic.TrackTarget(mainShip.transform);
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
        GameObject newShip = Instantiate(shipPrefab, mainShip.transform.position, Quaternion.identity) as GameObject;

        // set the parent to nulll so the menu doesnt think its an inventory it should read from
        mainShip.transform.SetParent(null);
        Destroy(mainShip.gameObject);
        mainShip = newShip;

        newShip.transform.SetParent(this.transform);
        newShip.GetComponent<PlayerController>().disableInput = true;
        SwitchToMainShip();
        // this reloads all the invertorys of the new ship...
        menu.OpenMenu();
        logic.TrackTarget(mainShip.transform);
    }

    public void UpgradeShip()
    {
        NextUpgrade nextShip =  ship.GetComponent<NextUpgrade>();
        if (!nextShip.prefab)
        {
            return;
        }
        //GameObject next =  Instantiate(nextShip.prefab);
        //if (!next)
        //{
        //    return;
        //}
        SetMainShip(nextShip.prefab);
        //next.transform.SetParent(ship.transform.parent);
        //next.transform.position = ship.transform.position;

        //Destroy(mainShip.gameObject);
        //mainShip = next;
        //reloadShipStats(true);

        //ship = next.GetComponent<Ship>();
        //SetFollowers(mainShip);

        //menu.OpenMenu();
        
    }

    void SetFollowers(GameObject shipGO)
    {
        cameraFollower.followObject = shipGO.transform;
        menu.playerController = shipGO.GetComponent<PlayerController>();
        menu.SetShip(shipGO);
    }

}
