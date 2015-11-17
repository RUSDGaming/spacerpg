using UnityEngine;
using System.Collections;

public class Util : MonoBehaviour {

	
    public static float AngleBetween(Vector3 look, Transform pos)
    {
        Vector3 enenmyToPlayer = (look) - pos.position;
        float angle = Vector2.Angle(enenmyToPlayer, pos.up);
        Vector3 cross = Vector3.Cross(enenmyToPlayer, pos.up);
        if (cross.z > 0)
            angle = -angle;
        return angle;
    }

}
