using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoBlurb : MonoBehaviour {

    [SerializeField] Text text;
    [SerializeField] float moveAmount = 10;
    [SerializeField] float bounnceTime;
    [SerializeField]    float fadeTime;
	// Use this for initialization
	void Start () {
        iTween.Init(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
   IEnumerator StartFade()
    {
        float randomX = Random.Range(-moveAmount, moveAmount);

        iTween.MoveTo(gameObject,
            iTween.Hash("y", gameObject.transform.position.y + moveAmount,
            "x", gameObject.transform.position.x + randomX,
            "easeType", iTween.EaseType.easeOutQuad,
            "loopType", iTween.LoopType.none, "delay", 0,
            "time", bounnceTime));

        yield return new WaitForSeconds(bounnceTime);

        text.CrossFadeAlpha(0, fadeTime, false);

        iTween.MoveTo(gameObject,
           iTween.Hash("y", gameObject.transform.position.y - moveAmount *2,
           "x", gameObject.transform.position.x + randomX,
           "easeType", iTween.EaseType.easeOutQuad,
           "loopType", iTween.LoopType.none, "delay", 0,
           "time", fadeTime));

        yield return new WaitForSeconds(fadeTime);
        Destroy(gameObject);
    }

    public void init(Vector2 pos, string msg)
    {
        init(pos, msg, Color.magenta);
    }
    public void init(Vector2 pos, string msg, Color color)
    {


        text.text = msg;
        text.color = color;


        
        //iTween.MoveTo(gameObject,new Vector3(900,900,0),3);


        StartCoroutine(StartFade());
    }

}
