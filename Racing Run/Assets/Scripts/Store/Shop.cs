using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    [System.Serializable]
    public struct ShopItemTextures
    {
        public ShopItemTexture itemTextures;
        public SO_ItemTexture itemData;

    }


    public Text nutsText;
    public SO_PlayerStats soNuts;
    public ShopItemTextures[] itemTextures;
    public GameObject ShopItemsCanvas;
    void Start ()
    {
        UpdateText();

        for (int i = 0; i < itemTextures.Length; i++)
        {
            itemTextures[i].itemTextures.soItemTextue = itemTextures[i].itemData;
        }

        for (int i = 0; i < itemTextures.Length; i++)
        {
            GameObject go = Instantiate(itemTextures[i].itemTextures.gameObject);
            go.transform.parent = ShopItemsCanvas.transform;
        }
    }
	


    public void UpdateText()
    {
        nutsText.text = soNuts.nuts.ToString();
    }
}
