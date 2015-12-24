using UnityEngine;
using System.Collections;
using Game.Interfaces;
using Game.Events;
using System;

public abstract class BaseShip : MonoBehaviour, iShip {


    [SerializeField]
    protected Sprite[] explosionSprites;

    protected Sprite baseSprite;
    public SaveGameInfo playerStats;
    protected ShowThrust thrust;
    protected SpriteRenderer sr;



    #region equipment
    public WeaponInventory[] weaponSlots;
    [SerializeField]   protected ItemScript[] capacitors;
    [SerializeField]   protected ItemScript[] engines;
    [SerializeField]   protected ItemScript[] thrusters;
    [SerializeField]   protected ItemScript[] armors;
    #endregion


    


    protected Rigidbody2D body;
    protected ShowForceFeild forceFeild;

    #region Ship Base Stats
    [SerializeField]    public float baseHealth;
    [SerializeField]    public float baseArmor;
    [SerializeField]    public float baseMaxShield;
    [SerializeField]    public float baseMaxEnergy;
    [SerializeField]    public float baseEnergyRegen;
    [SerializeField]    public float baseMoveForce;
    [SerializeField]    public float baseMaxSpeed;
    [SerializeField]    public float baseTurnRate;
    #endregion


    #region Actual Stats
    public float maxHealth;
    public float currentHealth;
    public float armor;
    public float maxShield;
    public float currentShield;
    public float maxEnergy;
    public float currentEnergy;
    public float energyRegen;
    public float moveForce;
    public float maxSpeed;
    public float turnRate;
    #endregion

    public int id = -1;
    public bool playerControlled = false;
    
    public bool alive = true;

    public float lastWeaponSoundPlayed;
    public float weaponSoundRate = .1f;


    protected virtual void Awake()
    {
        baseSprite = GetComponent<SpriteRenderer>().sprite;
        body = GetComponent<Rigidbody2D>();
        
        forceFeild = GetComponentInChildren<ShowForceFeild>();
        lastWeaponSoundPlayed = -weaponSoundRate;
        thrust = GetComponentInChildren<ShowThrust>();
        sr = GetComponent<SpriteRenderer>();
        // set up weapon s
        weaponSlots = GetComponentsInChildren<WeaponInventory>();
        

    }

    

    public virtual void SetActualStats(SaveGameInfo stats, bool heal)
    {

        if (stats == null)
        {
            Debug.LogError("Stats were null....");
            return;
        }
        // Debug.Log("setting actual stats, player id is : " + stats.playerId );
        playerStats = stats;
        maxHealth = PlayerStats.SetActualStat(PlayerStats.STATS.HEALTH, playerStats.HEALTH, baseHealth);
        armor = PlayerStats.SetActualStat(PlayerStats.STATS.ARMOR, playerStats.ARMOR, baseArmor);
        maxShield = PlayerStats.SetActualStat(PlayerStats.STATS.SHIELD, playerStats.SHIELD, baseMaxShield);
        energyRegen = PlayerStats.SetActualStat(PlayerStats.STATS.ENERGY_REGENERATION, playerStats.ENERGY_REGENERATION, baseEnergyRegen);
        maxEnergy = PlayerStats.SetActualStat(PlayerStats.STATS.ENERGY_CAPACITY, playerStats.ENERGY_CAPACITY, baseMaxEnergy);

        moveForce = PlayerStats.SetActualStat(PlayerStats.STATS.MOVE_SPEED, playerStats.MOVE_SPEED, baseMoveForce);
        maxSpeed = PlayerStats.SetActualStat(PlayerStats.STATS.MOVE_SPEED, playerStats.MOVE_SPEED, baseMaxSpeed);
        turnRate = PlayerStats.SetActualStat(PlayerStats.STATS.TURN_RATE, playerStats.TURN_RATE, baseTurnRate);
        if (heal)
        {
            currentHealth = maxHealth;
            currentShield = maxShield;
            currentEnergy = maxEnergy;
        }

       
    }

    void FixedUpdate()
    {
        regenEnergy();
        RegenShield();
    }
   

    // tries to fire all the weapons in the group
    public bool TryToFire(int weaponGroup, bool isPlayer)
    {
        foreach (WeaponInventory weaponSlot in weaponSlots)
        {
            if (weaponSlot.items[0] != null)
            {
                Weapon weapon = weaponSlot.items[0].GetComponent<Weapon>();
                if (weapon.TryToFire(ref currentEnergy))
                {
                    //if (Time.time - lastWeaponSoundPlayed > weaponSoundRate)
                    //{
                    //    lastWeaponSoundPlayed = Time.time;
                    //    weapon.PlaySound();
                    //}
                    body.AddForce(-weapon.transform.up * weapon.knockBackForce, ForceMode2D.Impulse);
                }
            }
        }
        return false;
    }

    public void MouseUp()
    {
        foreach (WeaponInventory weaponSlot in weaponSlots)
        {
            if (weaponSlot.items[0] != null)
            {
                Weapon weapon = weaponSlot.items[0].GetComponent<Weapon>();
                weapon.MouseUp();
            }
        }
    }

    public virtual void DamageAdvanced(float value, int weaponType)
    {

    }
    public virtual void Damage(float damage, int damagerId)
    {

        if (!alive)
        {
            return;
        }

        float damageToShip = 0;
        damageToShip = DamageShield(damage);
        damageToShip = DamageArmor(damageToShip);
        currentHealth -= damageToShip;
        if (currentHealth <= 0)
        {
            alive = false;
            KillShip(damagerId);
        }

        PlayerHitEventArgs args = new PlayerHitEventArgs { playerId = 1, damageDelt = damageToShip };
        GameEventSystem.PublishEvent(typeof(PlayerDamagedSubscriber), args);
        if (damageToShip > 0)
            InfoBlurbManager.CreateInfoBlurb(this.transform.position, "" + damageToShip.ToString("0.0"), Color.red);
    }

    float DamageArmor(float damage)
    {
        float shipDamage = damage - armor;

        if (shipDamage > 0)
        {
            return shipDamage;
        }
        else
        {
            return 0;
        }
    }

    float DamageShield(float damage)
    {
        currentShield -= damage;

        if (forceFeild)
            forceFeild.ShowShield(maxShield, currentShield);
        if (currentShield >= 0)
        {
            return 0;
        }
        else
        {
            float damageRemaing = -currentShield;
            currentShield = 0;
            return damageRemaing;
        }
    }


    public float GetThrust()
    {
        float thrust = 0;

        return thrust;
    }


    
    



    public void MoveUnit(Vector2 moveDir, bool relativeInput)
    {
        if (!body)
            return;

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
            thrust.thrust(moveDir.x, moveDir.y);
    }

    public void RotateUnit(float deg)
    {
        if (!body)
            return;
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

    void RegenShield()
    {

        if (currentShield >= maxShield)
        {
            currentShield = maxShield;
            return;
        }

        float sheildRegenAmount = maxShield * .1f * Time.fixedDeltaTime;

        if (sheildRegenAmount * 10f > currentEnergy)
            return;

        currentEnergy -= sheildRegenAmount * 10f;
        currentShield += sheildRegenAmount;
    }

    protected IEnumerator ShowDamage(float damageToShip)
    {
        InfoBlurbManager.CreateInfoBlurb(this.transform.position, damageToShip.ToString("0.00"), Color.red);
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1, .5f, .5f);
        yield return new WaitForSeconds(.1f);
        sr.color = new Color(1, 1, 1);
    }

    public IEnumerator DeathAnim()
    {

        alive = false;
        GetComponent<Collider2D>().enabled = false;

        for (int i = 0; i < explosionSprites.Length; i++)
        {
            if (sr == null)
            {
                Debug.Log("hate hate hate");
            }
            else
            {
                sr.sprite = explosionSprites[i];
                yield return new WaitForSeconds(.1f);
            }
        }

        GameObject.Destroy(this.gameObject);

    }

    public abstract bool CanDamage(bool playerProjectile);
    protected abstract void KillShip(int killerId);

}
