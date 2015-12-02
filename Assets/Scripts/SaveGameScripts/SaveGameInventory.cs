using UnityEngine;
using System.Collections;
using System;


[Serializable]
public class SaveGameInventory : SaveGame {

    SaveGameItem[] items;



    public  void getItems(ref Item[] itemz) {

       // Debug.Log(items.Length);
        for (int i = 0; i < items.Length; i++)
        {
            if(i <itemz.Length)
            {
                //Debug.Log("getting item");
                if (items[i] == null)
                    continue;
               // Debug.Log("item was not null?");
                itemz[i] = ItemManager.GetNewItem(items[i].id);
                itemz[i].currentSize = items[i].currentSize;
            }
        }

        
    }

    public void SetItems(Item[] itemz)
    {
        items = new SaveGameItem[itemz.Length];
        for (int i = 0; i < itemz.Length; i++)
        {
            if (itemz[i] != null)
            {
                Debug.Log("setting an item");
                items[i] = new SaveGameItem();
                items[i].id = itemz[i].id;
                items[i].currentSize = itemz[i].currentSize;
            }
        }

    }

}
