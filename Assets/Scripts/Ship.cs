using UnityEngine;
using System.Collections;
using Game.Interfaces;
using System;


[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(PlayerController))]
public class Ship : MonoBehaviour ,iShip{


    [SerializeField]
    float energyRegen = 2;

    
    public WeaponInventory[] weaponSlots;

    [SerializeField]
    ItemScript[] capacitors;

    [SerializeField]
    ItemScript[] engines;

    [SerializeField]
    ItemScript[] thrusters;

    [SerializeField]
    ItemScript[] armors;

    [SerializeField]
    float moveForce = 5000;
    [SerializeField]
    float maxSpeed = 10f;

    [SerializeField]
    ControlSwitcher switcher;
    [SerializeField]
    float maxTurnRate = 10f;

    Rigidbody2D body;


    public float currentHealth;
    public float maxHealth;

    public float baseEnergy;
    public float baseEnergyRegen;
    public float maxEnergy;
    public float currentEnergy;

    public float baseForce;
    public float baseMaxSpeed;

    public float baseArmor;

    //public int itemSlots;


    // Use this for initialization
    void Start () {
        currentEnergy = maxEnergy;
        currentHealth = maxHealth;
        body = GetComponent<Rigidbody2D>();
        switcher = GetComponentInParent<ControlSwitcher>();
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void FixedUpdate()
    {
        regenEnergy();
    }

    // tries to fire all the weapons in the group
    public bool TryToFire(int weaponGroup)
    {
        foreach (WeaponInventory weaponSlot in weaponSlots)
        {
            if (weaponSlot.items[0] != null) {
                Weapon weapon =  weaponSlot.items[0].GetComponent<Weapon>();                
                weapon.TryToFire(ref currentEnergy);
            }
            
        }
        return false;

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
        Debug.Log("Playe Should not control Core");
        switcher.SwitchToShipCore();        
    }

    

    public void MoveUnit(Vector2 force,bool relativeInput)
    {
        if (relativeInput)
        {
            body.AddRelativeForce(force * Time.fixedDeltaTime * moveForce);
        }
        else
        {
        body.AddForce(force * Time.fixedDeltaTime * moveForce);
        }

        if (body.velocity.magnitude > maxSpeed)
        {
            body.velocity = body.velocity.normalized * maxSpeed;
        }
    }

    public void RotateUnit(float deg)
    {

        if (Mathf.Abs(deg) > maxTurnRate * Time.fixedDeltaTime)
        {
            body.MoveRotation(body.rotation + (maxTurnRate * Mathf.Sign(deg)) * Time.fixedDeltaTime);
        }
        else
        {
            body.MoveRotation(body.rotation + deg);
        }
        
    }


    void regenEnergy()
    {
        currentEnergy += energyRegen * Time.fixedDeltaTime;
        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
    }

}
