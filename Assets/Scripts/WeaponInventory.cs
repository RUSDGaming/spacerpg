using UnityEngine;
using System.Collections;

public class WeaponInventory : Inventory
{


    // public GameObject weapon;
    // public GameObject weapon_Inv;
    Weapon weaponScript;



    [SerializeField]
    bool aboveShip = false;


    void Start()
    {
        inventoryType = InventoryType.WEAPON_SLOT;


        if (items[0] != null)
        {
            SetUpWeapon(items[0]);
        }


    }

    public override bool ItemSits(Item item, int index)
    {
        //Debug.Log("trying to store Item with index : " + index);
        SetUpWeapon(item);
        if (item)
        {
            item.transform.SetParent(cargo);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            item.gameObject.SetActive(true);
        }

        items[index] = item;

        return false;

    }
    public override bool ItemFits(Item item)
    {

        if (item)
        {
            if (item.itemType == Item.ItemType.WEAPON)
                return true;
        }
        else
            return true;
        return false;
    }

    public void SetUpWeapon(Item wep)
    {


        if (wep)
        {
            wep.gameObject.SetActive(true);
            // tell s the stored prefab wher it is
            weaponScript = wep.GetComponent<Weapon>();
            weaponScript.Init(this.transform);
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
