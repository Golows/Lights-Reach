using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveProgress(PlayerProgress progress)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progress.save";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerProgress data = progress;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerProgress LoadProgress()
    {
        string path = Application.persistentDataPath + "/progress.save";

        if(File.Exists(path)) 
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerProgress data =  binaryFormatter.Deserialize(stream) as PlayerProgress;
            stream.Close();
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
        string path = Application.persistentDataPath + "/progress.save";
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
