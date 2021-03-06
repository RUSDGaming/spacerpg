﻿using UnityEngine;
using System.Collections;

public class PlayerSpawnTileScript : RandomTileScript {

    

    public override void init()
    {

        collider.size = new Vector2(width, height);
        //GameObject[] players =  GameObject.FindGameObjectsWithTag("Player");

        Vector3 playerSpawn = new Vector3( transform.position.x  , transform.position.y );


        LevelGeneratorScript lgs = GetComponentInParent<LevelGeneratorScript>();

        lgs.playerSpawnPosition = playerSpawn;



        float maxWidth = Mathf.Sqrt(width * 1.1f);
        float maxHeight = Mathf.Sqrt(height / 2 * 1.1f);
        float maxLenght = width * .55f;

        for (int i = 0; i < numAsteroids; i++)
        {
            float randDeg = Random.Range(0, 360f);
            float length = Random.Range(10, maxLenght);
            length = AsteroidTileScript.func(length);

            //float randY = Random.Range(0, maxHeight) * Mathf.Sign(Random.Range(-1f, 1));
            GameObject gameObject = (GameObject)Instantiate(asteroid);
            gameObject.transform.SetParent(this.transform);
            gameObject.transform.localPosition = new Vector2(length * Mathf.Sin(randDeg * Mathf.Deg2Rad), length * Mathf.Cos(randDeg * Mathf.Deg2Rad));
        }

    }


}
