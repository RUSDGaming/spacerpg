using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(SpriteRenderer)) ]
public class ShowForceFeild : MonoBehaviour {


    [SerializeField]
    float displayRate =1f;

    SpriteRenderer sr;
    bool modAlpha;
	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //if (modAlpha)
       // {
            float newAplha = sr.color.a - displayRate * Time.deltaTime;
        //Debug.Log(newAplha);
            if (newAplha < 0)
            {
                newAplha = 0;
                this.gameObject.SetActive(false);
            }
            sr.color = new Color(1f, 1f, 1f,newAplha );
         //   modAlpha = false;
        //}
	}


    public void ShowSheild(float maxSheild, float currentSheild)
    {
        this.gameObject.SetActive(true);
        float percent = currentSheild / maxSheild;
        
        
        sr.color = new Color(1f, 1f, 1f, percent);

    }

}
