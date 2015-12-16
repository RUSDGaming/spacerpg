using UnityEngine;
using System.Collections;

public class MissileTracking : MonoBehaviour {

    Transform trackingTarget;
    [SerializeField]
    float maxTurnRate = 100f;    
    
    [SerializeField]
    Rigidbody2D body;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        
	
	}
    void FixedUpdate()
    {
        if (trackingTarget)
        {
            Vector3 missileToTracking = (trackingTarget.position) - transform.position;
            float angle = Vector2.Angle(missileToTracking, transform.up);
            Vector3 cross = Vector3.Cross(missileToTracking, transform.up);
            if (cross.z > 0)
                angle = -angle;

            if (Mathf.Abs(angle) > maxTurnRate * Time.fixedDeltaTime)
            {
                body.MoveRotation(body.rotation + (maxTurnRate * Mathf.Sign(angle)) * Time.fixedDeltaTime);
            }
            else
            {
                body.MoveRotation(body.rotation + angle);
            }

           
        }

    }

    

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            trackingTarget = other.transform;
            

        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
                if(trackingTarget != null)
                if( other.transform == trackingTarget.transform)
                {
                    trackingTarget = null;
                }
           
            


        }
    }
}
