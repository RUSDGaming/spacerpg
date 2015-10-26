using UnityEngine;
using System.Collections;
using SpriteTile;

public class SpawnLevel : MonoBehaviour {


    [SerializeField]
    TextAsset startLevel;

	// Use this for initialization
	void Start () {
        Tile.SetCamera();
        Tile.LoadLevel(startLevel);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
