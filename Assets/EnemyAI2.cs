using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyAI2 : MonoBehaviour
{

    public enum State
    {
        IDLE,WANDER, ATTACK
    }

    public State state = State.IDLE;

    EnemyShip ship;
    Rigidbody2D body;
    float angle;

    Vector3[] patrolPoints;

    public float minDist = 4f;
    public float MaxDist = 8f;
    int idleStartId;

    Vector3 startPos;

    //public Transform trackingObject;
    //public Transform bulletTransform;
    public List<Transform> playerShips = new List<Transform>(4);
    public List<Transform> bullets = new List<Transform>(10);

    public float maxIdleTime = 5f;
    public float maxWanderDist = 35f;


    // Use this for initialization
    void Start()
    {
        ship = GetComponent<EnemyShip>();
        if (ship == null)
        {
            body = GetComponentInParent<Rigidbody2D>();
            ship = GetComponentInParent<EnemyShip>();
        }
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        Think();
    }



    public void Think()
    {
        if (!ship.alive)
        {
            return;
        }

        if(playerShips.Count > 0)
        {
            Transform tracking = playerShips[0];
            if (tracking == null || !tracking.gameObject.activeInHierarchy)
            {
                playerShips.RemoveAt(0);
            }else if (!tracking.gameObject.GetComponent<Collider2D>().bounds.Intersects(gameObject.GetComponent<Collider2D>().bounds) )
            {
                playerShips.RemoveAt(0);
            }
            else
            {
                angle = Util.AngleBetween(tracking.position, transform);
                ship.RotateUnit(angle);
                if (angle <= ship.turnRate)
                {
                    //   Debug.Log("Hate");
                    // ship.TryToFire(0, false);
                    if (Vector2.Distance(ship.transform.position, tracking.transform.position) > minDist)
                    {
                        ship.MoveUnit(Vector2.up, true);
                    }
                }
            }
           

        }
        //&& playerShips.Count > 0
        if (bullets.Count > 0 )
        {
            // should find the closet bullet and move away from that
            if (bullets[0] == null || !bullets[0].gameObject.activeInHierarchy)
            {
                bullets.RemoveAt(0);
            }else if (!bullets[0].gameObject.GetComponent<Collider2D>().bounds.Intersects(gameObject.GetComponent<Collider2D>().bounds))
            {
                bullets.RemoveAt(0);
            }
            else
            {
                Vector2 shipPos = ship.transform.position;
                Vector2 rightPos = shipPos - Util.RotateVector(new Vector2(1,0) , ship.transform.eulerAngles.z);               
                Vector2 leftPos = shipPos - Util.RotateVector(new Vector2(-1, 0), ship.transform.eulerAngles.z);
               

                if (Vector2.Distance(rightPos, bullets[0].position) <
                    Vector2.Distance(leftPos, bullets[0].position))
                {                    
                    ship.MoveUnit(Vector2.right, true);
                }
                else
                {                 
                    ship.MoveUnit(Vector2.left, true);
                }
            }
        }

    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("playerProjectile"))
        {
            bullets.Add(other.transform);
        }
        else if (other.CompareTag("Player"))
        {
            playerShips.Add(other.transform);
           // Debug.Log("player enter");
        }

    }


    void SetTurretTracking(Transform target)
    {
        foreach (WeaponInventory weaponInvetory in ship.weaponSlots)
        {
            EnemyTurretScript ts = weaponInvetory.GetComponent<EnemyTurretScript>();
            if (ts)
            {
                ts.trackingTarget = target;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("playerProjectile"))
        {
            bullets.Remove(other.transform);
        }
        else if (other.CompareTag("Player"))
        {
            playerShips.Remove(other.transform);
          //  Debug.Log("player left");
        }
    }



}
