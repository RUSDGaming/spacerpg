using UnityEngine;
using System.Collections;

public class PlayerDetector : MonoBehaviour {


    
	// Use this for initialization
	void Start () {

        foreach (Transform child in transform)
        {
            //Debug.Log("HHHHIII");
            child.gameObject.SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter2D(Collider2D other)
    {

        //Debug.Log("Thing blah");
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player entering");
            foreach(Transform child in transform)
            {
                //Debug.Log("HHHHIII");
                child.gameObject.SetActive(true);
            }

        }

    }


    void OnTriggerExit2D(Collider2D other) {
        //Debug.Log("Thing blah");
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Player entering");
            foreach (Transform child in transform)
            {
                //Debug.Log("HHHHIII");
                child.gameObject.SetActive(false);
            }

        }
    }

    

}
