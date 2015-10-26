using UnityEngine;
using System.Collections;
using Game.Interfaces;
using System;

public class EnemySpawnScript : MonoBehaviour, DamageUnit{

    [SerializeField]
    GameObject enenmyToSpawn;

    [SerializeField]
    float spawnRate = 5;

    [SerializeField]
    float maxHealth = 50f;


    float lastSpawn;
    float currentHealth;
    

    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Time.time - lastSpawn >= spawnRate)
        {
            GameObject.Instantiate(enenmyToSpawn);
            lastSpawn = Time.time;
        }


	}
    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Destroy();
        }
    }


    void Destroy()
    {
        Destroy(gameObject);

    }
}
