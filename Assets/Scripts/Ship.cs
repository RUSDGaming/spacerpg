﻿using UnityEngine;
using System.Collections;
using Game.Interfaces;
using System;
using Game.Events;


[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(PlayerController))]
public class Ship : MonoBehaviour ,iShip{


    

    #region equipment
    public WeaponInventory[] weaponSlots;
    [SerializeField]    ItemScript[] capacitors;
    [SerializeField]    ItemScript[] engines;
    [SerializeField]    ItemScript[] thrusters;
    [SerializeField]    ItemScript[] armors;
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
    public float maxHealth;
    public float currentHealth;
    public float armor;
    public float maxSheild;
    public float currentSheild;
    public float maxEnergy;
    public float currentEnergy;
    public float energyRegen;
    public float moveForce;
    public float maxSpeed;
    public float turnRate;
    #endregion

    public PlayerStats playerStats;

    ShowThrust thrust;


    // Use this for initialization
    void Start () {
        //currentEnergy = maxEnergy;
        //currentHealth = maxHealth;
        body = GetComponent<Rigidbody2D>();
        switcher = GetComponentInParent<ControlSwitcher>();
        forceFeild = GetComponentInChildren<ShowForceFeild>();
        lastWeaponSoundPlayed = -weaponSoundRate;
        thrust = GetComponentInChildren<ShowThrust>();
    }

	public void SetActualStats(PlayerStats stats, bool heal)
    {
        playerStats = stats;
        stats.SetActualStat(PlayerStats.STATS.HEALTH, baseHealth,ref maxHealth);        
        stats.SetActualStat(PlayerStats.STATS.ARMOR, baseArmor, ref armor);
        stats.SetActualStat(PlayerStats.STATS.SHEILD, baseMaxSheild, ref maxSheild);        
        stats.SetActualStat(PlayerStats.STATS.ENERGY_REGENERATION, baseEnergyRegen, ref energyRegen);        
        stats.SetActualStat(PlayerStats.STATS.ENERGY_CAPACITY, baseMaxEnergy, ref maxEnergy);

        stats.SetActualStat(PlayerStats.STATS.MOVE_SPEED, baseMoveForce, ref moveForce);        
        stats.SetActualStat(PlayerStats.STATS.MOVE_SPEED, baseMaxSpeed, ref maxSpeed);
        stats.SetActualStat(PlayerStats.STATS.TURN_RATE, baseTurnRate, ref turnRate);
        if (heal)
        {
        currentHealth = maxHealth;        
        currentSheild = maxSheild;        
        currentEnergy = maxEnergy;

        }


    }
	
    void FixedUpdate()
    {
        regenEnergy();
        RegenSheild();
    }
    float lastWeaponSoundPlayed ;
    float weaponSoundRate = .1f;

    // tries to fire all the weapons in the group
    public bool TryToFire(int weaponGroup, bool isPlayer)
    {
        foreach (WeaponInventory weaponSlot in weaponSlots)
        {
            if (weaponSlot.items[0] != null) {
                Weapon weapon =  weaponSlot.items[0].GetComponent<Weapon>();                
              if(  weapon.TryToFire(ref currentEnergy, true, playerStats))
                {
                    if(Time.time - lastWeaponSoundPlayed > weaponSoundRate)
                    {
                        lastWeaponSoundPlayed = Time.time;
                        weapon.PlaySound();

                    }
                }

            }
            
        }
        return false;

    }

    public void DamageAdvanced(float value, int weaponType)
    {

    }
    public void Damage(float damage, int damagerId)
    {
        float damageToShip = 0;
        damageToShip = DamageSheild(damage);
        damageToShip = DamageArmor(damageToShip);
        currentHealth -= damageToShip;
        if(currentHealth <= 0)
        {
            Destroy();
        }

        PlayerHitEventArgs args = new PlayerHitEventArgs { playerId = 1, damageDelt = damageToShip };
        GameEventSystem.PublishEvent(typeof(PlayerDamagedSubscriber), args);
        if(damageToShip > 0)
        InfoBlurbManager.CreateInfoBlurb(this.transform.position, "" + damageToShip, Color.red);
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

    

    public void MoveUnit(Vector2 moveDir,bool relativeInput)
    {
        if (relativeInput)
        {
            body.AddRelativeForce(moveDir * Time.fixedDeltaTime * moveForce);
        }
        else
        {
        body.AddForce(moveDir * Time.fixedDeltaTime * moveForce);
        }

        if (body.velocity.magnitude > maxSpeed)
        {
            body.velocity = body.velocity.normalized * maxSpeed;
        }

        if (thrust)
            thrust.thrust(moveDir.x,moveDir.y);
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
