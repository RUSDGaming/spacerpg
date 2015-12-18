
using UnityEngine;
using System.Collections;

using Game.Interfaces;
using System;

public class ShipCore : MonoBehaviour , iShip {

    [SerializeField]
    float currentHealth = 5;
    [SerializeField]
    float maxHealth = 5;
    [SerializeField]
    float currentEnergy =10;
    [SerializeField]
    float maxEnergy = 10;
    [SerializeField]
    float energyRegen = 2;
    [SerializeField]
    float energyCost =  1f;
    [SerializeField]
    float fireRate = 1f;
    [SerializeField]
    float lastShot;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip laserSound;
    [SerializeField]
    float moveForce = 5000;
    [SerializeField]
    float maxSpeed = 10f;

    public int itemSlots;


    Rigidbody2D body;

    // Use this for initialization
    void Start () {
        currentEnergy = maxEnergy;
        currentHealth = maxHealth;
        body  = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

	}
    void FixedUpdate()
    {
        regenEnergy();

    }

    void regenEnergy()
    {
        currentEnergy += energyRegen * Time.fixedDeltaTime;
        if(currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
    }

    public void Damage(float damageAmount, int damagerId)
    {
        this.currentHealth -= damageAmount;
        if(this.currentHealth <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        Debug.Log("Game Over Sad Face");
        Application.LoadLevel("MainMenu");
    }

    public bool TryToFire(int weaponGroup,bool isPlayer)
    {
        if (Time.time - lastShot >= fireRate)
        {
            if (currentEnergy >= energyCost)
            {
                audioSource.pitch = UnityEngine.Random.Range(1f, 1.3f);
                audioSource.PlayOneShot(laserSound);
                //  Debug.Log("fired a bullet");
                currentEnergy -= energyCost;
                GameObject projectileInstance = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
                projectileInstance.SendMessage("IsPlayer", true);
                lastShot = Time.time;

                return true;
            }
        }
        return false;
    }

    public void MoveUnit(Vector2 force,bool relativeInput)
    {
        if (relativeInput)
        {
            body.AddRelativeForce(force * Time.fixedDeltaTime * moveForce);
        }
        else
        {
            body.AddForce(force * Time.fixedDeltaTime * moveForce);
        }

        if (body.velocity.magnitude > maxSpeed)
        {
            body.velocity = body.velocity.normalized * maxSpeed;
        }

    }

    public void RotateUnit(float deg)
    {
        body.MoveRotation(body.rotation + deg);
    }

    public void SetActualStats(SaveGameInfo stats, bool heal)
    {
        //Debug.Log("ship core not set up yet to load actual stats");
    }

    public bool CanDamage(bool playerProjectile)
    {
        throw new NotImplementedException();
    }
}
