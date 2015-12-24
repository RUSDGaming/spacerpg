using UnityEngine;
using System.Collections;

public class FlameThrowerWeaponScript : Weapon {


    [SerializeField]    float flameAngle = 10f;
    [SerializeField]    GameObject flameCone;
    
	// Use this for initialization
	void Start () {
        itemType = ItemType.WEAPON;
	}
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetMouseButton(0)) {
        //    float blah = 999f;
        //    TryToFire(ref blah, true, null);
        //}
            

	}

    protected override bool CanFire(float energy)
    {
        if(energy > energyCost * Time.fixedDeltaTime)
        {
            return true;
        }
        return false;
    }
    public override void MouseUp()
    {
        flameCone.SetActive(false);
    }

    public override bool TryToFire(ref float energy)
    {


        if (CanFire(energy))
        {
            flameCone.SetActive(true);
            //  Debug.Log("fired a bullet");
            energy -= energyCost * Time.fixedDeltaTime;
            GameObject projectileInstance = (GameObject)Instantiate(projectile, transform.position, transform.rotation);

            float randomRot = Random.Range(-flameAngle, flameAngle);
            projectileInstance.transform.localRotation =   Quaternion.Euler(new Vector3(0, 0, projectileInstance.transform.localEulerAngles.z + randomRot));
            FlameParticleScript flame = projectileInstance.GetComponent<FlameParticleScript>();
            Rigidbody2D body = GetComponentInParent<Rigidbody2D>();
            if (body)
            {
                //Debug.Log("Found the body");
                flame.init(body.velocity);
            }
            else
            {
                flame.init(Vector2.zero);
            }
            //projectileScript.IsPlayer(isPlayer);
            // could optimize code by saving damage values. 
            //projectileScript.damage = getWeaponDamage(stats);


            if (stats != null)
            {
                //projectileScript.id = stats.playerId;
                //Debug.Log("player id that was fired is : " + stats.playerId);
            }

            lastShot = Time.time;

            return true;
        }
        else
        {
            flameCone.SetActive(false);
        }

        return false;

    }
}
