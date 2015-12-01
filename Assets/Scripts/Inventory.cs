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

    
    // i probbally shouldnt do this it just is funny... lol
    public virtual bool ItemSits(Item item , int index)
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

   public virtual bool ItemSits(Item item)
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

    public virtual bool ItemFits(Item item)
    {

        return true;
    }

    

    public virtual void LoadItemsIntoUI(Transform ui)
    {
        //Debug.Log("starting the thing" +items.Length);

        for (int i = 0; i < items.Length; i++)
        {
           // Debug.Log("aoeu");
            UISlot2 slot = ui.GetChild(i).GetComponent<UISlot2>();
            slot.inventory = this;
            slot.gameObject.SetActive(true);
            slot.SetUpItem(items[i], i);
        }
    }

}
