using UnityEngine;
using System.Collections;

public class WeaponTester : MonoBehaviour {

    [SerializeField]
    Weapon wep;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            float energy = 999;
            wep.TryToFire( ref energy);
        }
        if (Input.GetMouseButtonUp(0))
        {
            wep.MouseUp();
        }
	}
}
