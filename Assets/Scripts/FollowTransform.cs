using UnityEngine;
using System.Collections;

public class FollowTransform : MonoBehaviour {



    public Transform followObject;
    [SerializeField]
    Transform backGround;

    [SerializeField]
    float paralaxRatio = .9f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(backGround)
        backGround.transform.position = followObject.transform.position * paralaxRatio;
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
