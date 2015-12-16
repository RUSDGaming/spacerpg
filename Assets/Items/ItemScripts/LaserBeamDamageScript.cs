using UnityEngine;
using System.Collections;

using Game.Interfaces;
public class LaserBeamDamageScript : MonoBehaviour
{

    //GameObject hitTarget;
    iDamage target;
    [SerializeField] LaserBeamWeaponScript laser;
    
    float damageTimer;


    public void FixedUpdate()
    {

        damageTimer -= Time.fixedDeltaTime;

        if (damageTimer <= 0)
        {
            if(target != null)
            {
                damageTimer = laser.damageTickRate;
                target.Damage(laser.damage * laser.damageTickRate,1);
            }
        }
    }

    public void SetTarget(GameObject go)
    {
        //hitTarget = go;
        if (go)
            target = go.GetComponent<iDamage>();
        else
            target = null;
    }
    


}
