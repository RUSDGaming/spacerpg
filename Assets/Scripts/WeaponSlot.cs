using UnityEngine;
using System.Collections;

public class WeaponSlot : MonoBehaviour {


    public GameObject weapon;
    public GameObject weapon_Inv;
    Weapon weaponScript;
   
    AudioSource audioSource;
    SpriteRenderer slotSprite;

    [SerializeField]
    bool aboveShip = false;
    
    
    void Start() {

        audioSource = GetComponent<AudioSource>();

        if(weapon != null)
        {
            SetUpWeapon(weapon);
        }

        if (aboveShip)
        {
            slotSprite.sortingOrder = 2;
        }
        else
        {
            slotSprite.sortingOrder = 0;
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
            slotSprite = GetComponent<SpriteRenderer>();
            slotSprite.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            SpriteRenderer slotSprite = GetComponent<SpriteRenderer>();
            slotSprite.sprite = null;
        }
    }

}
