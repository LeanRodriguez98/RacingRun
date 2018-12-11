using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Cheats : MonoBehaviour {
    private GameSaveManager gameSaveManagerInstance;
    [HideInInspector] public Car carInstance;
    public SO_ItemTexture[] soItemTextures;
    public Text inmortalCheatText;
    private void Start()
    {
        if (GameSaveManager.instance != null)
            gameSaveManagerInstance = GameSaveManager.instance;
        if (Car.instance != null)
            carInstance = Car.instance;
    }

    public void AddMoney(int nutsToAdd)
    {
        carInstance.nuts += nutsToAdd;
        carInstance.soPlayerStats.nuts += nutsToAdd;
        gameSaveManagerInstance.SaveGame(carInstance.soPlayerStats);

    }

    public void SubstractMoney(int nutsToSubstract)
    {
        if (carInstance.nuts >= nutsToSubstract)
        {
            carInstance.nuts -= nutsToSubstract;
            carInstance.soPlayerStats.nuts -= nutsToSubstract;
            gameSaveManagerInstance.SaveGame(carInstance.soPlayerStats);
        }
        else
        {
            carInstance.soPlayerStats.nuts -= carInstance.nuts;
            carInstance.nuts -= carInstance.nuts;
            gameSaveManagerInstance.SaveGame(carInstance.soPlayerStats);
        }
        

    }

    public void RemoveTextures()
    {
        for (int i = 0; i < soItemTextures.Length; i++)
        {
            soItemTextures[i].boughted = false;
        }
        soItemTextures[0].boughted = true;
        carInstance.soPlayerStats.materialName = soItemTextures[0].materialName;
        for (int i = 0; i < soItemTextures.Length; i++)
        {
            gameSaveManagerInstance.SaveGame(soItemTextures[i]);
        }
        carInstance.soPlayerStats.materialName = "CarTexture1";
        for (int i = 0; i < carInstance.meshParts.Length; i++)
        {
            carInstance.meshParts[i].material = Resources.Load<Material>(carInstance.soPlayerStats.materialName);
        }
        gameSaveManagerInstance.SaveGame(carInstance.soPlayerStats);

    }

    public void Inmortal()
    {
        if (!carInstance.InmortalCheat)
        {
            inmortalCheatText.text = "OFF Inmortal";
            carInstance.InmortalCheat = true;
        }
        else
        {
            inmortalCheatText.text = "ON Inmortal";
            carInstance.InmortalCheat = false;
        }
    }

    public void FullHeal()
    {
        carInstance.life = 3;
    }

    public void FullNitro()
    {
        carInstance.nitroAcumulation = carInstance.maxNitroAcumulation;
    }

    public void RemoveMoney()
    {
        carInstance.nuts = 0;
        carInstance.soPlayerStats.nuts = 0;
        gameSaveManagerInstance.SaveGame(carInstance.soPlayerStats);
    }

}
