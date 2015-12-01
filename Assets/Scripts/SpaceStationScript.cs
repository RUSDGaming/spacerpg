using UnityEngine;
using System.Collections;
using Game.Interfaces;
using System.Collections.Generic;

public class SpaceStationScript : EnemyShip{

    [SerializeField]    EnemyShip ship;

    public float spawnRate = 5;
    public int numSpawnsAtOnce = 5;
    float lastSpawn;

    List<EnemyShip> ships;
  
    

    // Use this for initialization
    void Start () {
        base.Start();
        ships = new List<EnemyShip>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Time.time - lastSpawn >= spawnRate)
        {            
            lastSpawn = Time.time;
            ships.RemoveAll(item => item == null);
            if(ships.Count < numSpawnsAtOnce)
            {
                EnemyShip es = Instantiate(ship);
                es.transform.SetParent(transform);        
                    
                es.transform.localPosition = new Vector3(0 ,-8);
                ships.Add(es);
            }
        }


	}
    

}
