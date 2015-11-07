using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour
{


    //Transform trackingTarget;
    [SerializeField] Vector3 trackingPosition;

    [SerializeField]
    float maxRotation = 90f;
    [SerializeField]
    float turnRate = 10f;

    // Use this for initialization
    void Start()
    {
        
    }

    Vector2 bodyToTarget;
    [SerializeField]
    float angleBetween;

    // Update is called once per frame
    void Update()
    {
        
        trackingPosition  =  Camera.main.ScreenToWorldPoint( Input.mousePosition);
    }

    void FixedUpdate()
    {

        
            bodyToTarget = trackingPosition - transform.position;
            angleBetween = Vector2.Angle(bodyToTarget, transform.up);
            Vector3 cross = Vector3.Cross(bodyToTarget, transform.up);
            //Debug.Log("transform.up" + transform.up);

            if (cross.z > 0)
            {
                angleBetween = -angleBetween;

            }



            #region fancy less that 180 stuff
            if (maxRotation < 180)
            {
                float rotation = transform.localEulerAngles.z + angleBetween;
                //Debug.Log(turnRate * Time.fixedDeltaTime);
                angleBetween = Mathf.Abs(angleBetween);
                if (angleBetween > turnRate * Time.fixedDeltaTime)
                {
                    angleBetween = turnRate * Time.fixedDeltaTime;
                }

                if (rotation > 180)
                {
                    rotation = rotation - 360;
                }
                else if (rotation < -180)
                {
                    rotation = rotation + 360;
                }
                //Debug.Log(rotation.ToString("0"));

                if (Mathf.Abs(rotation) > maxRotation)
                    return;

                float currentRotation = transform.localEulerAngles.z;

                if (currentRotation > 180)
                {
                    currentRotation = currentRotation - 360;
                }
                else if (currentRotation < -180)
                {
                    currentRotation = currentRotation + 360;
                }

                if (currentRotation > rotation)
                {
                    // subtract
                    transform.localEulerAngles = (new Vector3(0, 0, transform.localEulerAngles.z - angleBetween));
                }
                else
                {
                    // add
                    transform.eulerAngles = (new Vector3(0, 0, transform.eulerAngles.z + angleBetween));
                }
                return;
            }
            #endregion
            //if the rotation is above 180 take the shortest path...

            if (Mathf.Abs(angleBetween) > turnRate * Time.fixedDeltaTime)
            {
                //angleBetween = Mathf.Abs(turnRate * Time.fixedDeltaTime * Mathf.Sign(angleBetween));
                transform.eulerAngles = (new Vector3(0, 0, transform.eulerAngles.z + (turnRate * Mathf.Sign(angleBetween)) * Time.fixedDeltaTime));
            }
            else
            {
                transform.eulerAngles = (new Vector3(0, 0, transform.eulerAngles.z + angleBetween));
            }




        
        


    }
}
