﻿using UnityEngine;
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


}
