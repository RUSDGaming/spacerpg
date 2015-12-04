using System;
using System.Collections;
using UnityEngine;
[Serializable]
public class SaveGameShip : SaveGame {


    public int shipId;
    public SaveGameItem[] WeaponSlots;
    public SaveGameItem[] cargo;




    public void GetItems(ref Item[] items)
    {

        // Debug.Log(items.Length);
        for (int i = 0; i < cargo.Length; i++)
        {
            if (i < items.Length)
            {
              //  Debug.Log("getting item");
                if (cargo[i] == null)
                    continue;
                // Debug.Log("item was not null?");
                Item item = ItemManager.GetNewItem(cargo[i].id);
                if (item)
                {
                    item.currentSize = cargo[i].currentSize;
                    items[i] = item;

                }
                
            }
        }


    }

    public void SetItems(Item[] items)
    {
        

               // Debug.Log(items.Length);
        cargo = new SaveGameItem[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                //Debug.Log(items[i].id +" id is : ");
                cargo[i] = new SaveGameItem();
                cargo[i].id = items[i].id;
                cargo[i].currentSize = items[i].currentSize;
            }
        }

    }
}
