using UnityEngine;
using System.Collections;
using Game.Interfaces;
using System;
using Game.Events;


[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent (typeof(BoxCollider2D))]
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

    public SaveGameInfo playerStats;
    public bool playerControlled = false;
    public bool playerAtSpaceStation = false;
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

	public void SetActualStats(SaveGameInfo stats, bool heal)
    {
       // Debug.Log("setting actual stats, player id is : " + stats.playerId );
        playerStats = stats;
        maxHealth =    PlayerStats.SetActualStat(PlayerStats.STATS.HEALTH, playerStats.HEALTH,baseHealth);        
        armor   =    PlayerStats.SetActualStat(PlayerStats.STATS.ARMOR, playerStats.ARMOR, baseArmor);
        maxSheild =     PlayerStats.SetActualStat(PlayerStats.STATS.SHEILD, playerStats.SHEILD, baseMaxSheild);        
        energyRegen =    PlayerStats.SetActualStat(PlayerStats.STATS.ENERGY_REGENERATION, playerStats.ENERGY_REGENERATION, baseEnergyRegen);        
        maxEnergy =  PlayerStats.SetActualStat(PlayerStats.STATS.ENERGY_CAPACITY, playerStats.ENERGY_CAPACITY, baseMaxEnergy);
        
        moveForce =     PlayerStats.SetActualStat(PlayerStats.STATS.MOVE_SPEED,playerStats.MOVE_SPEED, baseMoveForce);        
        maxSpeed =    PlayerStats.SetActualStat(PlayerStats.STATS.MOVE_SPEED,playerStats.MOVE_SPEED ,baseMaxSpeed);
        turnRate =     PlayerStats.SetActualStat(PlayerStats.STATS.TURN_RATE,playerStats.TURN_RATE ,baseTurnRate);
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
            KillShip();
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

        if(forceFeild)
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


    void KillShip()
    {
       // Debug.Log("Player Should be Destroyed");
       // Debug.Log("Playe Should not control Core");
        if(playerControlled)
        switcher.SwitchToShipCore();

        else
        {
            Destroy(gameObject);
        }
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
