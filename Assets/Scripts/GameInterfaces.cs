﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Game.Interfaces

{
    //public interface PlayerEnteredPortal: IEventSystemHandler
    //{
    //    void Event(int playerID, int portalNumber);

    //}


    public interface DamageUnit
    {
        void Damage(float damageAmount);
    }

    public interface Shooter
    {
        bool TryToFire(int weaponGroup);
    }

    public interface Movable
    {
        void MoveUnit(Vector2 force, bool relativeInput);
    }

    public interface Rotatable
    {
        void RotateUnit(float deg);
    }

    public interface Inventory
    {
        int GetSlotCount();
    }
    public interface SetStats{
        void SetActualStats(PlayerStats stats);
    }

    public interface iShip : DamageUnit , Shooter , Movable,Rotatable, SetStats { }
}
