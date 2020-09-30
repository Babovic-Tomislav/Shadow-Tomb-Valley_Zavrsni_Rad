using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;

public class SaveLoadGame : MonoBehaviour
{
    private SaveData CreateSaveGame()
    {
        SaveData save = new SaveData();

        save.playerData = new SaveData.PlayerData();
        save.playerInventory = new SaveData.PlayerInventory();
        save.playerInventory.SetUpInventorySave();
        save.playerData.SetPositionAndScene(GameObject.FindGameObjectWithTag("Player").transform.position.x, GameObject.FindGameObjectWithTag("Player").transform.position.y);
        return save;
    }
    public void SaveGame()
    {
        
        SaveData save = CreateSaveGame();



        BinaryFormatter bf = new BinaryFormatter();
      
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();


    }

    public void LoadGame()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            SaveData save = (SaveData)bf.Deserialize(file);
            file.Close();
            save.LoadGame();
        }
    }

}
