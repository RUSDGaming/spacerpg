using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Game.Interfaces;
using System;

public class PlayerMenu : MonoBehaviour
{
    

    public PlayerController playerController;
    public GameObject mainMenu;
    //public PlayerStats playerStats;

        
    [SerializeField]    GameObject storage;
    [SerializeField]    GameObject upgradeButton;
    [SerializeField]    GameObject scrapper;
    [SerializeField]    GameObject upgradeMenu;
    [SerializeField]    GameObject shop;

    [SerializeField]    Sprite defaultSprite;
    [SerializeField]    ControlSwitcher switcher;



    public GameObject ship;
    PlayerShip shipScript;

    public bool shipNearChest = false;

    [SerializeField]    GameObject slotHolder;
    [SerializeField]    GameObject shipImage;
    [SerializeField]    GameObject LevelUpText;

    bool menuOpen = false;

    void Start()
    {
        mainMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    public void OpenMenu()
    {

        
        if (shipScript)
        {
            if (shipScript.playerAtSpaceStation)
            {
                storage.SetActive(true);
                upgradeButton.SetActive(true);
                scrapper.SetActive(true);
            }
            if (shipScript.playerAtCrusher)
            {
                scrapper.SetActive(true);
            }
            if (shipScript.playerAtMechanic)
            {
                upgradeMenu.SetActive(true);
            }
            if (shipScript.playerAtBank)
            {
                storage.SetActive(true);
            }
            if (shipScript.playerAtShop)
            {
                shop.SetActive(true);
            }

        }
        LevelUpText.SetActive(false);
        playerController.disableInput = true;
        mainMenu.SetActive(true);
        menuOpen = true;
        switcher.reloadShipStats(false);
       // DeactivateInventorySlots();
      //  OpenInventorySlots();
    }



    void CloseMenu()
    {

       // CloseInventorySlots();
       // playerStats.SavePlayerStats();
        switcher.reloadShipStats(false);
        playerController.disableInput = false;
        mainMenu.SetActive(false);
        storage.SetActive(false);
        upgradeButton.SetActive(false);
        scrapper.SetActive(false);
        upgradeMenu.SetActive(false);
        menuOpen = false;
        shop.SetActive(false);
    }



    void FillUI()
    {

    }





    //void OpenInventorySlots()
    //{
    //    Inventory[] inventories = gameObject.transform.parent.GetComponentsInChildren<Inventory>(true);
    //    //Debug.Log(inventories.Length);
    //    int count = 0;
    //    int weaponCount = 0;
    //    foreach (Inventory inventory in inventories)
    //    {
    //        if (inventory.inventoryType == Inventory.InventoryType.STATIONARY)
    //        {
    //            Debug.Log("player stationary Inventory not yet implemented");
    //            continue;
    //        }

    //        // Debug.Log(inventory.inventoryType.ToString());
    //        // Debug.Log(inventory.transform.ToString());

    //        for (int i = 0; i < inventory.items.Length; i++)
    //        {
    //            UISlot slot;
    //            if (inventory.inventoryType == Inventory.InventoryType.WEAPON_SLOT)
    //            {

    //                slot = shipImage.transform.GetChild(weaponCount).gameObject.GetComponent<ShipSlot>();
    //                slot.transform.localPosition = (ship.transform.InverseTransformPoint(inventory.transform.position)) * 32;


    //                weaponCount++;
    //            }
    //            else
    //            {
    //                slot = slotHolder.transform.GetChild(i + count).gameObject.GetComponent<UISlot>();
    //            }
    //            slot.SetColor(inventory.inventoryType);
    //            slot.gameObject.SetActive(true);
    //            Transform slotItem = slot.transform.GetChild(0);
    //            ItemDragScript ids = slotItem.GetComponent<ItemDragScript>();


    //            if (inventory.items[i] != null)
    //            {
    //                ids.realGamePrefab = inventory.items[i].gameObject;
    //                ids.GetComponent<ItemDragScript>().inventoryIndex = i;
    //                slotItem.GetComponent<Image>().sprite = inventory.items[i].gameObject.GetComponent<SpriteRenderer>().sprite;
    //            }
    //            else
    //            {
    //                // if there were any previous items clear them out
    //                ids.realGamePrefab = null;
    //                slotItem.GetComponent<Image>().sprite = defaultSprite;
    //            }
    //        }
    //        if (inventory.inventoryType != Inventory.InventoryType.WEAPON_SLOT)
    //            count += inventory.items.Length;
    //    }
    //}

    //void CloseInventorySlots()
    //{
    //    Inventory[] inventories = gameObject.transform.parent.GetComponentsInChildren<Inventory>(true);
    //    int count = 0;
    //    int weaponCount = 0;
    //    foreach (Inventory inventory in inventories)
    //    {
    //        if (inventory.inventoryType == Inventory.InventoryType.STATIONARY)
    //        {
    //            Debug.Log("player stationary Inventory not yet implemented");
    //            continue;
    //        }

    //        //Debug.Log("inv length" + inventory.items.Length);
    //        for (int i = 0; i < inventory.items.Length; i++)
    //        {
    //            UISlot slot;
    //            if (inventory.inventoryType == Inventory.InventoryType.WEAPON_SLOT)
    //            {
    //                slot = shipImage.transform.GetChild(weaponCount).gameObject.GetComponent<ShipSlot>();
    //                weaponCount++;
    //            }
    //            else
    //            {
    //                slot = slotHolder.transform.GetChild(i + count).gameObject.GetComponent<UISlot>();
    //            }

    //            Transform slotItem = slot.transform.GetChild(0);
    //            ItemDragScript ids = slotItem.GetComponent<ItemDragScript>();

    //            if (ids.realGamePrefab != null)
    //            {
    //                inventory.ItemSits(ids.realGamePrefab.GetComponent<Item>(), i);
    //                //inventory.items[i] = ids.realGamePrefab.GetComponent<Item>();
    //            }
    //            else
    //            {
    //                inventory.ItemSits(null, i);
    //            }
    //        }
    //        count += inventory.items.Length;
    //    }
    //}


    //void DeactivateInventorySlots()
    //{
    //    foreach (Transform go in slotHolder.transform)
    //    {
    //        go.gameObject.SetActive(false);
    //    }
    //    foreach (Transform slot in shipImage.transform)
    //    {
    //        slot.gameObject.SetActive(false);
    //    }
    //}

    public void SetShip(GameObject shipObject)
    {
        this.ship = shipObject;
        shipScript = ship.GetComponent<PlayerShip>();
        Sprite shipSprite = ship.GetComponent<SpriteRenderer>().sprite;

        shipImage.GetComponent<Image>().sprite = shipSprite;

    }




}
