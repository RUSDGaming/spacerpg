using UnityEngine;
using System.Collections;

public class InfoBlurbManager : MonoBehaviour {

    [SerializeField]GameObject infoBlurb;
    static InfoBlurbManager instance;

	// Use this for initialization
	void Start () {
        
	}

    void Awake() {
        if (!instance)
        {
            Debug.Log("Setting up info blurb manager Instance");
            instance = this;
        }
    }

	
	// Update is called once per frame
	void Update () {
	
	}
    

    public static void CreateInfoBlurb(Vector3 pos, string msg, Color color)
    {
        if (instance)
        {
            Debug.Log("Creating info thisg");
        InfoBlurb blurb =  ((GameObject) Instantiate(instance.infoBlurb, pos, Quaternion.identity)).GetComponent<InfoBlurb>() ;
        blurb.init(pos, msg, color);
            blurb.transform.SetParent(instance.transform);
        }
        else
        {
            Debug.LogError("There aint no infoBlurb manager Kappa KApp Kappa");
        }

    }

}
