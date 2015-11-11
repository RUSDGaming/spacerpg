using UnityEngine;
using System.Collections;

public class WarpScript : MonoBehaviour {

    [SerializeField]
    Vector2 maxPos;

    [SerializeField]
    Vector2 minPos;

	// Use this for initialization
	void Start () {
	
	}

    public void init(Vector2 max, Vector2 min)
    {
        this.maxPos = max;
        this.minPos = min;

    }
	
	// Update is called once per frame
	void Update () {
	
        if(transform.position.x > maxPos.x)
        {
            transform.position = new Vector3(minPos.x, transform.position.y);
        }

        if (transform.position.y > maxPos.y)
        {
            transform.position = new Vector3(transform.position.x, minPos.y);
        }


        if (transform.position.x < minPos.x)
        {
            transform.position = new Vector3(maxPos.x, transform.position.y);
        }

        if (transform.position.y < minPos.y)
        {
            transform.position = new Vector3(transform.position.x, maxPos.y);
        }



    }
}
