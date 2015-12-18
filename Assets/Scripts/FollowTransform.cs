using UnityEngine;
using System.Collections;

public class FollowTransform : MonoBehaviour {



    public Transform followObject;
    [SerializeField]
    Transform backGround;

    [SerializeField]
    float backGroundParalaxRatio = .9f;


    [SerializeField]
    Transform transform1;
    [SerializeField]
    float transform1Paralax;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (transform1)
            transform1.position = followObject.position * transform1Paralax;

	}
    void LateUpdate()
    {

        if (!followObject)
        {
            GameObject[] players =  GameObject.FindGameObjectsWithTag("Player");

            
            if(players.Length > 1)
            {   
                followObject = players[0].transform;
            }
            else
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");      
                if(player)          
                followObject = player.transform;
            }

            return;
        }

        if(backGround)
        backGround.position = followObject.position * backGroundParalaxRatio;

        if (followObject)
        {
        Vector3 transformPosition = new Vector3( 
          followObject.position.x,
          followObject.position.y,
            this.transform.position.z) ;
        this.transform.position = transformPosition;
        }
    }
}
