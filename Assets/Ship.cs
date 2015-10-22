using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {
    
 
    [SerializeField]
    WeaponSlot[] weaponSlots;

    [SerializeField]
    ItemScript[] capacitors;

    [SerializeField]
    ItemScript[] engines;

    [SerializeField]
    ItemScript[] thrusters;

    [SerializeField]
    ItemScript[] armors;


    public float currentHealth;
    public float maxHealth;

    public float baseEnergy;
    public float baseEnergyRegen;
    public float maxEnergy;
    public float currentEnergy;

    public float baseForce;
    public float baseMaxSpeed;

    public float baseArmor;



    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // tries to fire all the weapons in the group
    public void TryToFire(int weaponGroup)
    {
        foreach (WeaponSlot weaponSlot in weaponSlots)
        {
            if (weaponSlot.weapon != null) {
                Weapon weapon =  weaponSlot.weapon.GetComponent<Weapon>();                
                weapon.TryToFire(currentEnergy, out currentEnergy);
               

            }
            
        }

    }

    public void DamageAdvanced(float value, int weaponType)
    {

    }
    public void Damage(float value)
    {
        currentHealth -= value;
        if(currentHealth <= 0)
        {
            Destroy();
        }
    }

    public float GetThrust()
    {
        float thrust = 0;
       
        return thrust;
    }


    void Destroy()
    {
        Debug.Log("Player Should be Destroyed");
    }
}
