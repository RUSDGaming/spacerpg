using UnityEngine;
using System.Collections;

public class Weapon :Item {

    

    public GameObject projectile;

    ///public Transform parentTransform;

    [SerializeField]
    protected AudioClip weaponSound;

   // protected AudioManager audioManager;

    public float damageRatio = .2f;
    public float explosionRatio = 0;
    public float laserRatio = 0;
    public float projectileRatio = 0;

    public float damage;
    public float fireRate = 1f;    
    public float energyCost;
    public float knockBackForce = 2f;
    

    protected float lastShot = 0;

 

	// Use this for initialization
	void Start () {
        itemType = ItemType.WEAPON;
        
        
	}
	public void Init(Transform parentTransform)
    {

       // if (GameObject.FindGameObjectWithTag("AudioManager"))
       // audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        //this.parentTransform = parentTransform;
        lastShot = -fireRate;
    }
    protected virtual bool CanFire(float energy,SaveGameInfo stats)
    {
        if(energy >= energyCost)
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

    public virtual bool TryToFire(ref float energy,bool isPlayer,SaveGameInfo stats)
    {

        
            if(CanFire(energy,stats))
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
        
        return false;

    }
    protected float shotsPerSecond(SaveGameInfo stats)
    {
        if (stats != null)
        {
            float rate = fireRate * (1+ stats.FIRE_RATE * .1f);
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
        AudioManager.playSound(weaponSound);
    }



}
