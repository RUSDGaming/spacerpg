using UnityEngine;
using System.Collections;

public class PortalScriptV2 : MonoBehaviour
{

    [SerializeField]    float startX;
    [SerializeField]    float startY;
    [SerializeField]   public SideScrollTestLevel region;
    

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
            // start the level
            // warp the player
            Vector2 movePos = new Vector2(region.transform.position.x+40, region.transform.position.y);
            other.transform.position = movePos;
            
            region.Init();
        }

    }

   

}
