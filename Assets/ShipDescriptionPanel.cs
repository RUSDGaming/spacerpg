using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ShipDescriptionPanel : MonoBehaviour {

    [SerializeField]    Image shipImage;
    [SerializeField]    Text shipTypeTile;
    [SerializeField]    Text shipDescription;
    [SerializeField]    Text classBonuses;




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
                break;
            case PriortySelectorScript.ClassType.UTILITY:
                shipTypeTile.text = "Utility Class";
                break;
            case PriortySelectorScript.ClassType.DEFFENSE:
                shipTypeTile.text = "Deffense Class";
                break;
            case PriortySelectorScript.ClassType.EMPTY:
                shipTypeTile.text = "Balanced Class";
                break;
            default:
                break;
        }

    }
}
