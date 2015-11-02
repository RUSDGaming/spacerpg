using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Weapon :Item {

    

    public GameObject projectile;

    ///public Transform parentTransform;

    [SerializeField]
    protected AudioClip weaponSound;
    
    protected AudioSource audioSource;

    public float damage;
    public float fireRate;    
    public float energyCost;    
    

    protected float lastShot = 0;

 

	// Use this for initialization
	void Start () {

        
        
	}
	public void Init(Transform parentTransform, AudioSource audioSource)
    {
        this.audioSource = audioSource;
        //this.parentTransform = parentTransform;
        lastShot = -fireRate;
    }

    public virtual bool TryToFire(ref float energy)
    {

        //Debug.Log("trying to fire laser with energy : " + energy);
       
        if(Time.time - lastShot  >= fireRate )
        {
            if(energy >= energyCost)
            {
                audioSource.pitch = Random.Range(1f, 1.3f);
                audioSource.PlayOneShot(weaponSound);
              //  Debug.Log("fired a bullet");
                 energy -= energyCost;
                GameObject projectileInstance = (GameObject) Instantiate(projectile,transform.position,transform.rotation);
                
                ProjectileScript projectileScript = projectileInstance.GetComponent<ProjectileScript>();
                projectileScript.IsPlayer(true);
                projectileScript.damage = damage;
                

                lastShot = Time.time;

                return true;
            }
        }
        return false;

    }




}
