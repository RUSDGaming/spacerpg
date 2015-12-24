using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShopItemButtonScript : MonoBehaviour {


    [SerializeField]    Image image;
    Item itemPrefab;
    public ShopManagerScript shopManagerScript;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setItem(Item i)
    {
        itemPrefab = i;
        image.sprite = i.GetComponent<SpriteRenderer>().sprite;
    }

    public void SetSelectedItem()
    {
        shopManagerScript.SetSelectItem(itemPrefab);
    }
}
