using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour {


    [SerializeField]    RectTransform mainMenu;
    [SerializeField]    RectTransform newGame;
    [SerializeField]    RectTransform load;
    [SerializeField]    RectTransform options;


    public void OpenNewGame()
    {
        newGame.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }


    public void OpenLoad()
    {
        load.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    public void OpenOptions()
    {
        options.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    public void Back()
    {
        newGame.gameObject.SetActive(false);
        load.gameObject.SetActive(false);
        options.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
    }


   




    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadScene(string sceneName)
    {
        Application.LoadLevel(sceneName);
    }

}
