using UnityEngine;
using System.Collections;

using Game.Interfaces;
public class EnemyAI : MonoBehaviour
{

    iShip ship;

    public Transform trackingObject;
    // Use this for initialization
    void Start()
    {
        ship = GetComponent<iShip>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void FixedUpdate()
    {
        if (trackingObject != null)
        {
            
            Vector3 enenmyToPlayer = (trackingObject.position) - transform.position;
            float angle = Vector2.Angle(enenmyToPlayer, transform.up);
            Vector3 cross = Vector3.Cross(enenmyToPlayer, transform.up);
            if (cross.z > 0)
                angle = -angle;

            ship.RotateUnit(angle);

            ship.TryToFire(0,false);
           // if (body.velocity.magnitude > enemyScript.maxSpeed)
          //  {
          //      body.velocity = body.velocity.normalized * enemyScript.maxSpeed;
          //  }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }

    public void OnTriggerStay2D(Collider2D other)
    {

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
        foreach (WeaponInventory weaponInvetory in GetComponent<EnemyShip>().weaponSlots)
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
        }
    }
}
