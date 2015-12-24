using UnityEngine;
using System.Collections;

public class MechanicScript : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerShip ship = collision.GetComponent<PlayerShip>();
        if (ship)
        {
            ship.playerAtMechanic = true;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerShip ship = collision.GetComponent<PlayerShip>();
        if (ship)
        {
            ship.playerAtMechanic = false;
        }

    }
}
