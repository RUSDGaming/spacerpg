using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

using Game.Events;

public class PlayerStats : MonoBehaviour , EnemyDiedSubscriber
{
    [SerializeField]   GameObject levelUpText;

    public PlayerInfo playerInfo;
    public GameObject statHolder;

    // current level of the player
    const string LEVEL = "level";
    // current exp 
    const string EXP = "exp";
    // points play has to spend
    const string POINTS = "points";

    ControlSwitcher switcher;

    public enum STATS
    {
        HEALTH, ARMOR, SHIELD,
        TURN_RATE, MOVE_SPEED,
        DAMAGE, FIRE_RATE,
        LASER, PROJECTILE, EXPLOSION,
        ENERGY_REGENERATION, ENERGY_CAPACITY,
        FIRE, ELECTRIC, ACID, ICE, LIGHT, DARK
    }




    public float exp;
    public int level;  // used to calculate how much exp you will need for the next level as well as how many points you get on leveling up. 
    public int points;  // how many points you get to spend   

    public Dictionary<STATS, int> statBook = new Dictionary<STATS, int>();

    #region stat descriptions
    // improve health // 5% 
    // improve armor  // 5%
    // improve sheild // 5% increase in strength


    // improve turn rate // 10%
    // improve move speed // 5%


    // improve weapon damage // will increase all weapons base damage
    // improve weapon fireRate

    // improve laser  // fireate, range, damage, energy cost 
    // improve projectile // firerate , armor pen, ammo capacity  
    // improve explosion //  blast radius, damage, 

    // improve energy regen // 5%
    // improve energy capacity // 5%

    //**** improve elemmental damage;
    // fire  dot 
    // electrity lower energy regen rate
    // acid dot lowers armor
    // ice slows move and turn radius
    // light heals allies
    // dark deals damage based on other magic levels 
    #endregion

    // Use this for initialization
    void Start()
    {
        LoadPlayerStats();
        GameEventSystem.RegisterSubScriber(this);
        playerInfo = GetComponent<PlayerInfo>();
        switcher = GetComponent<ControlSwitcher>();
    }

    public void OnDestroy()
    {
        GameEventSystem.UnRegisterSubscriber(this);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ResetPlayerStats();
        }
    }

    private void ResetPlayerStats()
    {
        int i = 0;
        foreach (STATS stat in Enum.GetValues(typeof(STATS)))
        {
            int statLevel = 0;            
            PlayerPrefs.SetInt(stat.ToString(), statLevel);
            statBook[stat] = 0;
            StatMenuScript sms = statHolder.transform.GetChild(i).GetComponent<StatMenuScript>();
            i++;
            sms.Description = enumMethods.GetString(stat);
            sms.Level = statLevel.ToString();
        }

        exp = 0;
        level = 0;
        points = 0;
        PlayerPrefs.SetFloat(EXP, 0);
        PlayerPrefs.SetInt(LEVEL, 0);
        PlayerPrefs.SetInt(POINTS, 0);

        

    }


    // loads a dictionary with all the player stats.  This is mainly done for readablityl and code maintaiblitly. 
    private void LoadPlayerStats()
    {


        //level = PlayerPrefs.GetInt(LEVEL);        
        //exp = PlayerPrefs.GetFloat(EXP);
        //points = PlayerPrefs.GetInt(POINTS);

        //int i = 0;
        //foreach (STATS stat in Enum.GetValues(typeof(STATS)))
        //{
        //    int statLevel = PlayerPrefs.GetInt(stat.ToString());
        //    statBook.Add(stat, statLevel);
        //    StatMenuScript sms = statHolder.transform.GetChild(i).GetComponent<StatMenuScript>();
        //    i++;
        //    sms.Description = enumMethods.GetString(stat);
        //    sms.Level = statLevel.ToString();
        //    sms.setUpButton(stat, this);
        //}
        

    }



    public bool LevelUpStat(STATS stat)
    {

        if (points < 1)
            return false;

        int statLevel;
        if (statBook.TryGetValue(stat, out statLevel))
        {
            statLevel++;
            statBook[stat] = statLevel;
            points--;
            switcher.reloadShipStats(true);
        }


        return true;
    }

    /// <summary>
    /// This should be called when ever a player goes back to the hub 
    /// <br></br>
    /// and when he closes his menu after he makes changes
    /// </summary>
    public void SavePlayerStats()
    {
        foreach (STATS stat in Enum.GetValues(typeof(STATS)))
        {
            int statLevel;
            if (statBook.TryGetValue(stat, out statLevel))
            {
                PlayerPrefs.SetInt(stat.ToString(), statLevel);
            }
        }
        PlayerPrefs.SetFloat(EXP, exp);
        PlayerPrefs.SetInt(LEVEL, level);
        PlayerPrefs.SetInt(POINTS, points);
    }
    

    public static  float  SetActualStat(PlayerStats.STATS stat, int level ,float baseValue)
    {

        float actualValue = 0;

            switch (stat)
            {
                case STATS.HEALTH: actualValue = baseValue * (1 + .05f * level); break;
                case STATS.ARMOR: goto case STATS.HEALTH;                    
                case STATS.SHIELD: goto case STATS.HEALTH;                    
                case STATS.TURN_RATE:  actualValue = baseValue * (1 + .1f * level); break;
                case STATS.MOVE_SPEED: goto case STATS.HEALTH;
                case STATS.ENERGY_REGENERATION: goto case STATS.HEALTH;
                case STATS.ENERGY_CAPACITY: goto case STATS.HEALTH;
                default: actualValue = baseValue; break;
            }

        return actualValue;

    }

    public void HandleEvent(GameEventArgs args)
    {
        EnemyDiedEventArgs e = (EnemyDiedEventArgs) args;

        e.exp -= level;
        if (e.exp <= 0)
            return;
        

        // only get half exp if you dont kill it. 
        if(e.playerId == playerInfo.playerId)
        {
            exp += e.exp;
        }
        else
        {
            exp += e.exp / 2f;
        }
        // if you level up you get points;
        int tempLevel = level;       
        level = Mathf.FloorToInt( exp / 100);
        if(level - tempLevel > 0)
        {
            points += (level - tempLevel) *2;
            levelUpText.SetActive(true);
        }

        PlayerPrefs.SetFloat(EXP, exp);
        PlayerPrefs.SetInt(LEVEL, level);
        PlayerPrefs.SetInt(POINTS, points);
        InfoBlurbManager.CreateInfoBlurb(switcher.mainShip.transform.position, "EXP " + e.exp, Color.green);
    }

    

}


static class enumMethods
{
    public static string GetString(PlayerStats.STATS stat)
    {
        switch (stat)
        {
            case PlayerStats.STATS.HEALTH: return "Health";
            case PlayerStats.STATS.ARMOR: return "Armor";
            case PlayerStats.STATS.SHIELD: return "Sheild";
            case PlayerStats.STATS.TURN_RATE: return "Turn rate";
            case PlayerStats.STATS.MOVE_SPEED: return "Move Speed";
            case PlayerStats.STATS.DAMAGE: return "Damage";
            case PlayerStats.STATS.FIRE_RATE: return "Fire Rate";
            case PlayerStats.STATS.LASER: return "Laser";
            case PlayerStats.STATS.PROJECTILE: return "Projectile";
            case PlayerStats.STATS.EXPLOSION: return "Explosion";
            case PlayerStats.STATS.ENERGY_REGENERATION: return "Energy regen";
            case PlayerStats.STATS.ENERGY_CAPACITY: return "Energy cap";
            case PlayerStats.STATS.FIRE: return "Fire";
            case PlayerStats.STATS.ELECTRIC: return "Shock";
            case PlayerStats.STATS.ACID: return "Acid";
            case PlayerStats.STATS.ICE: return "Ice";
            case PlayerStats.STATS.LIGHT: return "Light";
            case PlayerStats.STATS.DARK: return "Dark";
            default: return "wtf";

        }

    }
}
