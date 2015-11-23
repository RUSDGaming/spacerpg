using UnityEngine;
using System.Collections;

public class PlayerClass : MonoBehaviour {



    public PriortySelectorScript.ClassType primaryType;

    public PriortySelectorScript.ClassType p1;
    public PriortySelectorScript.ClassType p2;
    public PriortySelectorScript.ClassType p3;

    public bool a1 = false;
    public bool a2 = false;
    public bool a3 = false;
    public bool a4 = false;
    public bool a5 = false;
    public bool a6 = false;

    public bool u1 = false;
    public bool u2 = false;
    public bool u3 = false;
    public bool u4 = false;
    public bool u5 = false;
    public bool u6 = false;

    public bool d1 = false;
    public bool d2 = false;
    public bool d3 = false;
    public bool d4 = false;
    public bool d5 = false;
    public bool d6 = false;




    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public override string ToString()
    {
        string msg = "";


        msg += "primary Type: " + primaryType + "\n";
        msg += "priority 1: " + p1 + "\n";
        msg += "priority 2: " + p2 + "\n";
        msg += "priority 3: " + p3 + "\n";
        return msg;

    }
}
