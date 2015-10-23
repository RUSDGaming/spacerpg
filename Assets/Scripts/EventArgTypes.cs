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
    }


}
