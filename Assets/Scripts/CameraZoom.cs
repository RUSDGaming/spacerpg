using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CheckInput();
	}

    void CheckInput()
    {
        float mouseScroll = Input.mouseScrollDelta.y;
        //Debug.Log(mouseScroll);
        if(mouseScroll !=0 )
        Camera.main.orthographicSize -= Mathf.Sign(mouseScroll);        
        Camera.main.orthographicSize =    Mathf.Clamp(Camera.main.orthographicSize, 1, 100);
         
    }
}
