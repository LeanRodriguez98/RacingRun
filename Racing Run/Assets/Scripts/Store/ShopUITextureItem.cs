using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUITextureItem : MonoBehaviour {
    [HideInInspector]public SO_ItemTexture soItemTextue;
    public SO_PlayerStats soPlayerStats;
    public RawImage itemReference;
    public RawImage backgroundIntemReference;
    public Text priceTextReference;
    public GameObject nutIcon;
    public GameObject equipedIcon;
    public Texture boughedBackground;
    public Texture notBoughedBackground;
    private GameSaveManager gameSaveManagerInstance;

    private void Awake()
    {
        UI_Events.onStoreButtonPressed += UpdateStoreItems;
        gameSaveManagerInstance = GameSaveManager.instance;

    }

    private void OnDestroy()
    {
        UI_Events.onStoreButtonPressed -= UpdateStoreItems;
    }

    public void UpdateStoreItems()
    {
        UpdateItem();
    }

    void Start () {
        UpdateItem();
    }

    public void UpdateItem()
    {
        itemReference.texture = soItemTextue.icon;
        priceTextReference.text = soItemTextue.price.ToString();
        if (soItemTextue.boughted)
        {
            priceTextReference.gameObject.SetActive(false);
            nutIcon.SetActive(false);
            backgroundIntemReference.texture = boughedBackground;
        }
        else
        {
            priceTextReference.gameObject.SetActive(true);
            nutIcon.SetActive(true);
            backgroundIntemReference.texture = notBoughedBackground;

        }

        if (soItemTextue.material == soPlayerStats.material)
        {
            equipedIcon.SetActive(true);
        }
        else
        {
            equipedIcon.SetActive(false);
        }
    }

    public void TryPurchase()
    {
        if (soItemTextue.price <= soPlayerStats.nuts && !soItemTextue.boughted)
        {
            soItemTextue.boughted = true;
            soPlayerStats.nuts -= soItemTextue.price;
        }

        if (soItemTextue.boughted && soItemTextue.material != soPlayerStats.material)
        {
            EquipItem();
        }

        UI_Events.UpdateStoreItems();
        gameSaveManagerInstance.SaveGame(soItemTextue);
        gameSaveManagerInstance.SaveGame(soPlayerStats);
    }

    public void EquipItem()
    {
        soPlayerStats.material = soItemTextue.material;

    }

    public void SetItemTextureSO(SO_ItemTexture other)
    {
        soItemTextue = other;
        UpdateItem();

    }
}
