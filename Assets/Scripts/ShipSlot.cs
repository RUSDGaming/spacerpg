using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ShipSlot : Slot, IDropHandler
{


  

    public WeaponSlot weaponSlot;


    public override void SetItem(ItemDragScript script)
    {
        base.SetItem(script);


    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!item)
        {
            Debug.Log("Dragged an item onto the ship");
           // ItemDragScript.selecetdItem.transform.SetParent(this.transform);
          //  ItemDragScript.selecetdItem.transform.localPosition = Vector3.zero;

          //  weaponSlot.SetUpWeapon(item.GetComponent<ItemDragScript>().realGamePrefab);

        }
    }
    public void removeItem()
    {
      //  weaponSlot.SetUpWeapon(null);
    }

    
}
