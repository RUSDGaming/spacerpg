using UnityEngine;
using System.Collections;

public class RandomTileScript : MonoBehaviour
{


    [SerializeField]  protected  GameObject asteroid;
    //public Transform levelParent;
    public float height = 80;
    public float width = 80;
    public  BoxCollider2D collider;

    public int x;
    public int y;
    public LevelGeneratorScript levelGeneratorScript;
    public int numAsteroids = 10;

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
        {
            levelGeneratorScript.PlayerEnteredTile(x, y);
            return;
        }
            
        if (other.isTrigger)
            return;

        other.transform.SetParent(this.transform);


    }
}
