using UnityEngine;
using System.Collections;

public class WarperScript : MonoBehaviour {

    public Vector2 maxPos;
    public Vector2 minPos;


    public void OnTriggerEnter2D(Collider2D other)
    {
        WarpScript ws = other.GetComponent<WarpScript>();
        if (!ws)
        {
            ws = other.gameObject.AddComponent<WarpScript>();
        }
            ws.init(maxPos, minPos);
        ws.enabled = true;
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        WarpScript ws = other.GetComponent<WarpScript>();
        if (!ws)
        {
            ws = other.gameObject.AddComponent<WarpScript>();            
        }
        ws.enabled = false;

    }
}
