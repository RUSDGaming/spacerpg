﻿using UnityEngine;
using System.Collections;

using Game.Interfaces;
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
        //Debug.Log("Trying to fire miniGun");
        if(energy < energyCost)
        {
            //Debug.Log("energy was to lowe");
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

        energy -= energyCost;
        Vector3 spawnPos = transform.position;
        spawnPos.x += Random.Range(-.3f, .3f);
        GameObject projectileInstance = (GameObject)Instantiate(projectile, spawnPos, transform.rotation);

        ProjectileScript projectileScript = projectileInstance.GetComponent<ProjectileScript>();
        projectileScript.IsPlayer(playerOwned);
        // could optimize code by saving damage values. 
        projectileScript.damage = getWeaponDamage();
        if(stats != null)
        projectileScript.id = stats.playerId;

        projectileScript.Init();
        lastShot = Time.time;

        fireRate -= fireRateDecrease;
        if(fireRate < maxFireRate)
        {
            fireRate = maxFireRate;
        }

        return true;

        


    }

}
