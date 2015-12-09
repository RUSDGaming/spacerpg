using UnityEngine;
using System.Collections;
using Game.Interfaces;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ProjectileScript : MonoBehaviour {

    public int id = -1;
    public float speed;
    public float damage;
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

        body.AddRelativeForce(Vector2.up * speed);
        if (timeDestroy)
        {
            StartCoroutine(Destroy(5f));
        }
    }
	// Update is called once per frame
	void Update () {
	
	}

    protected IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);
        iTween.Init(gameObject);

        iTween.ScaleTo(gameObject,
            iTween.Hash("y", 0,
            "x", 0,
            "easeType", iTween.EaseType.easeOutQuad,
            "loopType", iTween.LoopType.none, "delay", 0,
            "time", .5f));
        yield return new WaitForSeconds(.6f);

        Destroy(this.gameObject);
    }

    
    
                
    void OnTriggerEnter2D(Collider2D other)
    {

        //Debug.Log("tag hit is: " + other.tag);
       // Debug.Log(other.gameObject.layer.ToString());

        if (other.isTrigger)
            return;
        if (playerBullet && other.CompareTag("Player"))
            return;
        if (!playerBullet && other.CompareTag("Enemy"))
            return;

        if (playerBullet && other.CompareTag("Enemy"))
        {
            DamageUnit du = other.GetComponent<DamageUnit>();
            du.Damage(damage, id);
            StartCoroutine(BulletCollide());
        }

        if (!playerBullet && other.CompareTag("Player"))
        {
            DamageUnit du = other.GetComponent<DamageUnit>();
            du.Damage(damage, id);
            StartCoroutine(BulletCollide());
        }

        if (other.CompareTag("Neutral"))
        {
            DamageUnit du = other.GetComponent<DamageUnit>();
            du.Damage(damage, id);
            StartCoroutine(BulletCollide());
        }

        if (!other.isTrigger)
            StartCoroutine(BulletCollide());

    }

    protected IEnumerator BulletCollide()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.velocity = Vector3.zero;
        GetComponent<Collider2D>().enabled = false;

        iTween.Init(gameObject);
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
