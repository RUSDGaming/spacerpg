using UnityEngine;
using System.Collections;

public class CrusherScript : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerShip ship = collision.GetComponent<PlayerShip>();
        if (ship)
        {
            ship.playerAtCrusher = true;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        PlayerShip ship = collision.GetComponent<PlayerShip>();
        if (ship)
        {
            ship.playerAtCrusher = false;
        }

    }
}
