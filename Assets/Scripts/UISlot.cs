using UnityEngine;
using UnityEngine.UI;
using System;

[RequireComponent (typeof(Image))]
public class UISlot : MonoBehaviour  {


    [SerializeField]
    Color coreColor;
    [SerializeField]
    Color shipColor;

    [SerializeField]
    Color chestColor;

    [SerializeField]
    Image image;


    public void Start()
    {
        image = GetComponent<Image>();
    
    }
    public GameObject item
    {
        get
        {
            if(transform.childCount > 0)
            {
                return transform.GetChild (0).gameObject;
            }
            return null;
        }
    }


    public virtual void SetItem(ItemDragScript script)
    {
        Item itemToSwap;

        try
        {
        itemToSwap = script.realGamePrefab.GetComponent<Item>();
        }
        catch(Exception e) 
        {
            itemToSwap = null;
        }
        Item currentItem;
        try
        {
         currentItem = transform.GetChild(0).GetComponent<ItemDragScript>().realGamePrefab.GetComponent<Item>();
        }catch (Exception e)
        {
           currentItem = null;
        }

        if (CanItemGoInSlot(itemToSwap) )
        {
            UISlot otherSlot = script.transform.parent.GetComponent<UISlot>();
            if (otherSlot.CanItemGoInSlot(currentItem))
            {
                Transform currentItemTransform = transform.GetChild(0);
                currentItemTransform.transform.SetParent(script.transform.parent);
                currentItemTransform.transform.localPosition = Vector3.zero;

                script.transform.SetParent(this.transform);
                script.transform.localPosition = Vector3.zero;
            }

        }
    }

    protected virtual bool CanItemGoInSlot(Item item)
    {
        return true;
    }
    
    
    public void SetColor(Inventory.InventoryType type)
    {
        switch (type)
        {
            case Inventory.InventoryType.SHIP:
                {
                    image.color = shipColor;
                    break;
                }
            case Inventory.InventoryType.SHIP_CORE:
                {                   
                    image.color = coreColor;
                    break;
                }
            case Inventory.InventoryType.STATIONARY:
                {
                    image.color = chestColor;
                    break;
                }
            case Inventory.InventoryType.WEAPON_SLOT:
                {
                    //image.color = coreColor;
                    break;
                }
            default:
                {
                    break;
                }

        }
    }


}
