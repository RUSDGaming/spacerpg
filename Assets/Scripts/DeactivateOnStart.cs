using UnityEngine;
using System.Collections;

public class DeactivateOnStart : MonoBehaviour {


    public bool active;
	// Use this for initialization
	void Start () {
        gameObject.SetActive(active);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
