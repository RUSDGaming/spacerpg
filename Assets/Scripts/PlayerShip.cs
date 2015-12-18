using UnityEngine;
using System.Collections;
using Game.Interfaces;
using System;
using Game.Events;


[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(PlayerController))]
public class PlayerShip : BaseShip{

    [SerializeField]    protected ControlSwitcher switcher;

    protected override void Awake()
    {
        base.Awake();
        switcher = GetComponentInParent<ControlSwitcher>();
    }


    public override void SetActualStats(SaveGameInfo stats, bool heal)
    {
        base.SetActualStats(stats, heal);
        foreach (WeaponInventory slot in weaponSlots)
        {
            slot.init(stats,true);
        }
    }

    public override bool CanDamage(bool playerProjectile)
    {
        return !playerProjectile;
    }

    protected override void KillShip(int killerId)
    {        
        switcher.PlayerDeath();
    }
    
}
