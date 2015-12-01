using UnityEngine;
using System.Collections;

public class Weapon :Item {

    

    public GameObject projectile;

    ///public Transform parentTransform;

    [SerializeField]
    protected AudioClip weaponSound;

    protected AudioManager audioManager;

    public float damageRatio = .2f;
    public float explosionRatio = 0;
    public float laserRatio = 0;
    public float projectileRatio = 0;

    public float damage;
    public float fireRate;    
    public float energyCost;
    public float knockBackForce = 2f;
    

    protected float lastShot = 0;

 

	// Use this for initialization
	void Start () {

        
        
	}
	public void Init(Transform parentTransform)
    {

        if (GameObject.FindGameObjectWithTag("AudioManager"))
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        //this.parentTransform = parentTransform;
        lastShot = -fireRate;
    }

    public virtual bool TryToFire(ref float energy,bool isPlayer,SaveGameInfo stats)
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
                if(stats != null)
                {

                    projectileScript.id = stats.playerId;
                    //Debug.Log("player id that was fired is : " + stats.playerId);
                }

                lastShot = Time.time;

                return true;
            }
        }
        return false;

    }
    protected float getFireRate(SaveGameInfo stats)
    {
        if (stats != null)
        {
        float rate = fireRate * (1 - stats.FIRE_RATE * .02f);
        return rate;
        }
        return fireRate;

    }

    protected    float getWeaponDamage(SaveGameInfo stats) {

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
        audioManager.playSound(weaponSound);
    }



}
