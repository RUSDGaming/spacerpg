using UnityEngine;
using System.Collections;

public class WeaponInventory : Inventory {


   // public GameObject weapon;
   // public GameObject weapon_Inv;
    Weapon weaponScript;
   
    AudioSource audioSource;
    

    [SerializeField]
    bool aboveShip = false;
    
    
    void Start() {
        inventoryType = InventoryType.WEAPON_SLOT;
        audioSource = GetComponent<AudioSource>();

        if(items[0] != null)
        {
            SetUpWeapon(items[0]);
        }

      
    }

    public override bool StoreItem(Item item, int index)
    {
        Debug.Log("trying to store Item with index : " + index);
        SetUpWeapon(item);
        if (item)
        {
            item.transform.SetParent(cargo);
            item.transform.localPosition = Vector3.zero;
            item.gameObject.SetActive(true);
        }

        items[index] = item;

        return false;
        
    }

    public void SetUpWeapon(Item wep)
    {
        
        
        if (wep)
        {
            wep.gameObject.SetActive(true);
            // tell s the stored prefab wher it is
            weaponScript = wep.GetComponent<Weapon>();            
            weaponScript.Init(this.transform,audioSource);
            // gets the sprite from the game object.            
            SpriteRenderer weaponSprite = wep.GetComponent<SpriteRenderer>();

            if (aboveShip)
            {
                weaponSprite.sortingOrder = 2;
            }
            else
            {
                weaponSprite.sortingOrder = 0;
            }

        }
        else
        {
            
            //SpriteRenderer slotSprite = GetComponent<SpriteRenderer>();
            //slotSprite.sprite = null;
        }
    }

}
