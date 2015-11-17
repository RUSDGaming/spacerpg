using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public enum InventoryType
    {
        SHIP_CORE, STATIONARY, SHIP, WEAPON_SLOT
    }

    public int inventorySize = 10;
    public Item[] items ;

    public InventoryType inventoryType;
    [SerializeField]
    protected Transform cargo;

	// Use this for initialization
	void Start () {
       
            items = new Item[inventorySize]; 
        if(cargo)      
        for(int i = 0; i < cargo.childCount; i++)
        {
            items[i] = cargo.GetChild(i).GetComponent<Item>();
        }

	}	

    

    public virtual bool StoreItem(Item item , int index)
    {
        if (item)
        {
        item.transform.SetParent(cargo);
        item.transform.localPosition = Vector3.zero;
        item.gameObject.SetActive(false);
        }

        items[index] = item;

        return false;
    }

   public virtual bool StoreItem(Item item)
    {
        item.transform.SetParent(cargo);
        item.transform.localPosition = Vector3.zero;
        item.gameObject.SetActive(false);

        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) {
                items[i] = item;
                return true;
            }
        }
        return false;
             
    }

}
