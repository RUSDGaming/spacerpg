using UnityEngine;
using System.Collections;
using Game.Interfaces;
using System;
using Game.Events;

public class EnemyShip : MonoBehaviour , iShip{


    public WeaponInventory[] weaponSlots;

    #region Ship Base Stats ignored for now... 
    [SerializeField]    public float baseHealth;
    [SerializeField]    public float baseArmor;
    [SerializeField]    public float baseMaxSheild;
    [SerializeField]    public float baseMaxEnergy;
    [SerializeField]    public float baseEnergyRegen;
    [SerializeField]    public float baseMoveForce;
    [SerializeField]    public float baseMaxSpeed;
    [SerializeField]    public float baseTurnRate;
    #endregion


    #region Actual Stats
    [SerializeField]    float maxHealth;
    [SerializeField]    float currentHealth;
    [SerializeField]    float armor;
    [SerializeField]    float maxSheild;
    [SerializeField]    float currentSheild;
    [SerializeField]    float maxEnergy;
    [SerializeField]    float currentEnergy;
    [SerializeField]    float energyRegen;
    [SerializeField]    float moveForce;
    [SerializeField]    float maxSpeed;
    [SerializeField]    float turnRate;
    #endregion

    float lastWeaponSoundPlayed;
    float weaponSoundRate = .1f;
    // how much exp this unit gives on death
    [SerializeField]   float exp;

    Rigidbody2D body;
    ShowForceFeild forceFeild;

    // Use this for initialization
    void Start () {

    body = GetComponent<Rigidbody2D>();
    forceFeild = GetComponentInChildren<ShowForceFeild>();
        lastWeaponSoundPlayed = -weaponSoundRate;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        regenEnergy();
        RegenSheild();
    }

    public void Damage(float damageAmount)
    {
        float damageToShip = 0;
        damageToShip = DamageSheild(damageAmount);
        damageToShip = DamageArmor(damageToShip);
        currentHealth -= damageToShip;
        if (currentHealth <= 0)
        {
            DestroyShip();
        }

        if (damageToShip > 0)
            InfoBlurbManager.CreateInfoBlurb(this.transform.position, damageToShip.ToString(".0"), Color.red);
    }

    public void DestroyShip()
    {

        EnemyDiedEventArgs args = new EnemyDiedEventArgs { playerId = 1, exp = this.exp };
        GameEventSystem.PublishEvent(typeof(EnemyDiedSubscriber), args);

        GameObject.Destroy(this.gameObject);
    }

    public void MoveUnit(Vector2 force, bool relativeInput)
    {
        throw new NotImplementedException();
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

    public void SetActualStats(PlayerStats stats, bool heal)
    {
        throw new NotImplementedException();
    }

    public bool TryToFire(int weaponGroup,bool isPlayer)
    {
        foreach (WeaponInventory weaponSlot in weaponSlots)
        {
            if (weaponSlot.items[0] != null)
            {
                Weapon weapon = weaponSlot.items[0].GetComponent<Weapon>();                
                if (weapon.TryToFire(ref currentEnergy, false, null))
                {
                        //Debug.Log(Time.time - lastWeaponSoundPlayed + ">" +weaponSoundRate);
                    if (Time.time - lastWeaponSoundPlayed > weaponSoundRate)
                    {
                        lastWeaponSoundPlayed = Time.time;
                        weapon.PlaySound();

                    }
                }
            }

        }
        return false;
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

    float DamageSheild(float damage)
    {
        currentSheild -= damage;
        forceFeild.ShowSheild(maxSheild, currentSheild);
        if (currentSheild >= 0)
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
