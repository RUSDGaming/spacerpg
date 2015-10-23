using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {


    public GameObject projectile;

    public Transform parentTransform;

    [SerializeField]
    AudioClip laserSound;
    
    AudioSource audioSorce;

    public float damage;
    public float fireRate;    
    public float energyCost;    
    public int ammo;

    private float lastShot = 0;

 

	// Use this for initialization
	void Start () {

        
        
	}
	public void Init(Transform parentTransform, AudioSource audioSorce)
    {
        this.audioSorce = audioSorce;
        this.parentTransform = parentTransform;
        lastShot = -fireRate;
    }

    public bool TryToFire(float energy, out float energyOut)
    {

        
        energyOut = energy;
        if(Time.time - lastShot  >= fireRate )
        {
            if(energy >= energyCost)
            {
                audioSorce.pitch = Random.Range(1f, 1.3f);
                audioSorce.PlayOneShot(laserSound);
              //  Debug.Log("fired a bullet");
                energyOut = energy - energyCost;
                GameObject projectileInstance = (GameObject) Instantiate(projectile,parentTransform.position,parentTransform.rotation);
                projectileInstance.SendMessage("IsPlayer", true);
                lastShot = Time.time;

                return true;
            }
        }
        return false;

    }




}
