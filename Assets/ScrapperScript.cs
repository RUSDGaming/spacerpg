using UnityEngine;
using System.Collections;

public class ScrapperScript : MonoBehaviour {

    [SerializeField]    UISlot2 scrapSlot;

    ControlSwitcher switcher;
	// Use this for initialization
	void Start () {
        switcher = GetComponent<ControlSwitcher>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    
    public void ScrapItem()
    {
        Item i = scrapSlot.GetItem();
        if (i)
        {
            switcher.saveGameInfo.money += i.scrapValue * i.currentSize;
            scrapSlot.SetItem(null);
            Destroy(i);
            switcher.reloadShipStats(false);
            // TODO reload Player Stats
        }
    }
}
