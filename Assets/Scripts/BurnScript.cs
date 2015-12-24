using UnityEngine;
using System.Collections;
using Game.Interfaces;

public class BurnScript : MonoBehaviour
{
    public float burnTime;
    public float lastBurn;
    public float  burnRate;
    public float damage = 1;
    public int burnerId = -1;

    iDamage du;
    // Use this for initialization
    void Start()
    {
        du = GetComponent<iDamage>();
    }

    public void init(float burnDurration, float burnRate, float damage, int id)
    {
        du = GetComponent<iDamage>();
        burnTime = burnDurration;
        burnerId = id;
        this.damage = damage;
        this.burnRate = burnRate;
        lastBurn = -burnRate;
    }
    public void refresh(float burnDurration,float burnRate, float damage, int id)
    {
        burnerId = id;
        this.damage = damage;
        this.burnRate = burnRate;
        burnTime = burnDurration;

    }

    public void FixedUpdate()
    {
        if( Time.time >= lastBurn  + burnRate)
        {
            du.Damage(damage, burnerId);
            lastBurn = Time.time;
        }
            burnTime -= Time.fixedDeltaTime;

        if(burnTime <= 0)
        {
            Destroy(this);
        }

    }
}
