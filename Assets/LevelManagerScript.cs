using UnityEngine;
using System.Collections;

public class LevelManagerScript : MonoBehaviour {

    public static LevelManagerScript instance;
    [SerializeField]    PortalScriptV2[] portals;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {	
	}

    public void LevelCompleted(int id)
    {
        if(id + 1 < portals.Length)
        {
            portals[id].region.ClearLevel();
            portals[id + 1].gameObject.SetActive(true);
        }
    }
}
