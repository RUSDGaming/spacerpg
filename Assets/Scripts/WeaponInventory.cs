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

    public override bool ItemSits(Item item,int index)
    {

       // Debug.Log("(weapon inv) setting an item with index: " + index + " and this item is : " + item);
        SetUpWeapon(item);
      //  Debug.Log("Weapon was set up?:?:?");
        if (item)
        {
           // Debug.Log("Items stist on weapon slot");
            item.transform.SetParent(cargo);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            item.gameObject.SetActive(true);
        }
        else
        {
            // if you are stetting an item to null there shouldnt be an item in the child...
            // right now there will be a weapon just sitting there, idk how to remove it...
            //if (transform.childCount > 0)
            //{
            //    Debug.Log("Destroing something...");
            //    Destroy(transform.GetChild(0).gameObject);
            //}
            
        }

        items[0] = item;

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
            Collider2D col = wep.GetComponent<Collider2D>();
            if (col)
                col.enabled = false;
            ItemJuice juice = wep.GetComponent<ItemJuice>();
            if (juice)
            {
                juice.enabled = false;
            }
            //Debug.Log("Initing Weapon" + wep);
            wep.gameObject.SetActive(true);

            // tell s the stored prefab wher it is
            weaponScript = wep.GetComponent<Weapon>();
            if (!weaponScript)
            {
                Debug.LogError("You are trying to set up an item that is not a weapon!");
                return;
            }
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
