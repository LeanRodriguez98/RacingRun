using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    private GameManager gamemanagerInstance;
    private GameObject ButtonsPanel;
    private int optionOneIndex;
    private int optionTwoIndex;
    public Text OptionOneText;
    public Text OptionTwoText;
    private void Start()
    {
        gamemanagerInstance = GameManager.instance;
        ButtonsPanel = gamemanagerInstance.OptionsPanel;
        SelectOptions();
    }
    private void SelectOptions()
    {
        optionOneIndex = Random.Range(0, gamemanagerInstance.Terrains.Length);
        OptionOneText.text = gamemanagerInstance.Terrains[optionOneIndex].name;
        optionTwoIndex = Random.Range(0, gamemanagerInstance.Terrains.Length);
        OptionTwoText.text = gamemanagerInstance.Terrains[optionTwoIndex].name;
    }
    public void LoadScene(string name)
    {
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


    public void OptionOneButton()
    {
        gamemanagerInstance.Instanciator(optionOneIndex);
        ButtonsPanel.SetActive(false);
        SelectOptions();
    }


    public void OptionTwoButton()
    {
        gamemanagerInstance.Instanciator(optionTwoIndex);
        ButtonsPanel.SetActive(false);
        SelectOptions();
    }


    public void instancieTerrain()
    {
        gamemanagerInstance.Instanciator(Random.Range(0, gamemanagerInstance.Terrains.Length));
        ButtonsPanel.SetActive(false);
    }
    

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

  
}
