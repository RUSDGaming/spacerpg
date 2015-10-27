using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Game.Interfaces;
using System;

public class PlayerMenu : MonoBehaviour
{

    public GameObject ShipLayout;
    public GameObject Stats;
    public GameObject Inventory_UI;
    public PlayerController playerController;

    public GameObject slot;
    public GameObject shipSlot;

    public bool shipNearChest = false;

    [SerializeField]
    GameObject slotHolder;

    bool menuOpen = false;

    void Start()
    {
        ShipLayout.SetActive(false);
        Stats.SetActive(false);
        Inventory_UI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOpen)
            {
                CloseMenu();
                playerController.disableInput = false;
                ShipLayout.SetActive(false);
                Inventory_UI.SetActive(false);
                menuOpen = !menuOpen;

            }
            else
            {
                OpenMenu();
                playerController.disableInput = true;
                ShipLayout.SetActive(true);
                Inventory_UI.SetActive(true);
                menuOpen = !menuOpen;
            }
        }
    }

    void OpenMenu()
    {
        Inventory[] inventories = gameObject.transform.parent.GetComponentsInChildren<Inventory>(true);
        //Debug.Log(inventories.Length);
        DeactivateInventorySlots();
        int count = 0;
        foreach(Inventory inventory in inventories)
        {
            if(inventory.iventoryType == Inventory.InventoryType.STATIONARY)
            {
                Debug.Log("player stationary Inventory not yet implemented");
                continue;
            }

            Debug.Log("inv length"+ inventory.items.Length);
            for(int i = 0; i < inventory.items.Length; i++)
            {

                Slot slot = slotHolder.transform.GetChild(i + count).gameObject.GetComponent<Slot>();
                slot.SetColor(inventory.iventoryType);
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
                    slotItem.GetComponent<Image>().sprite = null;
                }

                
            }
            count += inventory.items.Length; 
        }

    }

    void CloseMenu()
    {

        Inventory[] inventories = gameObject.transform.parent.GetComponentsInChildren<Inventory>(true);
        //Debug.Log(inventories.Length);
        DeactivateInventorySlots();
        int count = 0;
        foreach (Inventory inventory in inventories)
        {
            if (inventory.iventoryType == Inventory.InventoryType.STATIONARY)
            {
                Debug.Log("player stationary Inventory not yet implemented");
                continue;
            }

            Debug.Log("inv length" + inventory.items.Length);
            for (int i = 0; i < inventory.items.Length; i++)
            {

                Slot slot = slotHolder.transform.GetChild(i + count).gameObject.GetComponent<Slot>();                
                Transform slotItem = slot.transform.GetChild(0);
                ItemDragScript ids = slotItem.GetComponent<ItemDragScript>();

                if(ids.realGamePrefab != null)
                {
                inventory.items[i] = ids.realGamePrefab.GetComponent<Item>();
                }
                else
                {
                    inventory.items[i] = null;
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
    }

    public void SetShip(GameObject ship)
    {
        Ship shipScript = ship.GetComponent<Ship>();
        int currentSlots = slotHolder.transform.childCount;
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
        ShipLayout.transform.GetChild(0).GetComponent<Image>().sprite = shipSprite;
        if (shipScript != null)
        {
            foreach (Transform shipSlot in ShipLayout.transform.GetChild(0).transform)
            {
                GameObject.Destroy(shipSlot.gameObject);
            }
            for (int i = 0; i < shipScript.weaponSlots.Length; i++)
            {
                GameObject shipSlotInstance = GameObject.Instantiate(shipSlot);
                
                shipSlotInstance.transform.SetParent( ShipLayout.transform.GetChild(0).transform,false);
                //shipSlotInstance.transform.position = shipScript.weaponSlots[i].transform.position;
                shipSlotInstance.GetComponent<RectTransform>().localPosition = shipScript.weaponSlots[i].transform.position * 32;
                
                shipSlotInstance.GetComponent<ShipSlot>().weaponSlot = shipScript.weaponSlots[i];
                GameObject weapon_Inv = GameObject.Instantiate(shipScript.weaponSlots[i].GetComponent<WeaponSlot>().weapon_Inv);
                weapon_Inv.transform.SetParent(  shipSlotInstance.transform,false);
                weapon_Inv.transform.localPosition = Vector3.zero;
            }
        }
        //}
        //catch(Exception e)
        //{
        //    Debug.LogError(e.ToString());
        //}
    }
    void ChangeSlots(int currentSlots, int shipSlots)
    {
        if (shipSlots > currentSlots)
        {
            for (int i = currentSlots; i < shipSlots; i++)
            {
                GameObject slotInstance = GameObject.Instantiate(slot);
                slotInstance.transform.parent = slotHolder.transform;
            }
        }
        else if (shipSlots < currentSlots)
        {
            for (int i = currentSlots; shipSlots < i; i--)
            {

                GameObject.Destroy(slotHolder.transform.GetChild(i - 1).gameObject);
            }
        }
    }




}
