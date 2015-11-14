using UnityEngine;
using System.Collections;

public class MapEdgeScript : MonoBehaviour
{


    [SerializeField]    Camera cam;
    [SerializeField]    Transform plane;

    Vector2 startPos;
    Transform trackingTarget;
    bool horizontal;
    float max;
    float min;

    public void Start()
    {
        

    }

    public void initCorner(Vector3 camPos, Vector3 planePos)
    {
        this.plane.position = planePos;
        this.cam.transform.position = camPos;

    }


    public void init(Transform tracking, Vector2 camPos,Vector2 planePos, bool horizontal, float max, float min)
    {
        this.trackingTarget = tracking;
        cam.transform.position = camPos;
        plane.position = planePos;
        this.min = min;
        this.max = max;
        this.horizontal = horizontal;

        
    }

    public void Update()
    {

        if (!trackingTarget)
            return;
        // just move vectically or horizontally  tracking teh player in 1 spot.
        if (horizontal )
        {
            cam.transform.position = new Vector3(trackingTarget.position.x, cam.transform.position.y,-1);
            plane.position = new Vector3(trackingTarget.position.x, plane.position.y);
        }
        else
        {
           cam.transform.position = new Vector3(cam.transform.position.x,   trackingTarget.position.y, -1);
           plane.position = new Vector3(plane.position.x, trackingTarget.position.y);
        }



    }





 
}
