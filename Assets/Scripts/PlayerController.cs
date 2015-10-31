using UnityEngine;
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
    public iShip ship;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
        ship = GetComponent<iShip>();
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
        //if (relativeInput)
        //{
        //body.AddRelativeForce(new Vector2(horizontal, vertical) * Time.fixedDeltaTime * moveForce);
        //}
        //else
        //{
        //    body.AddForce(new Vector2(horizontal, vertical) * Time.fixedDeltaTime * moveForce);
        //}
              
        //body.MoveRotation(body.rotation + angleBetween);
        ship.RotateUnit(angleBetween);
        // limit them move ment to the max speed;
        //if(body.velocity.magnitude > maxSpeed)
        //{
        //    body.velocity = body.velocity.normalized * maxSpeed;
        //}

        if (fireDown)
        {
            //Debug.Log("Player Controller caalling ship to fire");
            ship.TryToFire(0);
            fireDown = false;
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
    }

}
