using UnityEngine;
using System.Collections;

public class OuterRadius : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("PlayerLeftRadius!");
    }
}
