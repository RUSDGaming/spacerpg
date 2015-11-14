using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Weapon :Item {

    

    public GameObject projectile;

    ///public Transform parentTransform;

    [SerializeField]
    protected AudioClip weaponSound;

    protected AudioManager audioManager;

    public float damageRatio = 1;
    public float explosionRatio = 0;
    public float laserRatio = 0;
    public float projectileRatio = 0;

    public float damage;
    public float fireRate;    
    public float energyCost;    
    

    protected float lastShot = 0;

 

	// Use this for initialization
	void Start () {

        
        
	}
	public void Init(Transform parentTransform)
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        //this.parentTransform = parentTransform;
        lastShot = -fireRate;
    }

    public virtual bool TryToFire(ref float energy,bool isPlayer,PlayerStats stats)
    {

        //Debug.Log("trying to fire laser with energy : " + energy);
       
        if(Time.time - lastShot  >= getFireRate(stats) )
        {
            if(energy >= energyCost)
            {       
              //  Debug.Log("fired a bullet");
                 energy -= energyCost;
                GameObject projectileInstance = (GameObject) Instantiate(projectile,transform.position,transform.rotation);
                
                ProjectileScript projectileScript = projectileInstance.GetComponent<ProjectileScript>();
                projectileScript.IsPlayer(isPlayer);
                // could optimize code by saving damage values. 
                projectileScript.damage = getWeaponDamage(stats);
                if(stats)
                projectileScript.id = stats.playerInfo.playerId;

                lastShot = Time.time;

                return true;
            }
        }
        return false;

    }
    protected float getFireRate(PlayerStats stats)
    {
        if (stats)
        {
        float rate = fireRate * (1 - stats.statBook[PlayerStats.STATS.FIRE_RATE] * .02f);
        return rate;
        }
        return fireRate;

    }

    protected    float getWeaponDamage(PlayerStats stats) {

        float damage2 = 0f;
        if (stats)
        {
            damage2 += damage;
           damage2 += stats.statBook[PlayerStats.STATS.DAMAGE] * damageRatio;
           damage2 += stats.statBook[PlayerStats.STATS.LASER] * laserRatio;
           damage2 += stats.statBook[PlayerStats.STATS.EXPLOSION] * explosionRatio;
           damage2 += stats.statBook[PlayerStats.STATS.PROJECTILE] * projectileRatio;
            return damage2;
        }

        return damage;
    }
    
    
    public void PlaySound()
    {
        audioManager.playSound(weaponSound);
    }



}
