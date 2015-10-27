using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class Slot : MonoBehaviour ,IDropHandler {


    [SerializeField]
    Color coreColor;
    [SerializeField]
    Color shipColor;

    [SerializeField]
    Color chestColor;


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


    public void SetItem(ItemDragScript script)
    {

        // get a reffrence to this so you can moddifie it after you move it;
        Transform localSlot = gameObject.transform.GetChild(0);
        localSlot.SetParent(script.transform.parent);
        localSlot.localPosition = Vector3.zero;


        script.transform.SetParent(this.transform);
        script.transform.localPosition = Vector3.zero;


    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            //ItemDragScript.selecetdItem.transform.SetParent(this.transform);
            //ItemDragScript.selecetdItem.transform.localPosition = Vector3.zero;

        }
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

        }
    }
}
