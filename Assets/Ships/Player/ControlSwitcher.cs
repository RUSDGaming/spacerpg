using UnityEngine;
using System;
using System.Collections;
using Game.Interfaces;

public class ControlSwitcher : MonoBehaviour {

    //public    GameObject shipCore;
    public    GameObject mainShip;
   // [SerializeField]    FollowTransform cameraFollower;
    [SerializeField]    PlayerMenu menu;
    [SerializeField]    StatHolderScript statHolderScript;
    [SerializeField]    PlayerDetails playerDetails;
    [SerializeField]    GameObject gameOverHud;


    [SerializeField]    ProgressBar health;
    [SerializeField]    ProgressBar shield;
    [SerializeField]    ProgressBar energy;

    [SerializeField]    LoadShipFromSave loadShipFromSave;

    public SaveGameInfo saveGameInfo;
    
    PlayerShip ship;
    GameLogic logic;
    InventoryManager inventoryManager;

	// Use this for initialization
	void Start () {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<GameLogic>();

        inventoryManager = GetComponent<InventoryManager>();

        // TODO set the player id correctly
        LoadSavedShip();

        saveGameInfo.playerId = 1;
       

        statHolderScript.SaveGameInfo = saveGameInfo;
       
       
        inventoryManager.ship = ship;
        //if(scrapperScript.saveGameInfo == null)
        //{
        //    Debug.LogError("scrip wasnt set correctly");
        //}


        if (mainShip.activeInHierarchy)
        {
            SwitchToMainShip();
        }
        //else if (shipCore.activeInHierarchy)
        //{         
        //    SwitchToShipCore();           
        //}
	}
	// Update is called once per frame
	void Update () {

        if (!ship)
            return;

       health.setImage(ship.currentHealth, ship.maxHealth);
       energy.setImage(ship.currentEnergy, ship.maxEnergy);        
       shield.setImage(ship.currentShield, ship.maxShield);	
	}

    //public void SwitchToShipCore()
    //{
        
    //    shipCore.SetActive(true);
    //    shipCore.transform.position = mainShip.transform.position;
    //    mainShip.SetActive(false);
    //    //cameraFollower.followObject = shipCore.transform;
    //    menu.SetShip(shipCore);
    //    shipCore.GetComponent<PlayerController>().disableInput = false;
    //    menu.playerController = shipCore.GetComponent<PlayerController>();
    //    shipCore.GetComponent<ShipCore>().SetActualStats(saveGameInfo,true);
    //    inventoryManager.ship = ship;
    //}

    public void SwitchToMainShip()
    {
        mainShip.SetActive(true);
       // mainShip.transform.position = shipCore.transform.position;
       // shipCore.SetActive(false);
      //  cameraFollower.followObject = mainShip.transform;
        menu.playerController = mainShip.GetComponent<PlayerController>();
        menu.SetShip(mainShip);
        reloadShipStats(true);
        ship = mainShip.GetComponent<PlayerShip>();
        if(logic)
        logic.TrackTarget(mainShip.transform);
        if(inventoryManager)
        inventoryManager.ship = ship;
    }

    public void reloadShipStats(bool heal)
    {
        //Debug.Log("reloading shipSttast");
        //shipCore.GetComponent<ShipCore>().SetActualStats(saveGameInfo,heal);
        mainShip.GetComponent<PlayerShip>().SetActualStats(saveGameInfo,heal);
        playerDetails.ship = mainShip.GetComponent<PlayerShip>();
        playerDetails.ReadPlayerStats();
    }

    public void SetShip(PlayerShip newShip)
    {
        if (mainShip)
        {
            mainShip.transform.SetParent(null);
            Destroy(mainShip.gameObject);
        }
        mainShip = newShip.gameObject;
        SwitchToMainShip();
        
        

    }


    [Obsolete("Use SetShip instead")]
    public void SetMainShip(GameObject shipPrefab)
    {
        ///shipCore.transform.position = mainShip.transform.position;
        GameObject newShip = Instantiate(shipPrefab, mainShip.transform.position, Quaternion.identity) as GameObject;

        // set the parent to nulll so the menu doesnt think its an inventory it should read from
        mainShip.transform.SetParent(null);
        Destroy(mainShip.gameObject);
        mainShip = newShip;

        LoadShipFromSave lsfs = GetComponentInChildren<LoadShipFromSave>();

        newShip.transform.SetParent(lsfs.transform);
        newShip.GetComponent<PlayerController>().disableInput = true;
        SwitchToMainShip();
        // this reloads all the invertorys of the new ship...
        menu.OpenMenu();
        logic.TrackTarget(mainShip.transform);
        inventoryManager.ship = ship;
    }

    public void UpgradeShip()
    {
        NextUpgrade nextShip =  ship.GetComponent<NextUpgrade>();

        if(saveGameInfo.money < nextShip.upgradeCost)
        {
            return;
        }

        if (!nextShip.prefab)
        {
            return;
        }

        saveGameInfo.money -= nextShip.upgradeCost;


        
        SetMainShip(nextShip.prefab);
        
        
    }


    public void LoadSavedShip()
    {
        string fileName = PlayerPrefs.GetString(LoadPannel.current);
        saveGameInfo = SaveGameSystem.LoadGame(fileName) as SaveGameInfo;
        if(saveGameInfo == null)
        {
            Debug.Log("Could not load player stats...");
            saveGameInfo = new SaveGameInfo();
        }
        loadShipFromSave.Load();

    }

    public void PlayerDeath()
    {
       // Debug.Log("player dided you need to show game over screen");
        // show game over screen
        gameOverHud.SetActive(true);
        ship.gameObject.SetActive(false);
        
    }

    public void SavePlayerGame()
    {
        // save player stats
        // save player shipInfo
            // save ship inventory
            // save ship equiped weapons

        //save stationary inventory
        //save scrapper ineventory
    }

}
