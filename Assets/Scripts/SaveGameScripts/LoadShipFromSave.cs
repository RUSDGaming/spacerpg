using UnityEngine;
using System.Collections;

public class LoadShipFromSave : MonoBehaviour {

    [SerializeField]
    ControlSwitcher switcher;
    public string shipSaveName = "ship";

	// Use this for initialization
	void Start () {
        Load();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F6))
        {
            Debug.Log("saving Ship");
            Save();
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            Debug.Log("LoadingShip");
            Load();
        }

    }


    public void Save()
    {

        GameObject go = transform.GetChild(0).gameObject;

        Ship ship = go.GetComponent<Ship>();
        SaveGameShip ssg = new SaveGameShip();
        ssg.shipId = ship.id;

        SaveGameSystem.SaveGame(ssg,LoadPannel.current + shipSaveName);

    }


    public void Load()
    {
        SaveGameShip ssg = SaveGameSystem.LoadGame(LoadPannel.current + shipSaveName) as SaveGameShip;

        Ship ship = ShipManager.GetNewShip(ssg.shipId);

        ship.transform.SetParent(transform);

        switcher.SetShip(ship);

    }
}
