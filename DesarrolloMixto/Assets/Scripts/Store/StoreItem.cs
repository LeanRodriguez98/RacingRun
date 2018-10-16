using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItem : MonoBehaviour {

    [System.Serializable]
    public struct Item
    {
        public RawImage canvasImage;
        public Texture icon;
        public Text canvasText;
        public int price;
        public Material texture;
        [HideInInspector] public bool boughted;
    };

    public Item item;
    public StoreManager storeManager;
    private PlayerStats playerStats;
    public ShopPanel shopPanel;
    private void Awake()
    {
       
        item.boughted = false;
        item.canvasText.text = item.price.ToString();
        item.canvasImage.texture = item.icon;
    }

    private void Start()
    {
        storeManager = StoreManager.instance;
        playerStats = PlayerStats.instance;
    }

    public void TryPurchase()
    {
        if (storeManager.nuts >
            item.price &&
            !item.boughted)
        {
            storeManager.nuts -= item.price;
            item.boughted = true;
            playerStats.currentMaterial = item.texture;
            shopPanel.UpdateText();
        }
        else if (item.boughted)
        {
            playerStats.currentMaterial = item.texture;
        }
    }
}
