﻿using UnityEngine;
using System.Collections;
using Game.Interfaces;
using System;

public class EnemyScript : MonoBehaviour , iDamage {



    public float health = 1;
    public float fireRate = 1f;
    float lastFire;
    public float moveForce = 1000;
    public float maxSpeed = 10;

    [SerializeField]
    AudioClip laserSound;
    [SerializeField]
    AudioSource audioSorce;

    public GameObject projectile;
    
	// Use this for initialization
	void Start () {
        lastFire = -fireRate;
	}
	
	// Update is called once per frame
	void Update () {

        // TryToFire();
	}

    public void Damage(float damage, int damagerId)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy();
        }

    }

    public void Destroy() {
        DestroyObject(this.gameObject); 
    }

    public void TryToFire()
    {
        if(Time.time - lastFire  >= fireRate)
        {
            audioSorce.pitch = UnityEngine.Random.Range(.8f, 1.2f);
            audioSorce.PlayOneShot(laserSound);
            lastFire = Time.time;
            GameObject go = (GameObject) Instantiate(projectile, this.transform.position,this.transform.rotation );
            go.SendMessage("IsPlayer", false);
        }
    }

    public bool CanDamage(bool playerProjectile)
    {
        throw new NotImplementedException();
    }
}
