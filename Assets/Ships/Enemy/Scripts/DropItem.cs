using UnityEngine;
using System.Collections;

public class DropItem : MonoBehaviour
{


    public int[] items;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnDestroy()
    {
    }

    public void SpawnItem()
    {

        int i = Random.Range(0, items.Length);

        Item item = ItemManager.GetNewItem(items[i]);

        item.transform.position = this.transform.position;
        ItemJuice juice = item.GetComponent<ItemJuice>();
        if(juice)
        juice.enabled = true;

    }
}
