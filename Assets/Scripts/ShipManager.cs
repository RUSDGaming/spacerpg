using UnityEngine;
using System.Collections;

public class ShipManager : MonoBehaviour {



    public static ShipManager instance;

    [SerializeField]    PlayerShip[] ships;
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


    public static PlayerShip GetNewShip(int i)
    {

        if (!instance)
        {
            Debug.LogError("No Ship Manager Manager Found!!!");
            return null;
        }
      //  Debug.Log("Createted a ship from the ship manager");
        PlayerShip ship = Instantiate(instance.ships[i]);
        return ship;
    }
}
