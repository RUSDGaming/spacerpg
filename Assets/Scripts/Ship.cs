using UnityEngine;
using System.Collections;
using Game.Interfaces;
using System;


[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(PlayerController))]
public class Ship : MonoBehaviour ,iShip{




    #region equipment
    public WeaponInventory[] weaponSlots;
    [SerializeField]
    ItemScript[] capacitors;
    [SerializeField]
    ItemScript[] engines;
    [SerializeField]
    ItemScript[] thrusters;
   [SerializeField]
    ItemScript[] armors;
    #endregion


    [SerializeField]
    ControlSwitcher switcher;


    Rigidbody2D body;

    #region Ship Base Stats
    [SerializeField]    float baseHealth;
    [SerializeField]    float baseArmor;
    [SerializeField]    float baseMaxSheild;
    [SerializeField]    float baseMaxEnergy;
    [SerializeField]    float baseEnergyRegen;
    [SerializeField]    float baseMoveForce;
    [SerializeField]    float baseMaxSpeed;
    [SerializeField]    float baseTurnRate;
    #endregion


    #region Actual Stats
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] float armor;
    [SerializeField] float maxSheild;
    [SerializeField] float currentSheild;
    [SerializeField] float maxEnergy;
    [SerializeField] float currentEnergy;
    [SerializeField] float energyRegen;
    [SerializeField] float moveForce;
    [SerializeField] float maxSpeed;
    [SerializeField] float turnRate;
    #endregion

    // Use this for initialization
    void Start () {
        currentEnergy = maxEnergy;
        currentHealth = maxHealth;
        body = GetComponent<Rigidbody2D>();
        switcher = GetComponentInParent<ControlSwitcher>();
       
    }
	public void SetActualStats(PlayerStats stats)
    {
        
        stats.SetActualStat(PlayerStats.STATS.HEALTH, baseHealth,ref maxHealth);        
        currentHealth = maxHealth;        
        stats.SetActualStat(PlayerStats.STATS.ARMOR, baseArmor, ref armor);
        stats.SetActualStat(PlayerStats.STATS.SHEILD, baseMaxSheild, ref maxSheild);        
        currentSheild = maxSheild;        
        stats.SetActualStat(PlayerStats.STATS.ENERGY_REGENERATION, baseEnergyRegen, ref energyRegen);        
        stats.SetActualStat(PlayerStats.STATS.ENERGY_CAPACITY, baseMaxEnergy, ref maxEnergy);
        currentEnergy = maxEnergy;

        stats.SetActualStat(PlayerStats.STATS.MOVE_SPEED, baseMoveForce, ref moveForce);        
        stats.SetActualStat(PlayerStats.STATS.MOVE_SPEED, baseMaxSpeed, ref maxSpeed);
        stats.SetActualStat(PlayerStats.STATS.TURN_RATE, baseTurnRate, ref turnRate);        
        


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

        if (Mathf.Abs(deg) > turnRate * Time.fixedDeltaTime)
        {
            body.MoveRotation(body.rotation + (turnRate * Mathf.Sign(deg)) * Time.fixedDeltaTime);
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
