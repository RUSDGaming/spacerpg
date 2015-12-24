using UnityEngine;
using System.Collections;

public class DestroyStuff : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
