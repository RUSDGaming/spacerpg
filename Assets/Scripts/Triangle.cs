using UnityEngine;
using System.Collections;


//http://stackoverflow.com/questions/13300904/determine-whether-point-lies-inside-triangle
public class Triangle  {

    public Vector2 p1;
    public Vector2 p2;
    public Vector2 p3;


    public Triangle() { }

    // no idea what is going on... 
    public bool isPointInside(Vector2 p)
    {

        float alpha = ((p2.y - p3.y) * (p.x - p3.x) + (p3.x - p2.x) * (p.y - p3.y)) /
        ((p2.y - p3.y) * (p1.x - p3.x) + (p3.x - p2.x) * (p1.y - p3.y));
         if(alpha < 0 || alpha > 1)        
          return false;
        float beta = ((p3.y - p1.y) * (p.x - p3.x) + (p1.x - p3.x) * (p.y - p3.y)) /
               ((p2.y - p3.y) * (p1.x - p3.x) + (p3.x - p2.x) * (p1.y - p3.y));
        if (beta < 0 || beta > 1)
            return false;
        float gamma = 1.0f - alpha - beta;
        if (alpha + beta <= 1)
           return true;

        return false;
    }

}
