using UnityEngine;
using System.Collections;

public class QuitGameScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.F4))
        {
            Application.Quit();

        }
	}
}
