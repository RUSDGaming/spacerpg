using UnityEngine;
using System.Collections;
using Game.Interfaces;
public class SunScript : MonoBehaviour
{
    public float MaxDamage = 1;
    public float maxDist = 70;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.isTrigger)
            return;

        DamageUnit du = other.GetComponent<DamageUnit>();
        if(du != null)
        {
            float dist = Vector3.Distance(other.transform.position, transform.position);
            float damage = maxDist - dist;

            if(damage  > 0)
            {
                damage = Mathf.Pow( damage / 9,4) ;
                BurnScript bs = other.gameObject.GetComponent<BurnScript>();
                if(!bs)
                {
                    bs = other.gameObject.AddComponent<BurnScript>();
                    bs.init(1f, .5f, damage,-1);
                }
                else
                {
                    bs.refresh(1f, .5f, damage, -1);
                }

            }
        }


    }
}
