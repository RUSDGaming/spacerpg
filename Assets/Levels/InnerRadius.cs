using UnityEngine;
using System.Collections;

public class InnerRadius : MonoBehaviour
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

        if(collision.CompareTag("Player"))
        Debug.Log("Player about to leave level!");
    }

}
