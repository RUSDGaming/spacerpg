using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ShipSlot : MonoBehaviour, IDropHandler
{


  

    public WeaponSlot weaponSlot;

 
    

    public GameObject item
    {
        get
        {
            if (transform.childCount > 0)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            Debug.Log("Dragged an item onto the ship");
           // ItemDragScript.selecetdItem.transform.SetParent(this.transform);
          //  ItemDragScript.selecetdItem.transform.localPosition = Vector3.zero;

            weaponSlot.SetUpWeapon(item.GetComponent<ItemDragScript>().realGamePrefab);

        }
    }
    public void removeItem()
    {
        weaponSlot.SetUpWeapon(null);
    }

    
}
