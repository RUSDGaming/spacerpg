using UnityEngine;
using System.Collections;
using SpriteTile;

public class MiniMapCameraScript : MonoBehaviour {

    public Transform tracking;
    public Vector3 LevelOffset;
    public Transform parent;

	// Use this for initialization
	void Start () {
        parent = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {

        if (!tracking)
            return;

        Vector3 regionStart = LevelOffset + parent.position;
       // Debug.Log(regionStart);
        Vector3 offsetPos =  (tracking.position - regionStart) /20;

       // Debug.Log(offsetPos);
        // Int2 pos =   Tile.WorldToMapPosition(offsetPos,0);
        //Vector3 posv = Tile.MapToWorldPosition(pos);
        //posv.z = -10;
        offsetPos.z = -10;
        this.transform.localPosition = offsetPos;

	
	}
}
