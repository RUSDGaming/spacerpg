using UnityEngine;
using System.Collections;

public class SpaceStationTileScript : RandomTileScript {

    [SerializeField]    SpaceStationScript station;
    [SerializeField]    EnemyShip enemyShip;
    public int numShips = 4;


    public override void init()
    {
        SpaceStationScript spaceStation = Instantiate(station);

        spaceStation.transform.SetParent(transform);
        spaceStation.transform.localPosition = Vector3.zero;

        for (int i = 0; i < numShips; i++)
        {
            EnemyShip es = Instantiate(enemyShip);
            es.transform.SetParent(transform);
            float randX = Random.Range(0, width);
            float randY = Random.Range(0, height);
            es.transform.localPosition = new Vector3(randX, randY);
        }


    }


}
