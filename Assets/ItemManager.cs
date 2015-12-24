using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {


    public static ItemManager instance;

    public  Item[] items;
	// Use this for initialization
	void Start () {
	}
	
    void Awake()
    {
        if (!instance)
        {
            Debug.Log("Setting up item manarger Instance");
            instance = this;
        }

    }

	// Update is called once per frame
	void Update () {
	
	}


    public static Item GetNewItem(int i)
    {

        if(!instance)
        {
            Debug.LogError("no Item Manager Found!!!");
            return null;
        }
        
       // Debug.Log("Createted an item from the item manager with id : " + i);
       if(i < 0 || i >= instance.items.Length)
        {
            Debug.LogError("Item Manager : you gave me an invalid item id! " + i);
            return null;
        }
        Item item = Instantiate(instance.items[i]);
        return item;
    }
}
