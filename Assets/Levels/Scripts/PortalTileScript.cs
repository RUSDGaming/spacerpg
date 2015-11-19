using UnityEngine;
using System.Collections;

public class PortalTileScript : RandomTileScript {

    [SerializeField]    GameObject portal;
  //  [SerializeField]    GameObject asteroid;

    //public int numAsteroids = 10;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public override void init()
    {

        GameObject go = Instantiate(portal);
        go.transform.SetParent(this.transform);
        go.transform.localPosition = new Vector2(0, 0);


        float maxWidth = Mathf.Sqrt(width * 1.1f);
        float maxHeight = Mathf.Sqrt(height / 2 * 1.1f);
        float maxLenght = width * .55f;

        for (int i = 0; i < numAsteroids; i++)
        {
            float randDeg = Random.Range(0, 360f);
            float length = Random.Range(8, maxLenght);
            length = AsteroidTileScript.func(length);

            //float randY = Random.Range(0, maxHeight) * Mathf.Sign(Random.Range(-1f, 1));
            GameObject gameObject = (GameObject)Instantiate(asteroid);
            gameObject.transform.SetParent(this.transform);
            gameObject.transform.localPosition = new Vector2(length * Mathf.Sin(randDeg * Mathf.Deg2Rad), length * Mathf.Cos(randDeg * Mathf.Deg2Rad));
        }
    }
}
