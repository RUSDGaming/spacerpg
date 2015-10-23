using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Game.Interfaces

{
    public interface PlayerEnteredPortal: IEventSystemHandler
    {
        void Event(int playerID, int portalNumber);
        
    }

    
}
