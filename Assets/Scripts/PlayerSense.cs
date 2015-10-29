using UnityEngine;
using System.Collections;

public class PlayerSense : MonoBehaviour {

    Vector3 playerPosition;
    public Transform parentTransform;
    public Rigidbody2D body;
    
    [SerializeField] EnenyScript enemyScript;
    public float maxTurnRate = 10f;


    bool playerSensed;

    float angle;


	// Use this for initialization
	void Start () {
        

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        if (playerSensed)
        {
            MoveEnenmy();
        }

      



    }

    void MoveEnenmy()
    {

        enemyScript.TryToFire();
        Vector3 enenmyToPlayer = (Vector2)(playerPosition) - body.position;
        angle = Vector2.Angle(enenmyToPlayer, transform.up);
        Vector3 cross = Vector3.Cross(enenmyToPlayer, transform.up);
        if (cross.z > 0)
            angle = -angle;

        if (Mathf.Abs(angle) > maxTurnRate * Time.fixedDeltaTime)
        {
            body.MoveRotation(body.rotation + (maxTurnRate * Mathf.Sign(angle)) * Time.fixedDeltaTime);
        }
        else
        {
            body.MoveRotation(body.rotation + angle);
        }
        if (angle < maxTurnRate)
        {
            body.AddRelativeForce(Vector2.up * Time.fixedDeltaTime * enemyScript.moveForce);
        }

        if (body.velocity.magnitude > enemyScript.maxSpeed)
        {
            body.velocity = body.velocity.normalized * enemyScript.maxSpeed;
        }



    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerSensed = true;
            playerPosition = other.transform.position;

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerSensed = false;
        }
    }




}
