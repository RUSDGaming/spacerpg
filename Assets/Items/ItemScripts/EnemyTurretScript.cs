using UnityEngine;
using System.Collections;

public class EnemyTurretScript : MonoBehaviour {


    public Transform trackingTarget;
    // Use this for initialization
    void Start()
    {
      
    }

    Vector2 bodyToTarget;
    float angleBetween;

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {

        if(trackingTarget)
        {
        bodyToTarget = trackingTarget.position - transform.position;
        //Debug.Log("transform posti" + transform.up);
        angleBetween = Vector2.Angle(bodyToTarget, (Vector2)transform.up);
        Vector3 cross = Vector3.Cross(bodyToTarget, transform.up);


        if (cross.z > 0)
            angleBetween = -angleBetween;
        transform.eulerAngles = (new Vector3(0, 0, (transform.eulerAngles.z + angleBetween)));
        }
        else
        {
            transform.localEulerAngles= Vector3.zero;
        }


    }
}
