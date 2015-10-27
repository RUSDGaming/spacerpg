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
        if (shipSlot)
        {           
            
            Debug.Log("Dragged an item off a ship slot");
            // shipSlot.removeItem();
        }

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
        
        //Debug.Log(ray.ToString());
        
        Slot slot =  ray.gameObject.transform.parent.gameObject.GetComponent<Slot>();
        if(slot != null)
        {
            slot.SetItem(selecetdItem.GetComponent<ItemDragScript>());
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
