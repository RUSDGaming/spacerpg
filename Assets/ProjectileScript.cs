using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {


    public float speed;
    public float damage;
    public bool playerBullet;
   

	// Use this for initialization
	void Start () {

        Rigidbody2D body = GetComponent<Rigidbody2D>();

        body.AddRelativeForce(Vector2.up * speed);
        StartCoroutine(Destroy());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
                
    void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("tag hit is: " +  other.tag);
       // Debug.Log(other.gameObject.layer);
        
        if (playerBullet && other.CompareTag("Enemy" ))
        {
          //  Debug.Log("sending message to destroy");
            other.SendMessage("Damage", damage);
            DestroyObject(this.gameObject);
        }

        if(!playerBullet && other.CompareTag( "Player"))
        {
            other.SendMessage("Damage", damage);
            DestroyObject(this.gameObject);
        }
        if(!other.isTrigger)
        DestroyObject(this.gameObject);
        
    }

    public void IsPlayer(bool isPlayer)
    {
        playerBullet= isPlayer;
        if (isPlayer)
        {
            gameObject.layer = LayerMask.NameToLayer("playerProjectile");
        }
        else
        {
           gameObject.layer = LayerMask.NameToLayer("enemyProjectile");
        }
    }


}
