using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class AbilitySelectorManager : MonoBehaviour {

    [SerializeField]    AbilitySelectorScript attack;
    [SerializeField]    AbilitySelectorScript utility;
    [SerializeField]    AbilitySelectorScript deffense;

    [SerializeField]    PriorityScript[] p;

    [SerializeField]    PlayerClass playerClass;
    [SerializeField]    Button startGameButton;
    [SerializeField]    ShipDescriptionPanel shipDescriptionPanel;


    public int pointAvailable = 3;

    //public string[] items;

    // Use this for initialization
    void Start () {
    
	}
	
	

    public void TierSelected()
    {

        pointAvailable = 3;
        pointAvailable -= attack.PointsSpent();
        pointAvailable -= utility.PointsSpent();
        pointAvailable -= deffense.PointsSpent();

        attack.Refresh();
        utility.Refresh();
        deffense.Refresh();

        if (pointAvailable  == 0)
        {
            
            int i = 0;
          
            for(int a = 0; a < attack.PointsSpent(); a++)
            {
                p[i].classType = PriortySelectorScript.ClassType.ATTACK;
                p[i].setText();
                i++;
            }

            for (int a = 0; a < utility.PointsSpent(); a++)
            {
                p[i].classType = PriortySelectorScript.ClassType.UTILITY;
                p[i].setText();
                i++;
            }

            for (int a = 0; a < deffense.PointsSpent(); a++)
            {
                p[i].classType = PriortySelectorScript.ClassType.DEFFENSE;
                p[i].setText();
                i++;
            }
            startGameButton.interactable = true;
            shipDescriptionPanel.SetClass(GetPrimaryType());




        }else
        {
            p[0].classType = PriortySelectorScript.ClassType.EMPTY;
            p[1].classType = PriortySelectorScript.ClassType.EMPTY;
            p[2].classType = PriortySelectorScript.ClassType.EMPTY;
            p[0].setText();
            p[1].setText();
            p[2].setText();
            startGameButton.interactable = false;
        }

       

    }




    public void CreateShip()
    {     
        
        playerClass.primaryType = GetPrimaryType();


        bool[] attackAbilities = attack.getSeletectAbilities();
        bool[] utilAbilities = utility.getSeletectAbilities();
        bool[] defAbilities = deffense.getSeletectAbilities();

        playerClass.a1 = attackAbilities[0];
        playerClass.a2 = attackAbilities[1];
        playerClass.a3 = attackAbilities[2];
        playerClass.a4 = attackAbilities[3];
        playerClass.a5 = attackAbilities[4];
        playerClass.a6 = attackAbilities[5];

        playerClass.u2 = utilAbilities[1];
        playerClass.u1 = utilAbilities[0];
        playerClass.u3 = utilAbilities[2];
        playerClass.u4 = utilAbilities[3];
        playerClass.u5 = utilAbilities[4];
        playerClass.u6 = utilAbilities[5];

        playerClass.d2 = defAbilities[1];
        playerClass.d1 = defAbilities[0];
        playerClass.d3 = defAbilities[2];
        playerClass.d4 = defAbilities[3];
        playerClass.d5 = defAbilities[4];
        playerClass.d6 = defAbilities[5];

        playerClass.p1 = p[0].classType;
        playerClass.p2 = p[1].classType;
        playerClass.p3 = p[2].classType;


        Debug.Log("Creating Ship with stats...");
        Debug.Log(playerClass.ToString());


    }

    public PriortySelectorScript.ClassType GetPrimaryType()
    {
        int numAttack = attack.PointsSpent();
        int numUtility = utility.PointsSpent();
        int numDeffense = deffense.PointsSpent();

        if (numAttack > 1) return  PriortySelectorScript.ClassType.ATTACK;
        else if (numUtility > 1) return PriortySelectorScript.ClassType.UTILITY;
        else if (numDeffense > 1) return PriortySelectorScript.ClassType.DEFFENSE;
        else return PriortySelectorScript.ClassType.EMPTY;
    }


}
