﻿using UnityEngine;
using System.Collections;
using Game.Interfaces;


public class PlayerController : MonoBehaviour {


    private float horizontal;
    private float vertical;
    //public float maxSpeed;
    //public float moveForce;
   //public float rotationSpeed;
    public bool relativeInput = true;
    public bool disableInput;
    // need to move this 
    public int id;
    private Vector3 mousePosition ;
    private Rigidbody2D body;

    Vector2 bodyToMouse;
    float angleBetween;
    bool fireDown;
    bool mouseUp;
    public PlayerShip ship;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        ship = GetComponent<PlayerShip>();
	}


	// Update is called once per frame
	void Update () {


        if (!disableInput)
        {
            CheckInput();
        }
	}

    void FixedUpdate()
    {
      
        bodyToMouse =   (Vector2)mousePosition - body.position ;      
        angleBetween = Vector2.Angle(bodyToMouse,(Vector2) transform.up);
        Vector3 cross = Vector3.Cross(bodyToMouse, transform.up);
        
        if (cross.z > 0)
            angleBetween = -angleBetween;      
         
        ship.MoveUnit(new Vector2(horizontal, vertical),relativeInput);
       
        ship.RotateUnit(angleBetween);
        
        if (fireDown)
        {           
            ship.TryToFire(0,true);
            fireDown = false;
        }
        if (mouseUp)
        {
            ship.MouseUp();
            mouseUp = false;
        }
        

    }
    void CheckInput()
    {


        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if ( Input.GetMouseButton(0))
        {
        fireDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseUp = true;
        }
    }

}
