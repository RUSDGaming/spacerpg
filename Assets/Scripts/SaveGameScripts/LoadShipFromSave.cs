using UnityEngine;
using System.Collections;

public class LoadShipFromSave : MonoBehaviour {

    [SerializeField]
    ControlSwitcher switcher;
    public string shipSaveName = "ship";

	// Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F6))
        {
            Debug.Log("saving Ship");
            Save(null);
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            Debug.Log("LoadingShip");
            Load();
        }

    }


    public  void Save(Ship ship)
    {
        if(ship == null)
        {
            GameObject go = transform.GetChild(0).gameObject;
            ship = go.GetComponent<Ship>();
        }

        SaveGameShip ssg = new SaveGameShip();
        ssg.shipId = ship.id;
        ssg.SetItems(ship.GetComponent<Inventory>().items);

        // foreach weapon slot 
        ssg.WeaponSlots = new SaveGameItem[ship.weaponSlots.Length];
        for (int i = 0; i < ship.weaponSlots.Length; i++)
        {
            if(ship.weaponSlots[i].items[0] != null)
            {
                SaveGameItem weapon = new SaveGameItem();
                weapon.id = ship.weaponSlots[i].items[0].id;
                ssg.WeaponSlots[i]  = weapon;
            }
            else
            {
                ssg.WeaponSlots[i] = null;
            }

        }




        string fileName = PlayerPrefs.GetString(LoadPannel.current);
        SaveGameSystem.SaveGame(ssg,fileName + shipSaveName);

    }


    public void Load()
    {

        string fileName = PlayerPrefs.GetString(LoadPannel.current);
        SaveGameShip ssg = SaveGameSystem.LoadGame(fileName + shipSaveName) as SaveGameShip;

        Ship ship = ShipManager.GetNewShip(ssg.shipId);

        ship.transform.SetParent(transform);
       // Debug.Log(ship.GetComponent<Inventory>().items.Length);
        ssg.GetItems(ref ship.GetComponent<Inventory>().items);

        if(ssg.WeaponSlots != null)
        for(int i = 0; i < ship.weaponSlots.Length; i++)
        {
            if(i < ssg.WeaponSlots.Length)
            if(ssg.WeaponSlots[i] != null)
            {
                Item item = ItemManager.GetNewItem(ssg.WeaponSlots[i].id);
                ship.weaponSlots[i].SetItemWithIndex(item,0);
            }
                else
                {
                    Debug.Log("weapon slot from saved game was null");
                    ship.weaponSlots[i].SetItemWithIndex(null,0);
                }
            
        }
        if (switcher)
            switcher.SetShip(ship);
        else
            Debug.LogError("Switcher not set!");


    }
}
