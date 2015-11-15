using UnityEngine;
using System.Collections;

public class PlayerSpawnTileScript : RandomTileScript {

    

    public override void init()
    {

        collider.size = new Vector2(width, height);
        GameObject[] players =  GameObject.FindGameObjectsWithTag("Player");

        Vector3 playerSpawn = new Vector3( transform.position.x  , transform.position.y );
        
        foreach (GameObject p in players)
        {
            p.transform.position = playerSpawn;
               
        }
    }


}
