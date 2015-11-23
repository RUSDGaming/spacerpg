using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PriorityScript : MonoBehaviour {


    public PriortySelectorScript.ClassType classType ;
    public Text text;
    


    public void setText()
    {
        switch (classType)
        {
            case PriortySelectorScript.ClassType.ATTACK:
                {
                    text.text = "A";
                }
                break;
            case PriortySelectorScript.ClassType.UTILITY:
                {
                    text.text = "U";
                }
                break;
            case PriortySelectorScript.ClassType.DEFFENSE:
                {
                    text.text = "D";
                }
                break;
            default:
                {
                    text.text = "-";
                }
                break;
        }
    }
}
