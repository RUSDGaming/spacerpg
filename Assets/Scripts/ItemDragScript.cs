using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ItemDragScript : MonoBehaviour , IBeginDragHandler, IDragHandler,IEndDragHandler{


    public  GameObject selecetdItem;
    public GameObject realGamePrefab;

    Vector3 startPosition;
    Transform startParent;
    //bool isOnShipSlot = false;
    ShipSlot shipSlot;
    public int inventoryIndex;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
       
        selecetdItem = gameObject;
        startPosition = this.transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        shipSlot = transform.parent.GetComponent<ShipSlot>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = Input.mousePosition;
         pos.Set( pos.x,pos.y,9f);
        transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //selecetdItem = null;

        var ray = eventData.pointerCurrentRaycast;
        //var ray = Input.mous;
        //var ray =  Camera.main.ScreenPointToRay(Input.mousePosition);
        
       // Debug.Log(ray.ToString());
        // this is when you drag over empty screen
        if(ray.gameObject != null)
        {


        Slot slot =  ray.gameObject.transform.parent.gameObject.GetComponent<Slot>();
        if(slot != null)
        {
                if (slot.GetComponent<ShipSlot>())
                {
                    //Debug.Log("item ended over ship slot");
                    if (!selecetdItem.GetComponent<ItemDragScript>().realGamePrefab)
                    {
                        slot.SetItem(selecetdItem.GetComponent<ItemDragScript>());
                    }else if(selecetdItem.GetComponent<ItemDragScript>().realGamePrefab.GetComponent<Weapon>())
                    {
                        slot.SetItem(selecetdItem.GetComponent<ItemDragScript>());
                    }
                }else if (shipSlot)
                {
                    if(!slot.transform.GetChild(0).GetComponent<ItemDragScript>().realGamePrefab){
                        slot.SetItem(selecetdItem.GetComponent<ItemDragScript>());
                    }else if(slot.transform.GetChild(0).GetComponent<ItemDragScript>().realGamePrefab.GetComponent<Weapon>()) {
                        slot.SetItem(selecetdItem.GetComponent<ItemDragScript>());
                    }
                }
                else
                {
                    slot.SetItem(selecetdItem.GetComponent<ItemDragScript>());

                }


        }
        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (transform.parent == startParent)
        {
        transform.position = startPosition;

        }
        else
        {
            if (shipSlot)
            {
                shipSlot.removeItem();
            }
            
        }
        
    }

  
}
