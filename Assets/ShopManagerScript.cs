using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShopManagerScript : MonoBehaviour {

    Item selectedItem;
    public Text itemNameText;
    public Text itemCostText;
    public Transform buttonHolder;
    public Image itemImage;

    public ControlSwitcher switcher;

    //public Button buyButton;
    [SerializeField] ShopItemButtonScript ItemHolderPrefab;

	// Use this for initialization
	void Start () {
        SetUpButtons();
	}

    void Awake()
    {
        switcher = GetComponentInParent<ControlSwitcher>();
        if (switcher == null)
        {
            Debug.LogError("Could not Find Switcher");
        }
    }


    void SetUpButtons() {
        foreach (Item i in ItemManager.instance.items) {
            ShopItemButtonScript shopButton = Instantiate(ItemHolderPrefab);
            shopButton.setItem( i);
            shopButton.shopManagerScript = this;
            shopButton.transform.SetParent(buttonHolder);
        }

    }

	// Update is called once per frame
	void Update () {
	
	}

    public void BuyItem()
    {
        Debug.Log(selectedItem);
        if(switcher.saveGameInfo.money >= selectedItem.buildCost)
        {
            switcher.saveGameInfo.money -= selectedItem.buildCost;
            Item item = Instantiate(selectedItem);
            item.transform.position = switcher.mainShip.transform.position;
            item.gameObject.SetActive(true);

            Collider2D col = item.GetComponent<Collider2D>();
            if (col)
                col.enabled = true;

        }
    }
    public void SetSelectItem(Item i)
    {
        selectedItem = i;
        itemNameText.text = i.name;
        itemCostText.text = i.buildCost.ToString();
        itemImage.sprite = i.GetComponent<SpriteRenderer>().sprite;

    }

}
