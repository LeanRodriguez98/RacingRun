using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour {

    #region Singleton
    public static StoreManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }
    #endregion
    [HideInInspector] public int loadedNuts;
    [HideInInspector] public int nuts = 0;
    [HideInInspector] public Car carInstance;
    private string sceneName;

    void Start ()
    {
        carInstance = Car.instance;
        SetNuts();
        sceneName = SceneManager.GetActiveScene().name;
        nuts = nuts + loadedNuts;
        PlayerPrefs.SetInt("Nuts", nuts);
    }
	
	void Update ()
    {
        if (carInstance != null)
        if (nuts != carInstance.nuts)
            nuts = carInstance.nuts;

        if (sceneName != SceneManager.GetActiveScene().name)
        {
            sceneName = SceneManager.GetActiveScene().name;
            nuts = nuts + loadedNuts;
            PlayerPrefs.SetInt("Nuts", nuts);
        }

    }

    private void SetNuts()
    {
        loadedNuts = PlayerPrefs.GetInt("Nuts", 0);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Nuts", nuts);
    }
}
