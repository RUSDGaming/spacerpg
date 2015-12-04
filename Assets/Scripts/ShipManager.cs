using UnityEngine;
using System.Collections;

public class ShipManager : MonoBehaviour {



    public static ShipManager instance;

    [SerializeField]    Ship[] ships;
    // Use this for initialization
    void Start()
    {
        
    }

    void Awake() {
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
            Debug.LogError("No Ship Manager Manager Found!!!");
            return null;
        }
      //  Debug.Log("Createted a ship from the ship manager");
        Ship ship = Instantiate(instance.ships[i]);
        return ship;
    }
}
