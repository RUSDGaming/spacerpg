using UnityEngine;
using System.Collections;
using Game.Events;
using Game.Interfaces;

public class PlayerEXPManager : MonoBehaviour, EnemyDiedSubscriber
{


    //public SaveGameInfo saveGameInfo;
    [SerializeField]
    GameObject levelUpText;    
    ControlSwitcher switcher;


    void Awake()
    {
        switcher = GetComponent<ControlSwitcher>();
        if(switcher == null)
        {
            Debug.LogError("Could not Find Switcher");
        }
    }

    // Use this for initialization
    void Start()
    {
        //switcher = gameObject.GetComponent<ControlSwitcher>();
        GameEventSystem.RegisterSubScriber(this);
        //saveGameInfo = switcher.playerStats;
    }

    public void OnDestroy()
    {
       // GameEventSystem.UnRegisterSubscriber(this);
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void HandleEvent(GameEventArgs args)
    {
        EnemyDiedEventArgs e = (EnemyDiedEventArgs)args;

        e.exp -= switcher.saveGameInfo.level;
        if (e.exp <= 0)
            return;

        // Debug.Log(e.playerId);
        // Debug.Log(saveGameInfo.playerId);

        // only get half exp if you dont kill it. 
        if (e.playerId == switcher.saveGameInfo.playerId)
        {
            switcher.saveGameInfo.exp += e.exp;
            InfoBlurbManager.CreateInfoBlurb(switcher.mainShip.transform.position, "EXP " + e.exp, Color.green);
        }
        else
        {
            switcher.saveGameInfo.exp += e.exp / 2f;
            InfoBlurbManager.CreateInfoBlurb(switcher.mainShip.transform.position, "EXP " + e.exp / 2f, Color.green);
        }
        // if you level up you get points;
        int tempLevel = switcher.saveGameInfo.level;
        switcher.saveGameInfo.level = Mathf.FloorToInt(switcher.saveGameInfo.exp / 100);
        if (switcher.saveGameInfo.level - tempLevel > 0)
        {
            switcher.saveGameInfo.points += (switcher.saveGameInfo.level - tempLevel) * 2;

            levelUpText.SetActive(true);



        }

        //Debug.Log("handle : " + saveGameInfo.exp);

    }
}
