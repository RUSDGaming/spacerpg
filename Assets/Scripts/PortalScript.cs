using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Game.Interfaces;
using Game.Events;

public class PortalScript : MonoBehaviour {


    public GameLogic gl;
    public int id;
    /// <summary>
    /// This is what the portal tell s the level gen to make. 
    /// </summary>
    public LevelGenInfo levelInfo;
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
        Debug.Log("A player Has entered The Portal");

            PortalEventArgs args = new PortalEventArgs { info = this.levelInfo, portalId=this.id ,playerId = other.GetComponent<PlayerController>().id};
            
            GameEventSystem.PublishEvent(typeof(PortalSubscriber),args); 
           // ExecuteEvents.ExecuteHierarchy<PlayerEnteredPortal>(gameObject, null, (x, y) => x.);
        }
    }
}
