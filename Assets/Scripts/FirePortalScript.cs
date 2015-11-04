using UnityEngine;
using System.Collections;
using Game.Interfaces;
using Game.Events;
public class FirePortalScript : MonoBehaviour{




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

        Debug.Log("Player entered the Portal");


            PortalEventArgs args = new PortalEventArgs { info = GetComponent<LevelGenInfo>(), portalId = 911, playerId = other.GetComponent<PlayerController>().id }; 
            GameEventSystem.PublishEvent(typeof(PortalSubscriber), args);

            

        }
    }

    
}
