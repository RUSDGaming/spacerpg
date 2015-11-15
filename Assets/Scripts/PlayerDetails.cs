using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDetails : MonoBehaviour {

    [SerializeField]    Text levelText;
    [SerializeField]    Text expText;
    [SerializeField]    Text pointText;
    [SerializeField]    Text healthText;
    [SerializeField]    Text armorText;
    [SerializeField]    Text shieldText;
    [SerializeField]    Text energyText;
    [SerializeField]    Text energyRegenText;
    [SerializeField]    Text speedText;
    [SerializeField]    Text thrustText;
    [SerializeField]    Text turnRateText;

    public Ship ship;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public void ReadPlayerStats() {
        levelText.text = "Level : " + "<color=black>" + ship.playerStats.level + "</color>";
        expText.text = "EXP : " + "<color=black>" +   ship.playerStats.exp + "</color>";
        pointText.text = "Points : " + "<color=black>" + ship.playerStats.points + "</color>";

        healthText.text = "Health : " + "<color=black>" + ship.maxHealth + "</color>";
        shieldText.text = "Shield : " + "<color=black>" + ship.maxSheild + "</color>";
        armorText.text = "Armor : " + "<color=black>" + ship.armor + "</color>";
        energyText.text = "Energy : " + "<color=black>" + ship.maxEnergy + "</color>";
        energyRegenText.text = "Energy Regen :  " + "<color=black>" + ship.energyRegen + "</color>";
        thrustText.text = "Thrust : " + "<color=black>" + ship.moveForce + "</color>";
        turnRateText.text = "Turn Rate : " + "<color=black>" + ship.turnRate + "</color>";
    }
        
}
