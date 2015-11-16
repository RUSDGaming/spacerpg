using UnityEngine;
using System.Collections;

public class ShowThrust : MonoBehaviour {


    [SerializeField]    Transform[] forwardThrusters;
    [SerializeField]    Transform[] aftThrusters;
    [SerializeField]    Transform[] portThrusters; // left
    [SerializeField]    Transform[] starboardThrusters; // right


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void thrust(float horizontal, float vertical)
    {
        if(vertical > 0)
        {
            foreach(Transform t in forwardThrusters)
            {
                t.gameObject.SetActive(true);
            }
        }
        if(vertical <= 0)
        {
            foreach (Transform t in forwardThrusters)
            {
                t.gameObject.SetActive(false);
            }

        }



    }

}
