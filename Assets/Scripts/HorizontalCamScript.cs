using UnityEngine;
using System.Collections;

public class HorizontalCamScript : MonoBehaviour
{

    [SerializeField]
    Transform ship;

    [SerializeField]    Transform background;

    [SerializeField]    float paralaxRatio = .9f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LateUpdate()
    {

        if (!ship)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");


            if (players.Length > 1)
            {
                ship = players[0].transform;
            }
            else
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player)
                    ship = player.transform;
            }

            return;
        }

        //transform.position = ship.position;
        Vector3 newPos = ship.position;
        newPos.x = ship.position.x;
        newPos.y = ship.position.y;
        newPos.z = -10;
        transform.position = newPos;


        if (background)
        {
            newPos.x *= paralaxRatio;
            newPos.y *= paralaxRatio;

            newPos.z = 0;
            background.position = newPos;
        }

    }
}
