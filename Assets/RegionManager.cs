using UnityEngine;
using System.Collections;

public class RegionManager : MonoBehaviour {

    public static RegionManager instance;

    [SerializeField]    LevelGeneratorScript[] regions;

    int region = 0;
	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public LevelGeneratorScript GetNextRegion()
    {

        if(region < regions.Length)
        return regions[region++];

        Debug.LogError("not enough regions, Did you forget to reste the counter?");
        return null;

    } 

    public void ResetRegionCounter()
    {
        region = 0;
    }
}
