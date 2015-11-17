using UnityEngine;
using System.Collections;
using Game.Interfaces;

public class PlayerAI : MonoBehaviour {

    public enum State
    {
        IDLE, START_IDLE, WANDER, PATROL, TURN, FACE_PLAYER, CHASE_PLAYER, ATTACK
    }

    public State state = State.START_IDLE;

    Ship ship;
    Rigidbody2D body;
    float angle;

    public float minDist = 4f;
    public float MaxDist = 8f;
    int idleStartId;

    Vector3 startPos;

    public Transform trackingObject;
    public float maxIdleTime = 5f;

    // Use this for initialization
    void Start()
    {
        ship = GetComponent<Ship>();
        if (ship == null)
        {
            body = GetComponentInParent<Rigidbody2D>();
            ship = GetComponentInParent<Ship>();
        }
        startPos = transform.position;
    }

    public void Init(Vector2 start)
    {
        startPos = start;
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

        if (!trackingObject)
        {

            if (other.CompareTag("Enemy"))
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
        if (other.transform == trackingObject)
        {
            trackingObject = null;
            SetTurretTracking(null);
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

        if (state == State.IDLE && id == idleStartId)
        {
            //Debug.Log("starting wander from idle");
            state = State.WANDER;
            StartCoroutine(Wander());

        }
    }

    IEnumerator Wander()
    {
       // Debug.Log("start wander");
        float deg = Random.Range(-180, 180);
        float degTurned = 0;
        //Debug.Log("Turn deg is " + deg);
        while (degTurned < Mathf.Abs(deg) && state == State.WANDER)
        {
            // Debug.Log(deg);
            float turnDeg = ship.turnRate * Mathf.Sign(deg) * Time.fixedDeltaTime / 4;
            StartCoroutine(Turn(turnDeg));
            degTurned += turnDeg * Mathf.Sign(deg);

            yield return new WaitForFixedUpdate();
        }
        if (state != State.WANDER)
        {
            yield break;
        }

        float moveTime = Random.Range(.5f, 1f);
        //moveTime = 5f;
        while (moveTime > 0 && state == State.WANDER)
        {
            StartCoroutine(move(new Vector2(0, .5f)));
            StartCoroutine(Turn(0));
            moveTime -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        if (state == State.WANDER)
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
        if (state == State.ATTACK)
        {
            Vector3 enenmyToPlayer = (trackingObject.position) - transform.position;
            angle = Vector2.Angle(enenmyToPlayer, transform.up);
            Vector3 cross = Vector3.Cross(enenmyToPlayer, transform.up);
            if (cross.z > 0)
                angle = -angle;
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
        if (Mathf.Abs(angle) < ship.turnRate)
            ship.TryToFire(0, false);

        yield return new WaitForEndOfFrame();
    }

    IEnumerator move()
    {
        return move(Vector2.zero);
    }

    IEnumerator move(Vector2 moveDir)
    {
        if (state == State.ATTACK)
            if (Mathf.Abs(angle) < ship.turnRate)
            {
                float dist = Vector3.Distance(trackingObject.transform.position, transform.position);
                if (dist > MaxDist)
                    ship.MoveUnit(new Vector2(0, 1), true);
                else if (dist < minDist)
                    ship.MoveUnit(new Vector2(0, -1), true);
                //else
                //    ship.MoveUnit(new Vector2(.5f, 0),true);
            }
        if (state == State.WANDER)
        {
            ship.MoveUnit(moveDir, true);
        }

        yield return new WaitForEndOfFrame();
    }



    void StateMachine()
    {

        if (trackingObject != null)
        {
            state = State.ATTACK;

        }
        else
        {
            if (state == State.ATTACK)
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
