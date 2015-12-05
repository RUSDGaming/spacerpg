﻿using UnityEngine;
using System.Collections;

public class ItemJuice : MonoBehaviour {

    [SerializeField]
    float rotationSpeed = 90;
    [SerializeField]
    float vertical = 0;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y +  Mathf.Sin(Time.time) * vertical, transform.position.z);
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        
    }
}
