using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using Game.Interfaces;
using System;

public class PlayerMenu : MonoBehaviour
{

    public GameObject ShipLayout;
    public GameObject Stats;
    public GameObject Inventory;
    public PlayerController playerController;

    public GameObject slot;
    public GameObject shipSlot;


    [SerializeField]
    GameObject slotHolder;

    bool menuOpen = false;

    void Start()
    {
        ShipLayout.SetActive(false);
        Stats.SetActive(false);
        Inventory.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOpen)
            {
                 playerController.disableInput = false;
                ShipLayout.SetActive(false);
                Inventory.SetActive(false);
                menuOpen = !menuOpen;
            }
            else
            {
                playerController.disableInput = true;
                ShipLayout.SetActive(true);
                Inventory.SetActive(true);
                menuOpen = !menuOpen;
            }
        }
    }


    public void SetShip(GameObject ship)
    {
        Ship shipScript = ship.GetComponent<Ship>();
        int currentSlots = slotHolder.transform.childCount;
        if (shipScript == null)
        {
            ChangeSlots(currentSlots, 1);
        }
        else
        {
            ChangeSlots(currentSlots, shipScript.itemSlots);
        }

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
                
                shipSlotInstance.transform.parent = ShipLayout.transform.GetChild(0).transform;
                //shipSlotInstance.transform.position = shipScript.weaponSlots[i].transform.position;
                shipSlotInstance.GetComponent<RectTransform>().localPosition = shipScript.weaponSlots[i].transform.position * 32;
                
                shipSlotInstance.GetComponent<ShipSlot>().weaponSlot = shipScript.weaponSlots[i];
                GameObject weapon_Inv = GameObject.Instantiate(shipScript.weaponSlots[i].GetComponent<WeaponSlot>().weapon_Inv);
                weapon_Inv.transform.parent = shipSlotInstance.transform;
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
