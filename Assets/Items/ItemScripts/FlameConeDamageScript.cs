using UnityEngine;
using System.Collections;
using Game.Interfaces;

public class FlameConeDamageScript : MonoBehaviour
{

    

    public float ticRate = .25f;
    public float damageMin = .5f;
    public float damageMax = 1.5f;
    public float damageIncr = .1f;
    public float burnDurration = .5f;

    public int playerId = 1;



    public void OnTriggerStay2D(Collider2D other)
    {
        
        //Debug.Log(other.gameObject);
        if (other.isTrigger)
            return;

        iDamage iDam = other.GetComponent<iDamage>();
        if(iDam != null)
        {
            //Debug.Log("other");
            BurnScript bs = other.gameObject.GetComponent<BurnScript>();
            if (!bs)
            {
                bs = other.gameObject.AddComponent<BurnScript>();
                bs.init(burnDurration, ticRate, damageMin, playerId);
            }
            else
            {
                float damage = bs.damage + damageIncr * Time.fixedDeltaTime;
                if (damage > damageMax)
                    damage = damageMax;
                bs.refresh(burnDurration, ticRate, damage, playerId);
            }
        }
    }
}
