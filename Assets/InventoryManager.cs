using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {

    public Ship ship;

   

    [SerializeField]    Inventory stationaryInventory;
    [SerializeField]    Inventory scrapInventory;




    [SerializeField]    Transform uiShipInventory;
    [SerializeField]    Transform[] uiWeaponIneventories;
    [SerializeField]    Transform uiStationaryIneventory;
    [SerializeField]    Transform uiScrapInventory;

    

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            stationaryInventory.LoadItemsIntoUI(uiStationaryIneventory);
            scrapInventory.LoadItemsIntoUI(uiScrapInventory);
            if (!ship)
            {
                Debug.Log("ship was not set!!");
                return;
            }

            Inventory shipIneventory = ship.GetComponent<Inventory>();
            shipIneventory.LoadItemsIntoUI(uiShipInventory);


            for (int i = 0; i < ship.weaponSlots.Length; i++)
            {
                uiWeaponIneventories[i].transform.localPosition = (ship.transform.InverseTransformPoint(ship.weaponSlots[i].transform.position)) * 35;
                ship.weaponSlots[i].LoadItemsIntoUI(uiWeaponIneventories[i]);
            }


        }
	}
}
