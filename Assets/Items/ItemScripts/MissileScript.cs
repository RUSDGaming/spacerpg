using UnityEngine;
using System.Collections;

public class MissileScript : ProjectileScript
{

    [SerializeField]
    float moveForce;
    [SerializeField]
    float maxSpeed;

    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        init();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FixedUpdate()
    {
        body.AddRelativeForce(Vector2.up * moveForce * Time.fixedDeltaTime);


        if (body.velocity.magnitude > maxSpeed)
        {
            body.velocity = body.velocity.normalized * maxSpeed;
        }
    }

    protected override void init()
    {
        StartCoroutine(Destroy(10));

    }
}
