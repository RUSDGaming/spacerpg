using UnityEngine;
using System.Collections;

public class AsteroidTileScript : RandomTileScript {


    [SerializeField]    GameObject asteroid;

    public int numAsteroids;


    public override void init()
    {
        for(int i = 0; i< numAsteroids; i++)
        {
            float randX = Random.Range(0, width);
            float randY = Random.Range(0, height);
            GameObject go = (GameObject) Instantiate(asteroid);
            go.transform.SetParent(this.transform);
            go.transform.localPosition = new Vector2(randX, randY);
        }
        
    }

}
