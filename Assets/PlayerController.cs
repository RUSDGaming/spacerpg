using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


    private float horizontal;
    private float vertical;
    public float maxSpeed;
    public float moveForce;
    public float rotationSpeed;
    public bool relativeInput;
    public bool disableInput;
    

    private Vector3 mousePosition ;
    private Rigidbody2D body;

    public Vector2 bodyToMouse;
    public float angleBetween;
    public bool fireDown;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}

    public Ship ship;

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
        if (relativeInput)
        {
        body.AddRelativeForce(new Vector2(horizontal, vertical) * Time.fixedDeltaTime * moveForce);
        }
        else
        {
            body.AddForce(new Vector2(horizontal, vertical) * Time.fixedDeltaTime * moveForce);
        }
              
        body.MoveRotation(body.rotation + angleBetween);
        
        // limit them move ment to the max speed;
        if(body.velocity.magnitude > maxSpeed)
        {
            body.velocity = body.velocity.normalized * maxSpeed;
        }

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
