using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerStats : MonoBehaviour
{
     

    public enum STATS
    {
        HEALTH, ARMOR, SHEILD,
        TURN_RATE, MOVE_SPEED,
        DAMAGE, FIRE_RATE,
        LASER, PROJECTILE, EXPLOSION,
        ENERGY_REGENERATION, ENERGY_CAPACITY,
        FIRE, ELECTRIC, ACID, ICE, LIGHT, DARK
    }


    public GameObject statHolder;


    float exp;
    int level;  // used to calculate how much exp you will need for the next level as well as how many points you get on leveling up. 
    int points;  // how many points you get to spend   

    public Dictionary<STATS, int> statBook = new Dictionary<STATS, int>();

    // improve health // you will have te ability to use scrap to repair yourself
    // improve armor  // is good vs projectiles
    // improve sheild // is good vs lasers


    // improve turn rate
    // improve move speed


    // improve weapon damage // will increase all weapons base damage
    // improve weapon fireRate

    // improve laser  // fireate, range, damage, energy cost 
    // improve projectile // firerate , armor pen, ammo capacity  
    // improve explosion //  blast radius, damage, 

    // improve energy regen // increases capacity 2 times energy regen
    // improve energy capacity // increases energy regen by x% of capacity

    //**** improve elemmental damage;
    // fire  dot 
    // electrity lower energy regen rate
    // acid dot lowers armor
    // ice slows move and turn radius
    // light heals allies
    // dark deals damage based on other magic levels 

    // Use this for initialization
    void Start()
    {
        LoadPlayerStats();
    }


    // loads a dictionary with all the player stats.  This is mainly done for readablityl and code maintaiblitly. 
    private void LoadPlayerStats()
    {
        int i = 0;
        foreach (STATS stat in Enum.GetValues(typeof(STATS)))
        {
           int statLevel =  PlayerPrefs.GetInt(stat.ToString());
            statBook.Add(stat, statLevel);
            StatMenuScript sms =  statHolder.transform.GetChild(i).GetComponent<StatMenuScript>();
            i++;
            sms.Description = enumMethods.GetString(stat);
            sms.Level = statLevel.ToString();
            sms.setUpButton(stat, this);
        }

    }


    public void LevelUpStat(STATS stat)
    {
        int statLevel;
        if (statBook.TryGetValue(stat, out statLevel))
        {
            statLevel++;
            statBook[stat] =  statLevel;
        }
    }
    
    /// <summary>
    /// This should be called when ever a player goes back to the hub 
    /// <br></br>
    /// and when he closes his menu after he makes changes
    /// </summary>
    public void SavePlayerStats()
    {
        foreach(STATS stat in Enum.GetValues(typeof(STATS)))
        {
            int statLevel;
            if(statBook.TryGetValue(stat,out statLevel))
            {
                PlayerPrefs.SetInt(stat.ToString(), statLevel);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}


static class enumMethods
{
    public static string GetString(this PlayerStats.STATS stat)
    {
        switch (stat)
        {
            case PlayerStats.STATS.HEALTH: return "Health";
            case PlayerStats.STATS.ARMOR: return "Armor";
            case PlayerStats.STATS.SHEILD: return "Sheild";
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
