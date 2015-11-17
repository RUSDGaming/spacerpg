using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Game.Events
{

    public class GameEventSystem  {


       static List<SubScriber> subscribers;

        public static void RegisterSubScriber(SubScriber sub)
        {
            if(subscribers == null)
            {
                subscribers = new List<SubScriber>();
            }

            subscribers.Add(sub);
           // Debug.Log("added a subscriber");
           // Debug.Log(sub.GetType().ToString());
           // Debug.Log(sub is PortalSubscriber);
        }

        
        public static void PublishEvent(Type subscriberType,GameEventArgs args)
        {
            // selects all of the relavent types
            if (subscribers == null)
            {
                return;
            }
            var query = from sub in subscribers where  subscriberType.IsAssignableFrom(sub.GetType()) select sub;
            
            foreach(SubScriber sub in query)
            {
              //  Debug.Log("sending info to thing");
                sub.HandleEvent(args);
            }


        }

    }

    public interface SubScriber{
         void HandleEvent(GameEventArgs args);
    }

    public interface PortalSubscriber : SubScriber{ }
    public interface LevelLoadedSubscriber : SubScriber { }
    public interface HomeSubscriber : SubScriber { }
    public interface PlayerDamagedSubscriber : SubScriber { };
    public interface EnemyDiedSubscriber : SubScriber { };
}
