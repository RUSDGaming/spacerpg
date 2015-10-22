using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class PlayerMenu : MonoBehaviour {

    public GameObject ShipLayout;
    public GameObject Stats;
    public GameObject Inventory;
    public PlayerController playerController;

    bool menuOpen = false;

    void Start()
    {
        ShipLayout.SetActive(false);
        Stats.SetActive(false);
        Inventory.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOpen)
            {
                playerController.disableInput = false;
                ShipLayout.SetActive(false);
                Inventory.SetActive(false);
                menuOpen = !menuOpen;
            }
            else
            {
                playerController.disableInput = true;
                ShipLayout.SetActive(true);
                Inventory.SetActive(true);
                menuOpen = !menuOpen;
            }
        }
    }


    

}
