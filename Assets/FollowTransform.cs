using UnityEngine;
using System.Collections;

public class FollowTransform : MonoBehaviour {



    public Transform followObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void LateUpdate()
    {
        Vector3 transformPosition = new Vector3( 
          followObject.position.x,
          followObject.position.y,
            this.transform.position.z) ;
        this.transform.position = transformPosition;
    }
}
