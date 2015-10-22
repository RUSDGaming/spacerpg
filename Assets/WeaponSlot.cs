using UnityEngine;
using System.Collections;

public class WeaponSlot : MonoBehaviour {


    public GameObject weapon;
    Weapon weaponScript;
    [SerializeField]
    AudioSource audioSource;
    
    
    void Start() {
        if(weapon != null)
        {
            SetUpWeapon(weapon);
        }
        
    }

    public void SetUpWeapon(GameObject wep)
    {

        weapon = wep;
        if (weapon)
        {
            // tell s the stored prefab wher it is
            weaponScript = weapon.GetComponent<Weapon>();            
            weaponScript.Init(this.transform,audioSource);
            // gets the sprite from the game object.
            SpriteRenderer slotSprite = GetComponent<SpriteRenderer>();
            slotSprite.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            SpriteRenderer slotSprite = GetComponent<SpriteRenderer>();
            slotSprite.sprite = null;
        }
    }

}
