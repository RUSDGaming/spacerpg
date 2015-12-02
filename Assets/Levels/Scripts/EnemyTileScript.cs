using UnityEngine;
using System.Collections;

public class EnemyTileScript : RandomTileScript {

    [SerializeField]    EnemyShip enemy;
    public int numEnemies;

    public override void init()
    {

        collider.size = new Vector2(width, height);
        for (int i = 0; i < numEnemies; i++)
        {
            //Debug.Log("Initing thingnn");
            EnemyShip ship = EnemyGenerator.CreateRandomShip();

            ship.transform.SetParent(this.transform);

            float randX = Random.Range(0, width);
            float randY = Random.Range(0, height);

            ship.transform.localPosition = new Vector2(randX - width/2, randY - height /2);

        }
        
    }
}
