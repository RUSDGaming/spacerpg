using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadPannel : MonoBehaviour
{


    [SerializeField]
    MainMenuScript mms;
    [SerializeField]
    AbilitySelectorManager abilitySelecetorManager;
    [SerializeField]
    Text text;
    public int saveNumber;

    SaveGameInfo info;
    public static string saveGame = "SaveGameInfo";
    public static string current = "CurrentSave";

    // Use this for initialization
    void Start()
    {
        //  loadFile();
    }
    void loadFile()
    {
        loadSaveGame();
        text.text = "Save " + saveNumber + " - " + info.primaryType;
        if(info.primaryType != PriortySelectorScript.ClassType.EMPTY )
            text.text += " - lvl:" +info.level;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnEnable()
    {
        loadFile();
    }

    public void Awake()
    {
    }



    void loadSaveGame()
    {
        info = SaveGameSystem.LoadGame(saveGame + saveNumber) as SaveGameInfo;
        if (info == null)
        {
            Debug.Log("object was null");
            info = new SaveGameInfo();
            SaveSaveGame();
        }

      //  Debug.Log(info.primaryType);


    }

    void SaveSaveGame()
    {
        SaveGameSystem.SaveGame(info, saveGame + saveNumber);
    }

    public void deleteSaveGame()
    {
        SaveGameSystem.DeleteSaveGame(saveGame + saveNumber);
        loadFile();
    }

    public void StartSavedGame()
    {

        if (info.primaryType == PriortySelectorScript.ClassType.EMPTY)
        {
            mms.OpenNewGame();
            abilitySelecetorManager.saveGameNumber = saveNumber;
        }
        else
        {
           Debug.Log("Starting a game " + info.primaryType);
            PlayerPrefs.SetString(current, saveGame + saveNumber);
            Application.LoadLevel("FunLevel");
        }

    }

}
