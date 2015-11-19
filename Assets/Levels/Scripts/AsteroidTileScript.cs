using UnityEngine;
using System.Collections;

public class AsteroidTileScript : RandomTileScript {




    //public int numAsteroids;


    public override void init()
    {
        collider.size = new Vector2(width, height);

        float maxWidth = Mathf.Sqrt(width * 1.1f);
        float maxHeight = Mathf.Sqrt(height /2 * 1.1f);
        float maxLenght = width  * .55f ;

        for(int i = 0; i< numAsteroids; i++)
        {
            float randDeg = Random.Range(0, 360f);
            float length = Random.Range(0, maxLenght);
            length = func(length);
            
            //float randY = Random.Range(0, maxHeight) * Mathf.Sign(Random.Range(-1f, 1));
            GameObject go = (GameObject) Instantiate(asteroid);
            go.transform.SetParent(this.transform);
            go.transform.localPosition = new Vector2(length * Mathf.Sin(randDeg * Mathf.Deg2Rad)  , length * Mathf.Cos(randDeg * Mathf.Deg2Rad) ) ;
        }
        
    }
    public static float func(float l)
    {
        l = l + Mathf.Sqrt(l*2);
        return l;
    }

}
