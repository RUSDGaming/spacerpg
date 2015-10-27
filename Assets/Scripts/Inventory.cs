using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour {

    public enum InventoryType
    {
        SHIP_CORE, STATIONARY, SHIP
    }

    public int inventorySize = 10;
    public Item[] items ;

    public InventoryType iventoryType;

	// Use this for initialization
	void Start () {
        items = new Item[inventorySize];       
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    
   public bool StoreItem(Item item)
    {

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
