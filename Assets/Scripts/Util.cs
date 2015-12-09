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


    public static Vector2 RotateVector(Vector2 v, float deg)
    {
        deg *= Mathf.Deg2Rad;
        float sin = Mathf.Sin(deg);
        float cos = Mathf.Cos(deg);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (cos * ty) + (sin * tx);
        return v;
    }

}
