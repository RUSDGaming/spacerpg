using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {


    public static ItemManager instance;

    [SerializeField]    Item[] items;
	// Use this for initialization
	void Start () {
        if (!instance)
        {
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
        //Debug.Log("Createted an item from the item manager");
        Item item = Instantiate(instance.items[i]);
        return item;
    }
}
