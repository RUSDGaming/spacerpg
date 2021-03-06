﻿using UnityEngine;
using System.Collections;
using System;

public class StatHolderScript : MonoBehaviour {



    private SaveGameInfo saveGameInfo;

    public SaveGameInfo SaveGameInfo {
        get {
            
            return saveGameInfo;
            }
        set {
            saveGameInfo = value;
            LoadPlayerStats();
        } }

    [SerializeField]    ControlSwitcher switcher;
    [SerializeField]     GameObject statHolder;

    // Use this for initialization
    void Start () {
       

	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public bool LevelUpStat(PlayerStats.STATS stat, ref int levelValue)
    {
        if (saveGameInfo.points < 1)
            return false;

        int points = 1;
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (SaveGameInfo.points >= 5)
                points = 5;
            else
                points = saveGameInfo.points;
        }

        //int statLevel;
        //if (statBook.TryGetValue(stat, out statLevel))
        //{
        //    statLevel++;
        //    statBook[stat] = statLevel;
        //    //points--;
        //    switcher.reloadShipStats(true);
        //}

        switch (stat)
        {
            case PlayerStats.STATS.HEALTH:
                saveGameInfo.HEALTH += points;
                levelValue = saveGameInfo.HEALTH;
                break;
            case PlayerStats.STATS.ARMOR:
                saveGameInfo.ARMOR += points;
                levelValue = saveGameInfo.ARMOR;
                break;
            case PlayerStats.STATS.SHIELD:
                saveGameInfo.SHIELD += points;
                levelValue = saveGameInfo.SHIELD;
                break;
            case PlayerStats.STATS.TURN_RATE:
                saveGameInfo.TURN_RATE += points;
                levelValue = saveGameInfo.TURN_RATE;
                break;
            case PlayerStats.STATS.MOVE_SPEED:
                saveGameInfo.MOVE_SPEED += points;
                levelValue = saveGameInfo.MOVE_SPEED;
                break;
            case PlayerStats.STATS.DAMAGE:
                saveGameInfo.DAMAGE += points;
                levelValue = saveGameInfo.DAMAGE;
                break;
            case PlayerStats.STATS.FIRE_RATE:
                saveGameInfo.FIRE_RATE += points;
                levelValue = saveGameInfo.FIRE_RATE;
                break;
            case PlayerStats.STATS.LASER:
                saveGameInfo.LASER += points;
                levelValue = saveGameInfo.LASER;
                break;
            case PlayerStats.STATS.PROJECTILE:
                saveGameInfo.PROJECTILE += points;
                levelValue = saveGameInfo.PROJECTILE;
                break;
            case PlayerStats.STATS.EXPLOSION:
                saveGameInfo.EXPLOSION += points;
                levelValue = saveGameInfo.EXPLOSION;
                break;
            case PlayerStats.STATS.ENERGY_REGENERATION:
                saveGameInfo.ENERGY_REGENERATION += points;
                levelValue = saveGameInfo.ENERGY_REGENERATION;
                break;
            case PlayerStats.STATS.ENERGY_CAPACITY:
                saveGameInfo.ENERGY_CAPACITY += points;
                levelValue = saveGameInfo.ENERGY_CAPACITY;
                break;
            case PlayerStats.STATS.FIRE:
                saveGameInfo.FIRE += points;
                levelValue = saveGameInfo.FIRE;
                break;
            case PlayerStats.STATS.ELECTRIC:
                saveGameInfo.ELECTRIC += points;
                levelValue = saveGameInfo.ELECTRIC;
                break;
            case PlayerStats.STATS.ACID:
                saveGameInfo.ACID += points;
                levelValue = saveGameInfo.ACID;
                break;
            case PlayerStats.STATS.ICE:
                saveGameInfo.ICE += points;
                levelValue = saveGameInfo.ICE;
                break;
            case PlayerStats.STATS.LIGHT:
                saveGameInfo.LIGHT += points;
                levelValue = saveGameInfo.LIGHT;
                break;
            case PlayerStats.STATS.DARK:
                saveGameInfo.DARK += points;
                levelValue = saveGameInfo.DARK;
                break;
            default:
                break;
        }
        saveGameInfo.points -= points;
        switcher.reloadShipStats(true);

        SaveGameSystem.SaveGame(saveGameInfo, PlayerPrefs.GetString(LoadPannel.current));
        return true;
    }


    // loads a dictionary with all the player stats.  This is mainly done for readablityl and code maintaiblitly. 
    private void LoadPlayerStats()
    {
        //Debug.Log("loading players statsss");       
        int i = 0;
        foreach (PlayerStats.STATS stat in Enum.GetValues(typeof(PlayerStats.STATS)))
        {            
            int statLevel = 0;
            switch (stat)
            {
                case PlayerStats.STATS.HEALTH:
                    statLevel = saveGameInfo.HEALTH;
                    break;
                case PlayerStats.STATS.ARMOR:
                    statLevel = saveGameInfo.ARMOR;
                    break;
                case PlayerStats.STATS.SHIELD:
                    statLevel = saveGameInfo.SHIELD;
                    break;
                case PlayerStats.STATS.TURN_RATE:
                    statLevel = saveGameInfo.TURN_RATE;
                    break;
                case PlayerStats.STATS.MOVE_SPEED:
                    statLevel = saveGameInfo.MOVE_SPEED;
                    break;
                case PlayerStats.STATS.DAMAGE:
                    statLevel = saveGameInfo.DAMAGE;
                    break;
                case PlayerStats.STATS.FIRE_RATE:
                    statLevel = saveGameInfo.FIRE_RATE;
                    break;
                case PlayerStats.STATS.LASER:
                    statLevel = saveGameInfo.LASER;
                    break;
                case PlayerStats.STATS.PROJECTILE:
                    statLevel = saveGameInfo.PROJECTILE;
                    break;
                case PlayerStats.STATS.EXPLOSION:
                    statLevel = saveGameInfo.EXPLOSION;
                    break;
                case PlayerStats.STATS.ENERGY_REGENERATION:
                    statLevel = saveGameInfo.ENERGY_REGENERATION;
                    break;
                case PlayerStats.STATS.ENERGY_CAPACITY:
                    statLevel = saveGameInfo.ENERGY_CAPACITY;
                    break;
                case PlayerStats.STATS.FIRE:
                    statLevel = saveGameInfo.FIRE;
                    break;
                case PlayerStats.STATS.ELECTRIC:
                    statLevel = saveGameInfo.ELECTRIC;
                    break;
                case PlayerStats.STATS.ACID:
                    statLevel = saveGameInfo.ACID;
                    break;
                case PlayerStats.STATS.ICE:
                    statLevel = saveGameInfo.ICE;
                    break;
                case PlayerStats.STATS.LIGHT:
                    statLevel = saveGameInfo.LIGHT;
                    break;
                case PlayerStats.STATS.DARK:
                    statLevel = saveGameInfo.DARK;
                    break;
                default:
                    break;
            }
            
            //statBook.Add(stat, statLevel);
            StatMenuScript sms = statHolder.transform.GetChild(i).GetComponent<StatMenuScript>();
            i++;
            sms.Description = enumMethods.GetString(stat);
            sms.Level = statLevel.ToString();
            sms.setUpButton(stat, this);
        }


    }


}
