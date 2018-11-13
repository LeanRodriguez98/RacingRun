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

    private void OnEnable()
    {
        UpdateItem();

    }

    public void UpdateItem()
    {
        itemReference.texture = Resources.Load<Texture>(soItemTextue.iconName);
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

        if (Resources.Load <Material>(soItemTextue.materialName) == Resources.Load<Material>(soPlayerStats.materialName))
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

        if (soItemTextue.boughted && Resources.Load<Material>(soItemTextue.materialName) != Resources.Load<Material>(soPlayerStats.materialName))
        {
            EquipItem();
        }

        gameSaveManagerInstance.SaveGame(soItemTextue);
        gameSaveManagerInstance.SaveGame(soPlayerStats);
        UI_Events.UpdateStoreItems();

    }

    public void EquipItem()
    {
        soPlayerStats.materialName = soItemTextue.materialName;

    }

    public void SetItemTextureSO(SO_ItemTexture other)
    {
        soItemTextue = other;
        UpdateItem();

    }
}
