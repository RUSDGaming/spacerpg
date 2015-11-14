using UnityEngine;
using System.Collections;

public class RandomTileScript : MonoBehaviour {

    public Transform levelParent;
    public float height = 80;
    public float width = 80;

    /// <summary>
    /// call this to set up the tile, each extension if the till will handle the rest  bababababy
    /// </summary>
    public virtual void init()
    {

    }
}
