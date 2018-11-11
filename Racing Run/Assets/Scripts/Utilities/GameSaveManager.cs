using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour {

    public static GameSaveManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }  

    private bool IsSavedFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    public void SaveGame(ScriptableObject soToSave)
    {
        if (!IsSavedFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/" + soToSave.name + "_data" )) 
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/" + soToSave.name + "_data");
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/" + soToSave.name + "_data/" + soToSave.name + "_save.txt");
        var json = JsonUtility.ToJson(soToSave);
        bf.Serialize(file, json);
        file.Close();
    }

    public void LoadGame(ScriptableObject soToLoad)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/" + soToLoad.name + "_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/" + soToLoad.name + "_data");
        }
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/" + soToLoad.name + "_data/" + soToLoad.name + "_save.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/" + soToLoad.name + "_data/" + soToLoad.name + "_save.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), soToLoad);
            file.Close();
        }
    }
}
