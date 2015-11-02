using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ProjectileScript : MonoBehaviour {


    public float speed;
    public float damage;
    public bool playerBullet;
    protected Rigidbody2D body;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();

        init();
        
	}
	
    protected virtual void init()
    {

        body.AddRelativeForce(Vector2.up * speed);
        StartCoroutine(Destroy(5f));
    }
	// Update is called once per frame
	void Update () {
	
	}

    protected IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
                
    void OnTriggerEnter2D(Collider2D other)
    {

        //Debug.Log("tag hit is: " + other.tag);
        Debug.Log(other.gameObject.layer.ToString());

        if (other.isTrigger)
            return;
        if (playerBullet && other.CompareTag("Player"))
            return;
        if (!playerBullet && other.CompareTag("Enemy"))
            return;

        if (playerBullet && other.CompareTag("Enemy"))
        {
            //  Debug.Log("sending message to destroy");
            other.SendMessage("Damage", damage);
            DestroyObject(this.gameObject);
        }

        if (!playerBullet && other.CompareTag("Player"))
        {
            other.SendMessage("Damage", damage);
            DestroyObject(this.gameObject);
        }

        if (!other.isTrigger)
            DestroyObject(this.gameObject);

    }

    public void IsPlayer(bool isPlayer)
    {
        playerBullet= isPlayer;
        if (isPlayer)
        {
            gameObject.layer = LayerMask.NameToLayer("playerProjectile");
            foreach(Transform t in gameObject.transform)
            {
             //   t.gameObject.layer = LayerMask.NameToLayer("playerProjectile");
            }
        }
        else
        {
           gameObject.layer = LayerMask.NameToLayer("enemyProjectile");
            foreach (Transform t in gameObject.transform)
            {
              //  t.gameObject.layer = LayerMask.NameToLayer("enemyProjectile");
            }
        }
    }


}
