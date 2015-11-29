using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class AbilitySelectorManager : MonoBehaviour {

    [SerializeField]    AbilitySelectorScript attack;
    [SerializeField]    AbilitySelectorScript utility;
    [SerializeField]    AbilitySelectorScript deffense;

    [SerializeField]    PriorityScript[] p;

    [SerializeField]    MainMenuScript mainMenuScript;


    //  [SerializeField]    PlayerClass playerClass;
    public SaveGameInfo saveGameInfo;
    [SerializeField]    Button startGameButton;
    [SerializeField]    ShipDescriptionPanel shipDescriptionPanel;

    [SerializeField]    Image shipImage;

    public int pointAvailable = 3;
    public int saveGameNumber;
    //public string[] items;

    // Use this for initialization
    void Start () {
        saveGameInfo = new SaveGameInfo();
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
        //int saveGameInfo;
        saveGameInfo.primaryType = GetPrimaryType();


        bool[] attackAbilities = attack.getSeletectAbilities();
        bool[] utilAbilities = utility.getSeletectAbilities();
        bool[] defAbilities = deffense.getSeletectAbilities();

        saveGameInfo.a1 = attackAbilities[0];
        saveGameInfo.a2 = attackAbilities[1];
        saveGameInfo.a3 = attackAbilities[2];
        saveGameInfo.a4 = attackAbilities[3];
        saveGameInfo.a5 = attackAbilities[4];
        saveGameInfo.a6 = attackAbilities[5];

        saveGameInfo.u2 = utilAbilities[1];
        saveGameInfo.u1 = utilAbilities[0];
        saveGameInfo.u3 = utilAbilities[2];
        saveGameInfo.u4 = utilAbilities[3];
        saveGameInfo.u5 = utilAbilities[4];
        saveGameInfo.u6 = utilAbilities[5];

        saveGameInfo.d2 = defAbilities[1];
        saveGameInfo.d1 = defAbilities[0];
        saveGameInfo.d3 = defAbilities[2];
        saveGameInfo.d4 = defAbilities[3];
        saveGameInfo.d5 = defAbilities[4];
        saveGameInfo.d6 = defAbilities[5];

        saveGameInfo.p1 = p[0].classType;
        saveGameInfo.p2 = p[1].classType;
        saveGameInfo.p3 = p[2].classType;


        Debug.Log("Creating Ship with stats...");
        Debug.Log(saveGameInfo.ToString());
        SaveGameSystem.SaveGame(saveGameInfo, LoadPannel.saveGame + saveGameNumber);
        mainMenuScript.OpenLoad();
    }

    public PriortySelectorScript.ClassType GetPrimaryType()
    {
        int numAttack = attack.PointsSpent();
        int numUtility = utility.PointsSpent();
        int numDeffense = deffense.PointsSpent();

        if (numAttack > 1) return  PriortySelectorScript.ClassType.ATTACK;
        else if (numUtility > 1) return PriortySelectorScript.ClassType.UTILITY;
        else if (numDeffense > 1) return PriortySelectorScript.ClassType.DEFFENSE;
        else return PriortySelectorScript.ClassType.BALANCED;
    }


}
