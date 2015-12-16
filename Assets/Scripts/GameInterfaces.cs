using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Game.Interfaces

{
    //public interface PlayerEnteredPortal: IEventSystemHandler
    //{
    //    void Event(int playerID, int portalNumber);

    //}


    public interface iDamage
    {
        void Damage(float damageAmount, int damagerId);
    }

    public interface Shooter
    {
        bool TryToFire(int weaponGroup, bool isPlayer);
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
        void SetActualStats(SaveGameInfo stats, bool heal);
    }

    public interface iShip : iDamage , Shooter , Movable,Rotatable, SetStats { }
}
