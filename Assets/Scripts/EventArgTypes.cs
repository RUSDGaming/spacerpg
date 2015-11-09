using UnityEngine;
using System.Collections;


namespace Game.Events
{
    public abstract class GameEventArgs { };


    public class PortalEventArgs : GameEventArgs
    {
        public int portalId;
        public LevelGenInfo info;
        public int playerId;
    };

    public class LevelFinishedLoadingEventArgs : GameEventArgs
    {
       public Vector2 startPos;
        public Vector2 endPos;
    }

    public class PlayerHitEventArgs : GameEventArgs
    {
        public int playerId;
        public float damageDelt;
    }

    public class PlayerHomeEventArgs: GameEventArgs
    {
        public int playerId;
    }

    public class EnemyDiedEventArgs : GameEventArgs
    {
        // who killed the enemy
        public int playerId;
        // how much exp the player gets for kiling the unit
        public float exp;
    }


}
