using UnityEngine;
using System.Collections;

public class ChargeLaserWeaponScript : Weapon
{

    [SerializeField]    float chargeTime = .5f;
    //[SerializeField]    int numCharges = 4;
    [SerializeField]    Sprite[] sprites;
    [SerializeField]    int currentCharge = 0;
    [SerializeField]    SpriteRenderer sr;

    bool playerOwned = true;
    float chargeTimer;

    public void Start()
    {
        itemType = ItemType.WEAPON;
        chargeTimer = chargeTime;
    }

    public override void MouseUp()
    {
        if(currentCharge > 0)
        {
            GameObject projectileInstance = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
            projectileInstance.transform.localScale = new Vector3(currentCharge *.7f, currentCharge *.7f,1);
            ProjectileScript projectileScript = projectileInstance.GetComponent<ProjectileScript>();
            projectileScript.speed *= (1 + ((currentCharge-1) * .5f));
            projectileScript.Init();
            projectileScript.IsPlayer(playerOwned);
            // could optimize code by saving damage values. 
            projectileScript.damage = getWeaponDamage(null) * Mathf.Pow(currentCharge,1.5f );
            
            PlaySound();
            // fire projectile
        }
        currentCharge = 0;
        sr.sprite = sprites[currentCharge];
        chargeTimer = chargeTime;
    }


    public override bool TryToFire(ref float energy)
    {
        
        chargeTimer -= Time.fixedDeltaTime;
        if(chargeTimer <= 0)
        {
            if(currentCharge  +1 < sprites.Length)
            {
                currentCharge++;
                sr.sprite = sprites[currentCharge];
            }
            chargeTimer = chargeTime;
        }

        return true;

    }



}
