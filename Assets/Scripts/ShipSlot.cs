using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ShipSlot : UISlot
{


    public override void SetItem(ItemDragScript script)
    {
        base.SetItem(script);
    }


    protected override bool CanItemGoInSlot(Item item)
    {
        if(item == null)
        {
            return true;
        }
        if(item is Weapon){
            return true;
        }
        return false;
        
    }


}
