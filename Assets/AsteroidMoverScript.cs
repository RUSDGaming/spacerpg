using UnityEngine;
using System.Collections;

public class AsteroidMoverScript : MonoBehaviour
{


    Rigidbody2D body;
    public Vector2 MoveDirection;
    [SerializeField]    float rotationSpeed = 45;

    BaseShip ship;
    void Awake()
    {
        ship = GetComponent<BaseShip>();
        body = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start()
    {
         body.velocity = MoveDirection;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FixedUpdate()
    {
       // ship.MoveUnit(MoveDirection, false);
        body.MoveRotation(body.rotation + rotationSpeed * Time.fixedDeltaTime);

    }
}
