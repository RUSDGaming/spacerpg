using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShipDescriptionPanel : MonoBehaviour {

    [SerializeField]    Image shipImage;
    [SerializeField]    Text shipTypeTile;
    [SerializeField]    Text shipDescription;
    [SerializeField]    Text classBonuses;


    [SerializeField]    GameObject attackShip;
    [SerializeField]    GameObject deffenseShip;
    [SerializeField]    GameObject utilityShip;
    [SerializeField]    GameObject balancedShip;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void SetClass( PriortySelectorScript.ClassType c)
    {
        switch (c)
        {
            case PriortySelectorScript.ClassType.ATTACK:
                shipTypeTile.text = "Attack Class";
                shipImage.sprite = attackShip.GetComponent<SpriteRenderer>().sprite;
                break;
            case PriortySelectorScript.ClassType.UTILITY:
                shipTypeTile.text = "Utility Class";
                shipImage.sprite = utilityShip.GetComponent<SpriteRenderer>().sprite;
                break;
            case PriortySelectorScript.ClassType.DEFFENSE:
                shipTypeTile.text = "Deffense Class";
                shipImage.sprite = deffenseShip.GetComponent<SpriteRenderer>().sprite;
                break;
            case PriortySelectorScript.ClassType.EMPTY:
                shipTypeTile.text = "Balanced Class";
                shipImage.sprite = balancedShip.GetComponent<SpriteRenderer>().sprite;
                break;
            default:
                break;
        }

    }
}
