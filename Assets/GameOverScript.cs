using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Application.LoadLevel("FunLevel");
        }
	}

    public void LoadMainMenu()
    {
        Application.LoadLevel("MainMenu");
        
    }

}
