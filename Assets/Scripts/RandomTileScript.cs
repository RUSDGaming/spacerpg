using UnityEngine;
using System.Collections;

public class RandomTileScript : MonoBehaviour
{

    public Transform levelParent;
    public float height = 80;
    public float width = 80;
    public  BoxCollider2D collider;
    

    /// <summary>
    /// call this to set up the tile, each extension if the till will handle the rest  bababababy
    /// </summary>
    public virtual void init()
    {
        collider.size = new Vector2(width, height);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            return;
        if (other.isTrigger)
            return;

        other.transform.SetParent(this.transform);


    }
}
