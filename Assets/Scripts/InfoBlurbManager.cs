﻿using UnityEngine;
using System.Collections;

public class InfoBlurbManager : MonoBehaviour {

    [SerializeField]GameObject infoBlurb;
    static InfoBlurbManager instance;

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    

    public static void CreateInfoBlurb(Vector3 pos, string msg, Color color)
    {
        if (instance)
        {
        InfoBlurb blurb =  ((GameObject) Instantiate(instance.infoBlurb, pos, Quaternion.identity)).GetComponent<InfoBlurb>() ;
        blurb.init(pos, msg, color);
        }
        else
        {
            Debug.LogError("There aint no infoBlurb manarger Kappa KApp Kappa");
        }

    }

}
