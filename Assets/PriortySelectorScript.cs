using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PriortySelectorScript : MonoBehaviour {


    //[SerializeField]    AbilitySelectorManager abilitySelectorManager;


    [SerializeField]    PriorityScript[] priorities;

    public enum ClassType
    {
        EMPTY, ATTACK, UTILITY, DEFFENSE, BALANCED
    }


    //[SerializeField]
    //Button swap1;
    //[SerializeField]
    //Button swap2;

    int current = 0 ;

    [SerializeField] Text text;
	// Use this for initialization
	void Start () {
      //  priorities = new RectTransform[3];
	}
	

    public void toggle()
    {        
    }

    public void Reset()
    {      
    }

    public void Swap12()
    {

        ClassType temp = priorities[0].classType;
        priorities[0].classType = priorities[1].classType;
        priorities[1].classType = temp;
        priorities[0].setText();
        priorities[1].setText();

        
    }

    public void Swap23()
    {

        ClassType temp = priorities[1].classType;
        priorities[1].classType = priorities[2].classType;
        priorities[2].classType = temp;
        priorities[1].setText();
        priorities[2].setText();

    }

}
