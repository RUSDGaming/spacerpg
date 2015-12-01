using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class SaveGameInfo : SaveGame {

    public float exp;
    public int level;  // used to calculate how much exp you will need for the next level as well as how many points you get on leveling up. 
    public int points;  // how many points you get to spend  

    public int money;

    public int HEALTH;
    public int ARMOR;
    public int SHIELD;
    public int TURN_RATE;
    public int MOVE_SPEED;
    public int DAMAGE;
    public int FIRE_RATE;
    public int LASER;
    public int PROJECTILE;
    public int EXPLOSION;
    public int ENERGY_REGENERATION;
    public int ENERGY_CAPACITY;
    public int FIRE;
    public int ELECTRIC;
    public int ACID;
    public int ICE;
    public int LIGHT;
    public int DARK;

    public PriortySelectorScript.ClassType primaryType;

    public PriortySelectorScript.ClassType p1;
    public PriortySelectorScript.ClassType p2;
    public PriortySelectorScript.ClassType p3;

    public bool a1 = false;
    public bool a2 = false;
    public bool a3 = false;
    public bool a4 = false;
    public bool a5 = false;
    public bool a6 = false;

    public bool u1 = false;
    public bool u2 = false;
    public bool u3 = false;
    public bool u4 = false;
    public bool u5 = false;
    public bool u6 = false;

    public bool d1 = false;
    public bool d2 = false;
    public bool d3 = false;
    public bool d4 = false;
    public bool d5 = false;
    public bool d6 = false;

    [NonSerialized]
    public int playerId;
}
