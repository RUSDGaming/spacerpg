using UnityEngine;
using System.Collections;

public class ChainLaser : Weapon {




    [SerializeField] float maxFireRate = .5f;
    [SerializeField] float fireRateDecrease = .5f;
    [SerializeField] float minFireRate = 3f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public override bool TryToFire(ref float energy)
    {
        Debug.Log("Trying to fire miniGun");
        if(energy < energyCost)
        {
            Debug.Log("energy was to lowe");
            return false;
        }        

        while(Time.time - lastShot >= fireRate + fireRateDecrease)
        {
            fireRate += fireRateDecrease;
            if(fireRate > minFireRate)
            {
                fireRate = minFireRate;
                break;
            }
        }

        if(Time.time - lastShot < fireRate)
        {
            return false;
        }

        audioSorce.pitch = Random.Range(1f, 1.3f);
        audioSorce.PlayOneShot(laserSound);
        energy -= energyCost;
        Vector3 spawnPos = parentTransform.position;
        spawnPos.x += Random.Range(-.3f, .3f);
        GameObject projectileInstance = (GameObject)Instantiate(projectile, spawnPos, parentTransform.rotation);
        projectileInstance.SendMessage("IsPlayer", true);
        lastShot = Time.time;

        fireRate -= fireRateDecrease;
        if(fireRate < maxFireRate)
        {
            fireRate = maxFireRate;
        }

        return true;

        


    }

}
