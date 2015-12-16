using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Game.Events
{

    public class GameEventSystem : MonoBehaviour
    {
        public void Awake()
        {
            if (!instance)
            {
                //Debug.Log("Setting up GameEventSystem Instance");
                instance = this;
            }


            //Debug.Log(Util.RotateVector(new Vector2(1, 0), 0));
            //Debug.Log(Util.RotateVector(new Vector2(1, 0), 90));
            //Debug.Log(Util.RotateVector(new Vector2(1, 0), 180));
            //Debug.Log(Util.RotateVector(new Vector2(1, 0), 270));
        }

        public static GameEventSystem instance;

        List<SubScriber> subscribers;

        public static void RegisterSubScriber(SubScriber sub)
        {
            if (instance.subscribers == null)
            {
                instance.subscribers = new List<SubScriber>();
            }
            instance.subscribers.Add(sub);
        }

        public static void UnRegisterSubscriber(SubScriber sub)
        {
            if (instance.subscribers != null)
            {
                instance.subscribers.Remove(sub);
            }
        }


        public static void PublishEvent(Type subscriberType, GameEventArgs args)
        {
            // selects all of the relavent types
            if (instance == null ||  instance.subscribers == null)
            {
                return;
            }
            var query = from sub in instance.subscribers where subscriberType.IsAssignableFrom(sub.GetType()) select sub;

            foreach (SubScriber sub in query)
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
