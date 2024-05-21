using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveProgress(PlayerProgress progress)
    {
        string path = Application.persistentDataPath + "/progress.json";
        string data = JsonUtility.ToJson(progress);
        File.WriteAllText(path, data);
    }

    public static PlayerProgress LoadProgress()
    {
        string path = Application.persistentDataPath + "/progress.json";

        if(File.Exists(path)) 
        {
            string readData = File.ReadAllText(path);
            PlayerProgress data = JsonUtility.FromJson<PlayerProgress>(readData);
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static bool FileExists()
    {
        string path = Application.persistentDataPath + "/progress.json";
        if( File.Exists(path) )
        {
            return true;
        }
        else
        {
            return false; 
        }
    }
}
