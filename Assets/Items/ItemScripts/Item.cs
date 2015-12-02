using UnityEngine;
using System;

[RequireComponent (typeof(SpriteRenderer))]
public class Item : MonoBehaviour
{

    public enum ItemType
    {
        NULL,BASIC,WEAPON,UTILITY,DEFENSE
    }
    public ItemType itemType;
    public int id = -1;

    public int stackSize = 1;
    public int currentSize = 1;
    public int scrapValue = 1;
    public int buildCost = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory inv =  other.GetComponent<Inventory>();
            if(inv.ItemSits(this))
                gameObject.SetActive(false);
        }
    }
}
