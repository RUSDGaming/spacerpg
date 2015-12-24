using UnityEngine;
using System.Collections;

public class Weapon : Item
{



    public GameObject projectile;

    ///public Transform parentTransform;

    [SerializeField]
    protected AudioClip weaponSound;

    // protected AudioManager audioManager;

    public float damageRatio = .2f;
    public float explosionRatio = 0;
    public float laserRatio = 0;
    public float projectileRatio = 0;
    public float fireRateRatio = .1f;

    public float damage;
    public float fireRate = 1f;
    public float energyCost;
    public float knockBackForce = 2f;


    protected float lastShot = 0;

    protected SaveGameInfo stats;

    [SerializeField]
    protected bool playerOwned;


    // Use this for initialization
    void Start()
    {
        itemType = ItemType.WEAPON;
        //TODO set the players id when you set the weapon

    }
    public virtual void Init(SaveGameInfo sgi, bool playerOwned)
    {
        stats = sgi;
        lastShot = -fireRate;
        this.playerOwned = playerOwned;
    }
    protected virtual bool CanFire(float energy)
    {
        if (energy >= energyCost)
        {

            if (Time.time - lastShot > 1 / shotsPerSecond(stats))
            {

                return true;
            }
        }

        return false;

    }

    public virtual void MouseUp()
    {

    }

    public virtual bool TryToFire(ref float energy)
    {

        if (CanFire(energy))
        {
            //  Debug.Log("fired a bullet");
            energy -= energyCost;
            GameObject projectileInstance = (GameObject)Instantiate(projectile, transform.position, transform.rotation);

            ProjectileScript projectileScript = projectileInstance.GetComponent<ProjectileScript>();
            projectileScript.Init();
            projectileScript.IsPlayer(playerOwned);
            // could optimize code by saving damage values. 
            projectileScript.damage = getWeaponDamage();
            projectileScript.Init();

            if (stats != null)
            {
                projectileScript.id = stats.playerId;
                //Debug.Log("player id that was fired is : " + stats.playerId);

            }
            PlaySound();
            lastShot = Time.time;

            return true;
        }

        return false;

    }
    protected float shotsPerSecond(SaveGameInfo stats)
    {
        if (stats != null)
        {
            float rate = fireRate * (1 + stats.FIRE_RATE * fireRateRatio);
            return rate;
        }
        return fireRate;

    }

    public float getWeaponDamage()
    {

        float damage2 = 0f;
        if (stats != null)
        {
            damage2 += damage;
            damage2 += stats.DAMAGE * damageRatio;
            damage2 += stats.LASER * laserRatio;
            damage2 += stats.EXPLOSION * explosionRatio;
            damage2 += stats.PROJECTILE * projectileRatio;
            return damage2;
        }

        return damage;
    }


    public void PlaySound()
    {
     //   AudioManager.playSound(weaponSound);
       // Debug.Log("Playing Sound");
    }



}
