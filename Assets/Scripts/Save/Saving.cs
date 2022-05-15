using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Saving
{
    public static void SaveGameData(PlayerStats stats)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(stats);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SaveData LoadGameData()
    {
        
        string path = Application.persistentDataPath + "/game.save";
        if(File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
   
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("Brak pliku: " + path + " Pomijam...");
            SaveData data = new SaveData();
            return data;
        }
        
    }
}
