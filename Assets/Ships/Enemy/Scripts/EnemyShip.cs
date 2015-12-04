using UnityEngine;
using System.Collections;
using Game.Interfaces;
using System;
using Game.Events;

public class EnemyShip : MonoBehaviour , iShip{


    [SerializeField]    Sprite[] explosionSprites;
    Sprite baseSprite;
    SpriteRenderer sr;
    public bool alive = true;

    public WeaponInventory[] weaponSlots;

    #region Ship Base Stats ignored for now... 
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
    public    float maxHealth;
    public    float currentHealth;
    public    float armor;
    public    float maxShield;
    public    float currentShield;
    public    float maxEnergy;
    public    float currentEnergy;
    public    float energyRegen;
    public    float moveForce;
    public    float maxSpeed;
    public    float turnRate;
    #endregion

    float lastWeaponSoundPlayed;
    float weaponSoundRate = .1f;
    // how much exp this unit gives on death
    [SerializeField]   float exp;

    Rigidbody2D body;
    ShowForceFeild forceFeild;

    // Use this for initialization
    protected void Start () {
       // Debug.Log(this);
        sr = GetComponent<SpriteRenderer>();
        baseSprite = GetComponent<SpriteRenderer>().sprite;
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
        RegenShield();
    }

    public void Damage(float damageAmount, int damagerId)
    {
        float damageToShip = 0;
        damageToShip = DamageShield(damageAmount);
        damageToShip = DamageArmor(damageToShip);
        currentHealth -= damageToShip;
        if (currentHealth <= 0)
        {
            DestroyShip(damagerId);
        }

        if (damageToShip > 0)
        {

            StartCoroutine(ShowDamage(damageToShip));

        }
    }
    private IEnumerator ShowDamage(float damageToShip)
    {
        InfoBlurbManager.CreateInfoBlurb(this.transform.position, damageToShip.ToString(".0"), Color.red);
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1, .5f, .5f);
        yield return new WaitForSeconds(.1f);
        sr.color = new Color(1, 1, 1);
    }


    public void DestroyShip(int killerdId)
    {
       // Debug.Log("ship dide" + killerdId);
        // all the ai will have negative ids
        if(killerdId > 0)
        {
        EnemyDiedEventArgs args = new EnemyDiedEventArgs { playerId = 1, exp = this.exp };
        GameEventSystem.PublishEvent(typeof(EnemyDiedSubscriber), args);
        }

        DropItem dropItem = GetComponent<DropItem>();
        if (dropItem)
        {
            dropItem.SpawnItem();
        }
        StartCoroutine(DeathAnim());

    }

    public IEnumerator DeathAnim()
    {

        alive = false;
        GetComponent<BoxCollider2D>().enabled = false;

        for (int i = 0 ; i < explosionSprites.Length; i++)
        {
            if (sr== null)
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

    public void MoveUnit(Vector2 moveDir, bool relativeInput)
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

        //if (thrust)
        //    thrust.thrust(moveDir.x, moveDir.y);
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

    public void SetActualStats(SaveGameInfo stats, bool heal)
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

    float DamageShield(float damage)
    {
        currentShield -= damage;
        if(forceFeild)
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

        if(currentShield >= maxShield)
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
}
