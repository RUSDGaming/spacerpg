using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    [SerializeField]    RectTransform imageTransform;


	// Use this for initialization
	void Start () {        
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void setImage(float currentValue, float maxValue)
    {

        float percent;
        if(maxValue > 0)
        {
            percent = currentValue / maxValue;
            percent = 1 - percent;       
        }
        else
        {
            percent = 1;
        }
        imageTransform.localPosition = new Vector3( -imageTransform.rect.width * percent, 0, 0);     
    }
}
