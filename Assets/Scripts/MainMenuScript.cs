using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {


    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

}
