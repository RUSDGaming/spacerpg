using UnityEngine;
using System.Collections;

using Game.Interfaces;
public class EnemyAI : MonoBehaviour
{

    public enum State
    {
        IDLE, START_IDLE,WANDER, PATROL , TURN, FACE_PLAYER, CHASE_PLAYER, ATTACK
    }

    public State state = State.START_IDLE;

    EnemyShip ship;
    Rigidbody2D body;
    float angle;

    Vector3[] patrolPoints;

    public float minDist = 4f;
    public float MaxDist = 8f;
    int idleStartId;

    Vector3 startPos;

    public Transform trackingObject;
    public Transform bulletTransform;
    public float maxIdleTime = 5f;
    public float maxWanderDist = 35f;

    // Use this for initialization
    void Start()
    {

        patrolPoints = new Vector3[2];
        ship = GetComponent<EnemyShip>();
        if (ship == null)
        {
        body = GetComponentInParent<Rigidbody2D>();
            ship = GetComponentInParent<EnemyShip>();
        }
        startPos = transform.position;
    }

    public void Init(Vector2 start)
    {
        Init(start, null);
    }
    public void Init(Vector2 start, params Vector3[] patrolPoints)
    {
        startPos = start;
        this.patrolPoints = patrolPoints;

    }


    void FixedUpdate()
    {       
        
        StateMachine();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    public void OnTriggerStay2D(Collider2D other)
    {

        if(!bulletTransform)
        if (other.gameObject.layer == LayerMask.NameToLayer("playerProjectile")){
                bulletTransform = other.transform;
        }

        if (!trackingObject)
        {

            if (other.CompareTag("Player"))
            {
               // Debug.Log("Player entered radar range");
                trackingObject = other.transform;

                SetTurretTracking(other.transform);
            }
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
        // when player leaves set the tracking object to null.
        if(other.transform == trackingObject)
        {
            trackingObject = null;
            SetTurretTracking(null);
            return;
        }

        if (other.transform == bulletTransform)
        {
            bulletTransform = null;
        }
        //state = State.IDLE;
    }

    IEnumerator Idle()
    {
        //Debug.Log("start Idle");
        state = State.IDLE;
        // prevents a past idle start from startking the wander before its time. 
        int id = ++idleStartId;
        yield return new WaitForSeconds(maxIdleTime);

        if(state == State.IDLE && id == idleStartId)
        {
           // Debug.Log("starting wander from idle");
            state = State.WANDER;
            StartCoroutine(Wander());

        }
    }

    IEnumerator Wander()
    {
        float deg;
        if(Vector2.Distance(transform.position, startPos )> maxWanderDist){
            deg = Util.AngleBetween(startPos, transform);
        }
        else
        {
            deg = Random.Range(-180, 180);
        }
        float degTurned = 0;
        //Debug.Log("Turn deg is " + deg);
        while (degTurned < Mathf.Abs(deg)  && state == State.WANDER)
        {
            // Debug.Log(deg);
            float turnDeg = ship.turnRate * Mathf.Sign(deg) * Time.fixedDeltaTime / 4;
            StartCoroutine(Turn(turnDeg));
            degTurned += turnDeg * Mathf.Sign(deg);
            
            yield return new WaitForFixedUpdate();
        }
        if(state != State.WANDER)
        {
            yield break;
        }

        float moveTime = Random.Range(.5f, 1f);
        //moveTime = 5f;
        while(moveTime > 0 && state == State.WANDER)
        {
            StartCoroutine(move(new Vector2(0,.5f)));
            moveTime -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        if(state == State.WANDER)
        {
            state = State.START_IDLE;
        }

        // rotate
        // move

        //yield return new WaitForSeconds(.2f);

    }

    // this turns the ship Kapprer chino 
    IEnumerator Turn(float deg)
    {
        if(state == State.ATTACK)
        {
            //Vector3 enenmyToPlayer = (trackingObject.position) - transform.position;
            //    angle = Vector2.Angle(enenmyToPlayer, transform.up);
            //Vector3 cross = Vector3.Cross(enenmyToPlayer, transform.up);
            //if (cross.z > 0)
            //    angle = -angle;
            angle = Util.AngleBetween(trackingObject.position, transform);
            ship.RotateUnit(angle);
        }
        else
        {
            ship.RotateUnit(deg);
        }


        yield return new WaitForEndOfFrame();
    }

    IEnumerator Attack()
    {
        StartCoroutine(Turn(0));
        StartCoroutine(move());
        if(Mathf.Abs(angle) < ship.turnRate)
            ship.TryToFire(0,false);
        
        yield return new WaitForEndOfFrame();
    }

    IEnumerator move()
    {
        return move(Vector2.zero);
    }

    IEnumerator move(Vector2 moveDir) {
        if (state == State.ATTACK)
        {
            if (Mathf.Abs(angle) < ship.turnRate)
            {
                float dist = Vector3.Distance(trackingObject.transform.position, transform.position);
                if (dist > MaxDist)
                    ship.MoveUnit(new Vector2(0, 1), true);
                else if (dist < minDist)
                    ship.MoveUnit(new Vector2(0, -1), true);
                //else
                
            }
            if (bulletTransform)
            {   // if there is a bullet inside you will dodge.
                ship.MoveUnit(new Vector2(1f, 0), true);
            }

        }
        if (state == State.WANDER)
        {
            ship.MoveUnit(moveDir, true);
        }

        yield return new WaitForEndOfFrame();
    }



    void StateMachine()
    {
        if (!ship.alive)
            return;

        if (trackingObject != null)
        {           
            state = State.ATTACK;

        }
        else
        {
            if(state == State.ATTACK )
            {
                state = State.START_IDLE;
            }
        }


        switch (state)
        {
            case State.START_IDLE:
                StartCoroutine(Idle());
                break;
            case State.WANDER:
                //StartCoroutine(Wander());
                break;
            case State.PATROL:
                break;
            case State.TURN:
               // StartCoroutine(Turn(0));
                break;
            case State.FACE_PLAYER:
                break;
            case State.CHASE_PLAYER:
                break;
            case State.ATTACK:
                StartCoroutine(Attack());
                break;
            default:
                break;
        }

    }
}
