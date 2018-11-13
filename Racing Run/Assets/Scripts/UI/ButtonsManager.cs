using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{


    public GameObject PausePanel;
    [HideInInspector]public Car carInstance;

    private ButtonsManager instance;
    private GameSaveManager gameSaveManagerInstance;


    public Sprite volumeOn;
    public Sprite volumeOff;
    public Button volumeButon;

    public SO_ItemTexture[] soItemTextures;
    public SO_DoTutorial soDoTutorial;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }
    private void Start()
    {
        if (Car.instance != null)
        {
            carInstance = Car.instance;
         
        }


        gameSaveManagerInstance = GameSaveManager.instance;
    }

    public void AddMoney(int nutsToAdd)
    {
        carInstance.nuts += nutsToAdd;
        carInstance.soPlayerStats.nuts += nutsToAdd;
        gameSaveManagerInstance.SaveGame(carInstance.soPlayerStats);

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
        if(!carInstance.InmortalCheat)
            carInstance.InmortalCheat = true;
        else
            carInstance.InmortalCheat = false;

    }

    public void FullHeal()
    {
        carInstance.life = 3;
    }

    public void RemoveMoney()
    {
        carInstance.nuts = 0;
        carInstance.soPlayerStats.nuts = 0;
        gameSaveManagerInstance.SaveGame(carInstance.soPlayerStats);
    }

    public void Pause()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            PausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            PausePanel.SetActive(false);
        }
        
    }


    public void ReplayTutorial()
    {
        soDoTutorial.doTutorial = true;
        gameSaveManagerInstance.SaveGame(soDoTutorial);
    }

    public void LoadScene(string name)
    {
        Time.timeScale = 1;      
        SceneManager.LoadScene(name);

    }
    public void Restart()
    {       

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Test()
    {
        Debug.Log("TEST");
    }


    public void TurnOff(GameObject go)
    {
        go.SetActive(false);
    }

    public void TurnOn(GameObject go)
    {
        go.SetActive(true);
    }



    public void ChangeVolume()
    {
        if (AudioListener.volume > 0)
        {
            AudioListener.volume = 0;
            volumeButon.image.sprite = volumeOff;
        }
        else
        {
            AudioListener.volume = 1;
            volumeButon.image.sprite = volumeOn;
        }
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    public Animator animations;
    public void TriggerButton()
    {
        animations.SetTrigger("trigger");
    }

}
