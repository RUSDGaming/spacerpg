using UnityEngine;
using System.Collections;

public class BossTileScript : RandomTileScript {


    [SerializeField]    EnemyShip boss;

    public override void init()
    {
        collider.size = new Vector2(width, height);
        EnemyShip ship = Instantiate(boss);
        ship.transform.SetParent(transform);
        ship.transform.localPosition = new Vector2(0, 0);
    }
}
