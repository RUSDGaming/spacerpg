using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Game.Interfaces;
using System;

public class PlayerMenu : MonoBehaviour
{

   // public GameObject ShipLayout;
   // public GameObject Stats;
    //public GameObject Inventory_UI;
    public PlayerController playerController;
    public GameObject mainMenu;
    public PlayerStats playerStats;

    //public GameObject slot;
   // public GameObject shipSlot;
    [SerializeField]
    Sprite defaultSprite;


    public GameObject ship;
    public bool shipNearChest = false;

    [SerializeField]
    GameObject slotHolder;

    [SerializeField]
    GameObject shipImage;

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
                playerController.disableInput = false;
                mainMenu.SetActive(false);
                menuOpen = false;

            }
            else
            {
                OpenMenu();
                playerController.disableInput = true;
                mainMenu.SetActive(true);
                menuOpen = true;
            }
        }
    }

    void OpenMenu()
    {
        DeactivateInventorySlots();
        OpenInventorySlots();
     //   OpenWeaponSlots();
    }

    void OpenWeaponSlots()
    {
        Ship shipScript = ship.GetComponent<Ship>();

        if (!shipScript)        
            return;
        

        for (int i = 0; i < shipScript.weaponSlots.Length; i++)
        {
            GameObject shipSlotInstance = shipImage.transform.GetChild(i).gameObject;
            shipSlotInstance.SetActive(true);

            shipSlotInstance.transform.localPosition = shipScript.weaponSlots[i].transform.localPosition * 32;
            ItemDragScript ids = shipSlotInstance.transform.GetChild(0).GetComponent<ItemDragScript>();

            //Debug.Log(shipSlotInstance.transform.GetChild(0));
            ids.realGamePrefab = shipScript.weaponSlots[i].items[0].gameObject;
            ids.GetComponent<Image>().sprite = shipScript.weaponSlots[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
            if(ids.realGamePrefab == null)
            {
                ids.GetComponent<Image>().sprite = defaultSprite;
            }
        }
    }

    void CloseMenu()
    {
        CloseInventorySlots();
    //    CloseWeaponSlots();
        playerStats.SavePlayerStats();
    }
    void CloseWeaponSlots()
    {
        Ship shipScript = ship.GetComponent<Ship>();

        if (!shipScript)   
            return;

        for (int i = 0; i < shipScript.weaponSlots.Length; i++)
        {
            GameObject shipSlotInstance = shipImage.transform.GetChild(i).gameObject;          
            ItemDragScript ids = shipSlotInstance.transform.GetChild(0).GetComponent<ItemDragScript>();
            shipScript.weaponSlots[i].StoreItem(ids.realGamePrefab.GetComponent<Item>(), 0);                     
            ids.GetComponent<Image>().sprite = defaultSprite;   
        }
    }

    void OpenInventorySlots()
    {
        Inventory[] inventories = gameObject.transform.parent.GetComponentsInChildren<Inventory>(true);
        //Debug.Log(inventories.Length);
        int count = 0;
        int weaponCount = 0;
        foreach (Inventory inventory in inventories)
        {
            if (inventory.inventoryType == Inventory.InventoryType.STATIONARY)
            {
                Debug.Log("player stationary Inventory not yet implemented");
                continue;
            }

            //Debug.Log("inv length" + inventory.items.Length);
            for (int i = 0; i < inventory.items.Length; i++)
            {
                UISlot slot;

                if(inventory.inventoryType == Inventory.InventoryType.WEAPON_SLOT)
                {
                    slot = shipImage.transform.GetChild(weaponCount).gameObject.GetComponent<ShipSlot>();
                    slot.transform.localPosition = inventory.transform.localPosition * 32;
                    weaponCount++;
                }
                else
                {
                    slot = slotHolder.transform.GetChild(i + count).gameObject.GetComponent<UISlot>();
                }
                slot.SetColor(inventory.inventoryType);
                slot.gameObject.SetActive(true);
                Transform slotItem = slot.transform.GetChild(0);
                ItemDragScript ids = slotItem.GetComponent<ItemDragScript>();


                if (inventory.items[i] != null)
                {
                    ids.realGamePrefab = inventory.items[i].gameObject;
                    ids.GetComponent<ItemDragScript>().inventoryIndex = i;                    
                    slotItem.GetComponent<Image>().sprite = inventory.items[i].gameObject.GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    // if there were any previous items clear them out
                    ids.realGamePrefab = null;
                    slotItem.GetComponent<Image>().sprite = defaultSprite;
                }
            }
            if(inventory.inventoryType !=Inventory.InventoryType.WEAPON_SLOT )
                count += inventory.items.Length;
        }
    }

    void CloseInventorySlots()
    {
        Inventory[] inventories = gameObject.transform.parent.GetComponentsInChildren<Inventory>(true);
        int count = 0;
        int weaponCount = 0;
        foreach (Inventory inventory in inventories)
        {
            if (inventory.inventoryType == Inventory.InventoryType.STATIONARY)
            {
                Debug.Log("player stationary Inventory not yet implemented");
                continue;
            }

            //Debug.Log("inv length" + inventory.items.Length);
            for (int i = 0; i < inventory.items.Length; i++)
            {
                UISlot slot;
                if(inventory.inventoryType == Inventory.InventoryType.WEAPON_SLOT)
                {                   
                    slot = shipImage.transform.GetChild(weaponCount).gameObject.GetComponent<ShipSlot>();                   
                    weaponCount++;
                }
                else
                {
                slot = slotHolder.transform.GetChild(i + count).gameObject.GetComponent<UISlot>();
                }

                Transform slotItem = slot.transform.GetChild(0);
                ItemDragScript ids = slotItem.GetComponent<ItemDragScript>();

                if (ids.realGamePrefab != null)
                {
                    inventory.StoreItem(ids.realGamePrefab.GetComponent<Item>(), i);
                    //inventory.items[i] = ids.realGamePrefab.GetComponent<Item>();
                }
                else
                {
                    inventory.StoreItem(null, i);                    
                }
            }
            count += inventory.items.Length;
        }
    }


    void DeactivateInventorySlots()
    {
        foreach(Transform go in slotHolder.transform)
        {
            go.gameObject.SetActive(false);
        }
        foreach(Transform slot in shipImage.transform)
        {
            slot.gameObject.SetActive(false);
        }
    }

    public void SetShip(GameObject shipObject)
    {
        this.ship = shipObject;
        Ship shipScript = ship.GetComponent<Ship>();
       // int currentSlots = slotHolder.transform.childCount;
        //if (shipScript == null)
        //{
        //    ChangeSlots(currentSlots, 1);
        //}
        //else
        //{
        //    ChangeSlots(currentSlots, shipScript.itemSlots);
        //}

        Sprite shipSprite = ship.GetComponent<SpriteRenderer>().sprite;

        //try
        //{
       shipImage.GetComponent<Image>().sprite = shipSprite;
        //if (shipScript != null)
        //{
            
        //    for (int i = 0; i < shipScript.weaponSlots.Length; i++)
        //    {
        //        GameObject shipSlotInstance = shipImage.transform.GetChild(i).gameObject;
        //        shipSlotInstance.SetActive(true);
                                
        //        shipSlotInstance.transform.localPosition = shipScript.weaponSlots[i].transform.position * 32;
        //        shipSlotInstance.transform.GetChild(0).GetComponent<ItemDragScript>().realGamePrefab = shipScript.weaponSlots[i].weapon;

        //    }
        //}
        //}
        //catch(Exception e)
        //{
        //    Debug.LogError(e.ToString());
        //}
    }
    //void ChangeSlots(int currentSlots, int shipSlots)
    //{
    //    if (shipSlots > currentSlots)
    //    {
    //        for (int i = currentSlots; i < shipSlots; i++)
    //        {
    //            GameObject slotInstance = GameObject.Instantiate(slot);
    //            slotInstance.transform.parent = slotHolder.transform;
    //        }
    //    }
    //    else if (shipSlots < currentSlots)
    //    {
    //        for (int i = currentSlots; shipSlots < i; i--)
    //        {

    //            GameObject.Destroy(slotHolder.transform.GetChild(i - 1).gameObject);
    //        }
    //    }
    //}




}
