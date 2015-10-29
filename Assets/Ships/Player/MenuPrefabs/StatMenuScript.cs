using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatMenuScript : MonoBehaviour
{

    [SerializeField]
    private Text description;
    [SerializeField]
    private Text level;
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
        set{ level.text = value; }
    }

    public void setUpButton(PlayerStats.STATS stat, PlayerStats playerStats)
    {
        addLevelButton.onClick.AddListener(() => { playerStats.LevelUpStat(stat); } );
    }
   
}
