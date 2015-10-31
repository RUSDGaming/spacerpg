using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ItemDragScript : MonoBehaviour , IBeginDragHandler, IDragHandler,IEndDragHandler{


    //public  GameObject selecetdItem;
    public GameObject realGamePrefab;

    Vector3 startPosition;
    Transform startParent;
    ShipSlot shipSlot;
    public int inventoryIndex;
    
    public void OnBeginDrag(PointerEventData eventData)
    {       
        //selecetdItem = gameObject;
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
        var ray = eventData.pointerCurrentRaycast;
       
        // this is when you drag over empty screen
        if(ray.gameObject != null)
        {
            UISlot slot =  ray.gameObject.transform.parent.gameObject.GetComponent<UISlot>();

            if(slot != null)            
                slot.SetItem(this);           
        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (transform.parent == startParent)
            transform.position = startPosition;                
    }
}
