using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


[RequireComponent (typeof(Image))]
[RequireComponent(typeof(CanvasGroup))]

public class UISlot2 : MonoBehaviour, IDragHandler,IBeginDragHandler, IEndDragHandler {


    Image image;
    Sprite startSprite;
    [SerializeField]    Item item;
    [SerializeField]    GameObject counterPanel;
    [SerializeField]    Text counterText;

    int inventoryIndex;
    public Inventory inventory;
    bool inited = false;
    
    Vector3 startPosition;

	// Use this for initialization
	void Start () {


        init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void init() {
        if (inited)
            return;
        image = GetComponent<Image>();
        startSprite = image.sprite;
        inited = true;

    }


    public void SetUpItem(Item i, int index)
    {
        init();

        item = i;
        inventoryIndex = index;
        inventory.SetItemWithIndex(i, index);        

        

        if (item)
        {
            image.sprite = item.gameObject.GetComponent<SpriteRenderer>().sprite;
            if(item.currentSize > 1)
            {
                counterPanel.SetActive(true);
                counterText.text = item.currentSize.ToString();
            }
            else
            {
                if (counterPanel)
                    counterPanel.SetActive(false);

            }
        }
        else
        {
            if(counterPanel)
            counterPanel.SetActive(false);
            image.sprite = startSprite;
        }
    }

    public void SetItem(Item i )
    {
        SetUpItem(i, inventoryIndex);
    }


    public Item GetItem()
    {
        return item;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        // set the position back to what it should be
        // 
        transform.position = startPosition;
        GetComponent<CanvasGroup>().blocksRaycasts = true;




        var ray = eventData.pointerCurrentRaycast;

        //Debug.Log("Ended over :" + ray.gameObject);
        // this is when you drag over empty screen
        if (ray.gameObject != null)
        {
            UISlot2  slot = ray.gameObject.GetComponent<UISlot2>();

            if (slot != null)
            {
                Item temp = item; // save this item
                if(slot.inventory.ItemFits(item) && inventory.ItemFits(slot.GetItem()))
                {
                 
                    SetItem(slot.GetItem()); /// set me to other item
                    slot.SetItem(temp);
                    
                }

    
               // Debug.Log("ended over another slot");

            }

        }else 
        {
            Vector3 dropPos = Camera.main.ScreenToWorldPoint(eventData.position);
            dropPos.z = 0;
            item.transform.parent = null;
            item.gameObject.transform.position = dropPos;
            item.gameObject.SetActive(true);

            Collider2D col = item.GetComponent<Collider2D>();
            if (col)
                col.enabled = true;

            ItemJuice juice = item.GetComponent<ItemJuice>();
            if (juice)
                juice.enabled = true;

            SetItem(null);
            //item = null;
            //image.sprite = startSprite;
        }

       

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        // idk
        //throw new NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        // move the thing
       // throw new NotImplementedException();
    }
}
