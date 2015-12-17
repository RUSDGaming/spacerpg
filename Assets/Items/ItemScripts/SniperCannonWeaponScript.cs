using UnityEngine;
using System.Collections;

using Game.Interfaces;
public class SniperCannonWeaponScript : Weapon{

    [SerializeField]    float maxDist = 40f;
    [SerializeField]    LayerMask layers;
    [SerializeField]    GameObject smokeTrail;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public override bool TryToFire(ref float energy, bool isPlayer, SaveGameInfo stats)
    {
        //return base.TryToFire(ref energy, isPlayer, stats);
        if (CanFire(energy, stats))
        {
           
            createSmokeTrail();
            lastShot = Time.time;
            return true;
        }
        return false;
    }


    void createSmokeTrail()
    {

        Vector3 smokePos = smokeTrail.transform.localPosition;
        Vector3 scale = smokeTrail.transform.localScale;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, maxDist, layers);
        if (hit.collider != null)
        {
            iDamage target = hit.collider.gameObject.GetComponent<iDamage>();
            if (target != null)
            {
                target.Damage(damage, 1);
            }

            Debug.Log(hit.collider.gameObject);
            smokePos.y = hit.distance / 2;
            scale.y = hit.distance * 8;
            StartCoroutine(HideSmoke());
            //laserBeam.SetTarget(hit.collider.gameObject);
        }
        else
        {
            smokePos.y = maxDist / 2;
            scale.y = maxDist * 8;
            StartCoroutine(HideSmoke());
            //laserBeam.SetTarget(null);
        }
        smokeTrail.transform.localPosition = smokePos;
        smokeTrail.transform.localScale = scale;
    }
    IEnumerator HideSmoke()
    {
        SpriteRenderer sr = smokeTrail.GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, 1);

        for (int i = 60; i >= 0; i-=5)
        {
          //  Debug.Log("blah");
            sr.color = new Color(1, 1, 1,i/60f );
            //yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(.01f);
          
        }

    }



}
