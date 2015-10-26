using UnityEngine;
using System.Collections;
using Game.Interfaces;

public class ControlSwitcher : MonoBehaviour {

    [SerializeField]
    GameObject shipCore;

    [SerializeField]
    GameObject mainShip;

    [SerializeField]
    FollowTransform cameraFollower;

    [SerializeField]
    PlayerMenu menu;

    

	// Use this for initialization
	void Start () {
        if (shipCore.activeInHierarchy)
        {
            menu.SetShip(shipCore);
            menu.playerController = shipCore.GetComponent<PlayerController>();
        }
        if (mainShip.activeInHierarchy)
        {
            menu.SetShip(mainShip);
            menu.playerController = mainShip.GetComponent<PlayerController>();
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SwitchToShipCore()
    {
        shipCore.SetActive(true);
        shipCore.transform.position = mainShip.transform.position;
        mainShip.SetActive(false);
        cameraFollower.followObject = shipCore.transform;
        menu.SetShip(shipCore);
        menu.playerController = shipCore.GetComponent<PlayerController>();

    }

    public void SwitchToMainShip()
    {
        mainShip.SetActive(true);
        mainShip.transform.position = shipCore.transform.position;
        shipCore.SetActive(false);
        cameraFollower.followObject = mainShip.transform;
        menu.playerController = mainShip.GetComponent<PlayerController>();
        menu.SetShip(mainShip);
    }

}
