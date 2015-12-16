using UnityEngine;
using System.Collections;
using Game.Interfaces;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ProjectileScript : MonoBehaviour {

    public int id = -1;
    public float speed;
    public float damage;
    public float aliveTime = 5;
    public bool playerBullet;
    protected Rigidbody2D body;
    public bool timeDestroy = true;

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();

        init();
        
	}
	
    protected virtual void init()
    {
        iTween.Init(gameObject);
        body.AddRelativeForce(Vector2.up * speed);
        if (timeDestroy)
        {
            StartCoroutine(StartDestroy(aliveTime));
        }
    }
	// Update is called once per frame
	void Update () {
	
	}

   

    
    
                
    void OnTriggerEnter2D(Collider2D other)
    {

       // Debug.Log("tag hit is: " + other.tag);
       // Debug.Log(other.gameObject.layer.ToString());

        if (other.isTrigger)
            return;
        if (playerBullet && other.CompareTag("Player"))
            return;
        if (!playerBullet && other.CompareTag("Enemy"))
            return;

        if (playerBullet && other.CompareTag("Enemy"))
        {
            iDamage du = other.GetComponent<iDamage>();
            du.Damage(damage, id);
            StartCoroutine(BulletCollide());
        }
        else if (!playerBullet && other.CompareTag("Player"))
        {
            iDamage du = other.GetComponent<iDamage>();
            du.Damage(damage, id);
            StartCoroutine(BulletCollide());
        }
        else if (other.CompareTag("Neutral"))
        {
            iDamage du = other.GetComponent<iDamage>();
            du.Damage(damage, id);
            StartCoroutine(BulletCollide());
        }
        else if (!other.isTrigger)
        {
            StartCoroutine(BulletCollide());
        }

    }

    protected IEnumerator StartDestroy(float time)
    {
        yield return new WaitForSeconds(time);


        iTween.ScaleTo(gameObject,
            iTween.Hash("y", 0,
            "x", 0,
            "easeType", iTween.EaseType.easeOutQuad,
            "loopType", iTween.LoopType.none, "delay", 0,
            "time", .5f));
        yield return new WaitForSeconds(.6f);

        Destroy(this.gameObject);
    }

    protected IEnumerator BulletCollide()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.velocity = Vector3.zero;
        GetComponent<Collider2D>().enabled = false;        
        //todo show a different sprite here instead of tweening
        iTween.ScaleTo(gameObject,
          iTween.Hash("y", 0,
          "x", 0,
          "easeType", iTween.EaseType.easeOutQuad,
          "loopType", iTween.LoopType.none, "delay", 0,
          "time", .2f));
        yield return new WaitForSeconds(.2f);
        Destroy(gameObject); 
        
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
