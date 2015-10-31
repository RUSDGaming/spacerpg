using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {

    Vector3 mousePosition;
	// Use this for initialization
	void Start () {
	
	}
   
    Vector2 bodyToMouse;    
    float angleBetween;
	
	// Update is called once per frame
	void Update () {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        bodyToMouse = mousePosition - transform.position;
        Debug.Log("transform posti" + transform.up);
        angleBetween = Vector2.Angle(bodyToMouse, (Vector2)transform.up);
        Vector3 cross = Vector3.Cross(bodyToMouse, transform.up);


        if (cross.z > 0)
            angleBetween = -angleBetween;
           transform.eulerAngles =(new Vector3(0,0,  ( transform.eulerAngles.z +  angleBetween)));

        
    }
}
