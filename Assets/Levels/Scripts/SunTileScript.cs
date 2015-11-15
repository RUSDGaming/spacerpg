using UnityEngine;
using System.Collections;

public class SunTileScript : RandomTileScript {

    [SerializeField] GameObject Sun;



    public override void init()
    {
        GameObject sun = Instantiate(Sun);

        sun.transform.SetParent(transform);
        sun.transform.localPosition = Vector3.zero;
    }
}
