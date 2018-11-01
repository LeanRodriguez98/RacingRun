using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public GameObject itemUIPrefab;       
    public SO_ItemTexture[] itemData;
    public Text nutsText;
    public SO_PlayerStats soNuts;
    public GameObject ShopItemsCanvas;
    private int nutsDisplayed;

    private void Awake()
    {
        UI_Events.onStoreButtonPressed += UpdateText;
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
            GameObject go = Instantiate(itemUIPrefab);
            go.transform.parent = ShopItemsCanvas.transform;
            ShopUITextureItem item = go.GetComponent<ShopUITextureItem>();
            item.SetItemTextureSO(itemData[i]);
        }
    }
	


    public void UpdateText()
    {
        nutsText.text = soNuts.nuts.ToString();
    }
}
