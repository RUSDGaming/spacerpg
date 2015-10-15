using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


    private float horizontal;
    private float vertical;
    public float speed;

    private Rigidbody2D body;
	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        horizontal = Input.GetAxisRaw("Horizantal");
        vertical = Input.GetAxisRaw("Vertical");

	}

    void FixedUpdate()
    {
        body.AddForce(new Vector2(horizontal, vertical));
    }

}
