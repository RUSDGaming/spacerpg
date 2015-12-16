using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour {


    public static EnemyGenerator instance;

    [SerializeField]    EnemyShip[] ships;
    [SerializeField]    Weapon[] weapons;


	// Use this for initialization
	void Start () {
	}

    void Awake()
    {
        if (!instance)
            instance = this;
    }
	
	// Update is called once per frame
	void Update () {

	}

    public static EnemyShip CreateRandomShip() {

        if (!instance)
        {
            Debug.LogError("You dont have an enemy Generator Insntance!!!");
            return null;
        }

        int randomNum = Random.Range(0, instance.ships.Length);

        EnemyShip newShip = Instantiate(instance.ships[randomNum]);

        // add a random weapon to each slot, or some of the slots depending on difficullty. 

        return newShip;

    }

}
