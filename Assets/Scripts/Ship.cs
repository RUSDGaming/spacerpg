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
    ShowForceFeild forceFeild;

    #region Ship Base Stats
    [SerializeField]   public float baseHealth;
    [SerializeField]   public float baseArmor;
    [SerializeField]   public float baseMaxSheild;
    [SerializeField]   public float baseMaxEnergy;
    [SerializeField]   public float baseEnergyRegen;
    [SerializeField]   public float baseMoveForce;
    [SerializeField]   public float baseMaxSpeed;
    [SerializeField]   public float baseTurnRate;
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
        //currentEnergy = maxEnergy;
        //currentHealth = maxHealth;
        body = GetComponent<Rigidbody2D>();
        switcher = GetComponentInParent<ControlSwitcher>();
        forceFeild = GetComponentInChildren<ShowForceFeild>();       
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
        RegenSheild();
    }

    // tries to fire all the weapons in the group
    public bool TryToFire(int weaponGroup, bool isPlayer)
    {
        foreach (WeaponInventory weaponSlot in weaponSlots)
        {
            if (weaponSlot.items[0] != null) {
                Weapon weapon =  weaponSlot.items[0].GetComponent<Weapon>();                
                weapon.TryToFire(ref currentEnergy,true);
            }
            
        }
        return false;

    }

    public void DamageAdvanced(float value, int weaponType)
    {

    }
    public void Damage(float damage)
    {
        float damageToShip = 0;
        damageToShip = DamageSheild(damage);
        damageToShip = DamageArmor(damageToShip);
        currentHealth -= damageToShip;
        if(currentHealth <= 0)
        {
            Destroy();
        }
    }

    float DamageArmor(float damage)
    {
        float shipDamage =  damage - armor;

        if(shipDamage > 0)
        {
            return shipDamage;
        }
        else
        {
            return 0;
        }
    }

    float DamageSheild(float damage)
    {
        currentSheild -= damage;
        forceFeild.ShowSheild(maxSheild, currentSheild);
        if(currentSheild >= 0)
        {
            return 0;
        }
        else
        {
            float damageRemaing = -currentSheild;
            currentSheild = 0;
            return damageRemaing;
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

    void RegenSheild()
    {

        if(currentSheild >= maxSheild)
        {
            currentSheild = maxSheild;
            return;
        }

        float sheildRegenAmount = maxSheild * .1f * Time.fixedDeltaTime;

        if (sheildRegenAmount * 10f > currentEnergy)
            return;

        currentEnergy -= sheildRegenAmount * 10f;
        currentSheild += sheildRegenAmount;
    }
}
