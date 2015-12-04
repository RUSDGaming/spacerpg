using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour
{

    public enum InventoryType
    {
        SHIP_CORE, STATIONARY, SHIP, WEAPON_SLOT
    }

    [SerializeField]
    bool loadInventoryFromSave = false;
    [SerializeField]
    string inventoryName = "notSet";
    public int inventorySize = 10;
    public Item[] items;

    public InventoryType inventoryType;
    [SerializeField]
    protected Transform cargo;

    public void Update()
    {
        if (loadInventoryFromSave)
        {

        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveItems();
        }

        
        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadSavedItems();
        }
        }

    }


    void Awake()
    {
        //Debug.Log("waking up....");
        items = new Item[inventorySize];
       
    }

    // Use this for initialization
    void Start()
    {
     if (loadInventoryFromSave)
        {
            LoadSavedItems();
        }
        else
        {
            if (cargo)
                for (int i = 0; i < cargo.childCount; i++)
                {
                    items[i] = cargo.GetChild(i).GetComponent<Item>();
                }
        }

    }


    // i probbally shouldnt do this it just is funny... lol
    public virtual bool ItemSits(Item item, int index)
    {

       // Debug.Log("setting an item with index: " + index+ " and this item is : " +item);
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

        Debug.Log("Setting an item without an index...");
        if (item)
        {
            item.transform.SetParent(cargo);
            item.transform.localPosition = Vector3.zero;
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
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

    public void LoadSavedItems()
    {
       // Debug.Log("loading inevtory from save");
        string fileName = PlayerPrefs.GetString(LoadPannel.current);
        SaveGameInventory sgi = SaveGameSystem.LoadGame(fileName + inventoryName) as SaveGameInventory;
        if (sgi != null)
        {
                sgi.getItems(ref items);
            
            
        }

    }
    public void SaveItems()
    {
        SaveGameInventory sgi = new SaveGameInventory();
        sgi.SetItems(items);
        string fileName = PlayerPrefs.GetString(LoadPannel.current);
        SaveGameSystem.SaveGame(sgi, fileName + inventoryName);
        //Debug.Log("saved inventory: " + inventoryName);
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
