using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveGameSystem {

	public static bool SaveGame(SaveGame saveGame, string name)
    {
        BinaryFormatter formmater = new BinaryFormatter();

        using(FileStream stream = new FileStream(GetSavePath(name), FileMode.Create))
        {
            try
            {
                formmater.Serialize(stream, saveGame);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return false;
            }
        }
        return true;

    }

    public static SaveGame LoadGame(string name)
    {
        if (!DoesSaveGameExist(name))
        {
            return null;
        }
        BinaryFormatter formmater = new BinaryFormatter();

        using(FileStream stream = new FileStream(GetSavePath(name), FileMode.Open))
        {
            try
            {
                return formmater.Deserialize(stream) as SaveGame;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return null;
            }
        }

    }

    public static bool DeleteSaveGame(string name)
    {
        try
        {
            File.Delete(GetSavePath(name));
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public static bool DoesSaveGameExist(string name)
    {
        return File.Exists(GetSavePath(name));
    }

    private static string GetSavePath(string name)
    {
        return Path.Combine(Application.persistentDataPath, name + ".sav");
    }

}
