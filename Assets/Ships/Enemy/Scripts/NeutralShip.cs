using UnityEngine;
using System.Collections;

public class NeutralShip : EnemyShip {

    public override bool CanDamage(bool playerProjectile)
    {
        return true;
    }
}
