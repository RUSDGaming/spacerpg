using UnityEngine;
using System.Collections;

public class ProjectileWeapon : Weapon {

    public int maxAmmo;
    public int currentAmmo;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override bool TryToFire(ref float energy,bool isPlayer,PlayerStats stats)
    {

        //Debug.Log("trying to fire laser with energy : " + energy);

        if (Time.time - lastShot >= fireRate)
        {
            if (currentAmmo > 0)
            {
                //audioSorce.pitch = Random.Range(1f, 1.3f);
                // audioSorce.PlayOneShot(laserSound);
                //  Debug.Log("fired a bullet");
                currentAmmo--;
                GameObject projectileInstance = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
                ProjectileScript projectileScript = projectileInstance.GetComponent<ProjectileScript>();
                projectileScript.IsPlayer(isPlayer);
                projectileScript.damage = damage;
                if(stats)
                projectileScript.id = stats.playerInfo.playerId;
                lastShot = Time.time;
                return true;
            }
        }
        return false;

    }

}
