using UnityEngine;
using System.Collections;

public class FlameThrowerWeaponScript : Weapon {


    [SerializeField]    float flameAngle = 10f;
	// Use this for initialization
	void Start () {
        itemType = ItemType.WEAPON;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0)) {
            float blah = 999f;
            TryToFire(ref blah, true, null);
        }
            

	}


    public override bool TryToFire(ref float energy, bool isPlayer, SaveGameInfo stats)
    {


        if (CanFire(energy, stats))
        {
            //  Debug.Log("fired a bullet");
            energy -= energyCost;
            GameObject projectileInstance = (GameObject)Instantiate(projectile, transform.position, transform.rotation);

            float randomRot = Random.Range(-flameAngle, flameAngle);
            projectileInstance.transform.localRotation =   Quaternion.Euler(new Vector3(0, 0, projectileInstance.transform.localEulerAngles.z + randomRot));
            ProjectileScript projectileScript = projectileInstance.GetComponent<ProjectileScript>();
            projectileScript.IsPlayer(isPlayer);
            // could optimize code by saving damage values. 
            projectileScript.damage = getWeaponDamage(stats);


            if (stats != null)
            {
                projectileScript.id = stats.playerId;
                //Debug.Log("player id that was fired is : " + stats.playerId);
            }

            lastShot = Time.time;

            return true;
        }

        return false;

    }
}
