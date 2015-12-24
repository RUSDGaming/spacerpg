using UnityEngine;
using System.Collections;

public class BackPortalScript : MonoBehaviour
{
    SideScrollTestLevel level;
    [SerializeField]    int levelId;

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
            other.transform.position = Vector2.zero;
            //level.ClearLevel();
            
            LevelManagerScript.instance.LevelCompleted(levelId);
            this.gameObject.SetActive(false);
        }
    }
}
