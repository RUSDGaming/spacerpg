using UnityEngine;
using System.Collections;
using Game.Interfaces;

public class ShipSelector : MonoBehaviour {

    [SerializeField]    PlayerShip[] ships;
    [SerializeField]    RectTransform shipInfoPanel;
    [SerializeField]    GameObject[] shipInfos;
    [SerializeField] ControlSwitcher switcher;

	// Use this for initialization
	void Start () {
        LoadShips();
	}


    void LoadShips()
    {

        for(int i = 0; i < ships.Length; i++)
        {
            PlayerShip ship = ships[i];
            shipInfos[i].SetActive(true);
            UIShipInfo info = shipInfos[i].GetComponent<UIShipInfo>();
            info.ShipImage = ship.gameObject.GetComponent<SpriteRenderer>().sprite;
            info.HealthText = "Health: "+ ship.baseHealth;
            info.ArmorText = "Armor: " + ship.baseArmor;
            info.ShieldText = "Sheild: " + ship.baseMaxShield;
            info.EnergyText = "Energy: " + ship.baseMaxEnergy;
            info.EnergyRegenText = "Energy Regen: " + ship.baseEnergyRegen;
            info.ThrustText = "Thrust :" + ship.baseMoveForce;
            info.TurnRateText = "Turn Rate: " + ship.baseTurnRate;
            info.SpeedText = "Max Speed : " + ship.baseMaxSpeed;
            info.MassText = "Mass: " + ship.gameObject.GetComponent<Rigidbody2D>().mass;
            info.setUpButton(ship.gameObject, switcher);
        }
        shipInfoPanel.sizeDelta = new Vector2(480, 200 * ships.Length);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
