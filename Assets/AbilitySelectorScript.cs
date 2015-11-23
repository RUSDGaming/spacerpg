using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbilitySelectorScript : MonoBehaviour {




    [SerializeField]    int tree;
    [SerializeField]    Toggle tier_1_A;
    [SerializeField]    Toggle tier_1_B;
    [SerializeField]    Toggle tier_2_A;
    [SerializeField]    Toggle tier_2_B;
    [SerializeField]    Toggle tier_3_A;
    [SerializeField]    Toggle tier_3_B;

    [SerializeField]
    AbilitySelectorManager abilitySelectorManager;


   // bool t1 = true;
   // bool t2 = true;
   // bool t3 = true;
   


    // Use this for initialization
    void Start () {
        tier_2_A.interactable = false;
        tier_2_B.interactable = false;
        tier_3_A.interactable = false;
        tier_3_B.interactable = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Refresh() {

        int points = abilitySelectorManager.pointAvailable;

        if(points > 0 || tier_1_A.isOn || tier_1_B.isOn)
        {
            tier_1_A.interactable = true;
            tier_1_B.interactable = true;
        }
        else
        {            
            tier_1_A.interactable = false;
            tier_1_B.interactable = false;

        }

        if ((points > 0 || tier_2_A.isOn || tier_2_B.isOn) && (tier_1_A.isOn || tier_1_B.isOn))
        {
            tier_2_A.interactable = true;
            tier_2_B.interactable = true;            
        }
        else
        {
            tier_2_A.interactable = false;
            tier_2_B.interactable = false;            
        }


        if ((points > 0 || tier_3_A.isOn || tier_3_B.isOn) && (tier_2_A.isOn || tier_2_B.isOn))
        {
            tier_3_A.interactable = true;
            tier_3_B.interactable = true;
        }
        else
        {
            
            tier_3_A.interactable = false;
            tier_3_B.interactable = false;
        }
                

    }

    public void toggle(int tier)
    {

        if(tier == 1  && !(tier_1_A.isOn || tier_1_B.isOn))
        {
            tier_2_A.isOn = false;
            tier_2_B.isOn = false;
            tier_3_A.isOn = false;
            tier_3_B.isOn = false;
        }
        if(tier == 2 && !(tier_2_A.isOn || tier_2_B.isOn))
        {
            tier_3_A.isOn = false;
            tier_3_B.isOn = false;
        }



        Refresh();
        abilitySelectorManager.TierSelected();
        
    }

    public int PointsSpent()
    {
        if (tier_3_A.isOn || tier_3_B.isOn)        
            return 3;           
        else if(tier_2_B.isOn || tier_2_A.isOn)        
            return 2;   
        else if(tier_1_B.isOn || tier_1_A.isOn)        
            return 1;
        
        return 0;

       
    }

    void HighestTierEnabled(int tier)
    {
        switch (tier)
        {
            case 0:
                {                
                    tier_1_A.interactable = false;
                    tier_1_B.interactable = false;
                    tier_1_A.isOn = false;
                    tier_1_B.isOn = false;

                    tier_2_A.interactable = false;
                    tier_2_B.interactable = false;
                    tier_2_A.isOn = false;
                    tier_2_B.isOn = false;

                    tier_3_A.interactable = false;
                    tier_3_B.interactable = false;
                    tier_3_A.isOn = false;
                    tier_3_B.isOn = false;
                }

                break;
            case 1:
                {            

                    tier_1_A.interactable = true;
                    tier_1_B.interactable = true;                  

                    tier_2_A.interactable = false;
                    tier_2_B.interactable = false;                    

                    tier_3_A.interactable = false;
                    tier_3_B.interactable = false;
                    
                }
                break;
            case 2:
                {
                 
                }
                break;
            case 3:
                {
                
                }
                break;
        }
    }


    /// <summary>
    ///  finds out what abilities are selected and returns them
    /// </summary>
    /// <returns>selected abilites in an array</returns>
    public bool[] getSeletectAbilities()
    {
        bool[] abilities = new bool[6];

        abilities[0] = tier_1_A.isOn;
        abilities[1] = tier_1_B.isOn;
        abilities[2] = tier_2_A.isOn;
        abilities[3] = tier_2_B.isOn;
        abilities[4] = tier_3_A.isOn;
        abilities[5] = tier_3_B.isOn;



        return abilities;
    }


}
