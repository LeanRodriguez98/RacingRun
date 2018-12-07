using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    private GameSaveManager gameSaveManagerInstance;
    private int nutsDisplayed;

    [Header("ItemPrefab")]
    [Space(10)]
    public GameObject itemUIPrefab;
    [Header("ItemsData")]
    [Space(10)]
    public SO_ItemTexture[] itemData;
    [Header("ShopItemsCanvas")]
    [Space(10)]
    public GameObject ShopItemsCanvas;
    [Header("NutsText")]
    [Space(10)]
    public Text nutsText;
    [Header("PlayerStats")]
    [Space(10)]
    public SO_PlayerStats soPlayerStats;

    private void Awake()
    {
        UI_Events.onStoreButtonPressed += UpdateText;
        gameSaveManagerInstance = GameSaveManager.instance;
    }

    private void OnDestroy()
    {
        UI_Events.onStoreButtonPressed -= UpdateText;

    }

    void Start ()
    {
        UpdateText();
        for (int i = 0; i < itemData.Length; i++)
        {
            gameSaveManagerInstance.LoadGame(itemData[i]);
            GameObject go = Instantiate(itemUIPrefab);
            go.transform.SetParent(ShopItemsCanvas.transform);
            go.transform.localScale = Vector3.one;
            ShopUITextureItem item = go.GetComponent<ShopUITextureItem>();
            item.SetItemTextureSO(itemData[i]);
        }
    }
	


    public void UpdateText()
    {
        gameSaveManagerInstance.LoadGame(soPlayerStats);
        nutsText.text = soPlayerStats.nuts.ToString();
    }
}
