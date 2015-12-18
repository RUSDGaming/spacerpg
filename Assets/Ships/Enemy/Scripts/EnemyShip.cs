using UnityEngine;
using System.Collections;
using Game.Interfaces;
using System;
using Game.Events;

public class EnemyShip : BaseShip {


   
    
    

   
  



   
    // how much exp this unit gives on death
    [SerializeField]   float exp;



    public override void SetActualStats(SaveGameInfo stats, bool heal)
    {
        base.SetActualStats(stats, heal);
        foreach (WeaponInventory slot in weaponSlots)
        {
            slot.init(playerStats,false);
        }
    }

    protected override void KillShip(int killerdId)
    {
       // Debug.Log("ship dide" + killerdId);
        // all the ai will have negative ids
        if(killerdId > 0)
        {
        EnemyDiedEventArgs args = new EnemyDiedEventArgs { playerId = 1, exp = this.exp };
        GameEventSystem.PublishEvent(typeof(EnemyDiedSubscriber), args);
        }

        DropItem dropItem = GetComponent<DropItem>();
        if (dropItem)
        {
            dropItem.SpawnItem();
        }
        StartCoroutine(DeathAnim());

    }

  

 
    

    public override bool CanDamage(bool playerProjectile)
    {
        return playerProjectile;
    }
}
