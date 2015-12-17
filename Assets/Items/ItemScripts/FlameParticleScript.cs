using UnityEngine;
using System.Collections;

public class FlameParticleScript : MonoBehaviour
{


    [SerializeField]
    float flameTime = 1f;
    bool firstFrame = true;
    Rigidbody2D body;
    [SerializeField]
    float speed = 100;
    // Use this for initialization
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        init();

    }


    protected void init()
    {
        //iTween.Init(gameObject);
        body.AddRelativeForce(Vector2.up * speed);

        //iTween.ScaleTo(gameObject,
        //  iTween.Hash("y", 0,
        //  "x", 0,
        //  "easeType", iTween.EaseType.easeOutQuad,
        //  "loopType", iTween.LoopType.none, "delay", 0,
        //  "time", flameTime));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FixedUpdate()
    {
        body.drag += .1f;
        if (firstFrame)
        {
            firstFrame = false;
            return;
        }
        if (Mathf.Abs(body.velocity.x) < 5f && Mathf.Abs(body.velocity.y) < 5f)
        {
            //Debug.Log(body.velocity);
            Destroy(gameObject);
        }
    }

    //public void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.isTrigger)
    //        return;
    //    else
    //        Destroy(this.gameObject);
    //}
}
