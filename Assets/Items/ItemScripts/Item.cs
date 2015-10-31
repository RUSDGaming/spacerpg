using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour
{

    public int stackSize;
    public int currentSize;


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
            if(inv.StoreItem(this))
                gameObject.SetActive(false);
        }
    }
}
