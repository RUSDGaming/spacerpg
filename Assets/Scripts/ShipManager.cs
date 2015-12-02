using UnityEngine;
using System.Collections;

public class ShipManager : MonoBehaviour {



    public static ShipManager instance;

    [SerializeField]    Ship[] ships;
    // Use this for initialization
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public static Ship GetNewShip(int i)
    {

        if (!instance)
        {
            Debug.LogError("no Item Manager Found!!!");
            return null;
        }
        Debug.Log("Createted an item from the item manager");
        Ship ship = Instantiate(instance.ships[i]);
        return ship;
    }
}
