using UnityEngine;
using System.Collections;
using Game.Events;
using Game.Interfaces;

public class PlayerEXPManager : MonoBehaviour, EnemyDiedSubscriber
{


    public SaveGameInfo saveGameInfo;
    [SerializeField]
    GameObject levelUpText;
    [SerializeField]
    ControlSwitcher switcher;

    // Use this for initialization
    void Start()
    {
        //switcher = gameObject.GetComponent<ControlSwitcher>();
        GameEventSystem.RegisterSubScriber(this);
        //saveGameInfo = switcher.playerStats;
    }

    public void OnDestroy()
    {
        GameEventSystem.UnRegisterSubscriber(this);
    }


    // Update is called once per frame
    void Update()
    {

    }


    public void HandleEvent(GameEventArgs args)
    {
        EnemyDiedEventArgs e = (EnemyDiedEventArgs)args;

        e.exp -= saveGameInfo.level;
        if (e.exp <= 0)
            return;

        // Debug.Log(e.playerId);
        // Debug.Log(saveGameInfo.playerId);

        // only get half exp if you dont kill it. 
        if (e.playerId == saveGameInfo.playerId)
        {
            saveGameInfo.exp += e.exp;
            InfoBlurbManager.CreateInfoBlurb(switcher.mainShip.transform.position, "EXP " + e.exp, Color.green);
        }
        else
        {
            saveGameInfo.exp += e.exp / 2f;
            InfoBlurbManager.CreateInfoBlurb(switcher.mainShip.transform.position, "EXP " + e.exp / 2f, Color.green);
        }
        // if you level up you get points;
        int tempLevel = saveGameInfo.level;
        saveGameInfo.level = Mathf.FloorToInt(saveGameInfo.exp / 100);
        if (saveGameInfo.level - tempLevel > 0)
        {
            saveGameInfo.points += (saveGameInfo.level - tempLevel) * 2;

            levelUpText.SetActive(true);



        }

        //Debug.Log("handle : " + saveGameInfo.exp);

    }
}
