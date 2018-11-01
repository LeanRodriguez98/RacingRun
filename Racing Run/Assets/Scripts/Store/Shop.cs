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
    void Start ()
    {
             
        for (int i = 0; i < itemData.Length; i++)
        {
            GameObject go = Instantiate(itemUIPrefab);
            go.transform.parent = ShopItemsCanvas.transform;
            ShopUITextureItem item = go.GetComponent<ShopUITextureItem>();
            item.SetItemTextureSO(itemData[i]);
        }
    }
	


    public void Update()
    {
        if(nutsDisplayed != soNuts.nuts)
        nutsText.text = soNuts.nuts.ToString();
        nutsDisplayed = soNuts.nuts;
    }
}
