﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatMenuScript : MonoBehaviour
{

    [SerializeField]
    private Text description;
    [SerializeField]
    private Text level;
    private int levelInt = 0;

    [SerializeField]
    private Button addLevelButton;

    public string Description
    {
        get{ return description.text; }
        set{ description.text = value; }
    }

    public string Level
    {
        get{ return level.text; }
        set{
            level.text = value;
            int.TryParse(value, out levelInt);
        }
    }

    public void setUpButton(PlayerStats.STATS stat, PlayerStats playerStats)
    {
        addLevelButton.onClick.AddListener(() => {
            playerStats.LevelUpStat(stat);
            levelInt++;
            Level = levelInt.ToString();
        });
    }
   
}
