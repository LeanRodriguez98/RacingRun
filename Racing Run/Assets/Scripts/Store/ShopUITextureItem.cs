using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUITextureItem : MonoBehaviour {
    private GameSaveManager gameSaveManagerInstance;
    private AudioManager audioManagerInstance;

    [HideInInspector] public GameObject areYouSurePrefab;
    [HideInInspector] public UI_AreYouSure areYouSureScript;
    [HideInInspector]public SO_ItemTexture soItemTextue;
    [Header("PlayerStats")]
    [Space(10)]
    public SO_PlayerStats soPlayerStats;
    [Header("Item")]
    [Space(10)]
    [Header("       Item - References")]
    [Space(5)]
    public RawImage itemReference;
    public RawImage backgroundIntemReference;
    public Text priceTextReference;
    [Header("       Item - GameObjects")]
    [Space(5)]
    public GameObject nutIcon;
    public GameObject equipedIcon;
    [Header("       Item - Backgrounds")]
    [Space(5)]
    public Texture boughedBackground;
    public Texture notBoughedBackground;
    [Header("AudioClips")]
    [Space(10)]
    public AudioManager.Clip BuyColorSound;
    public AudioManager.Clip ApplyColorSound;

    private void Awake()
    {
        UI_Events.onStoreButtonPressed += UpdateStoreItems;
        gameSaveManagerInstance = GameSaveManager.instance;
        audioManagerInstance = AudioManager.instance;
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
            if(areYouSurePrefab == null)
                areYouSurePrefab = GameObject.FindGameObjectWithTag("AreYouSurePanel");
            if(areYouSureScript == null)
                areYouSureScript = areYouSurePrefab.GetComponent<UI_AreYouSure>();
          
            areYouSureScript.animator.SetTrigger("Open");
            areYouSureScript.item = this;
        }
        else
        {
            TryEquip(false);    
            UpdateData();
        }

    }

    public void EquipItem(bool newItem)
    {
        soPlayerStats.materialName = soItemTextue.materialName;
        if (newItem)
            audioManagerInstance.PlayTriggerSound(BuyColorSound.clip, BuyColorSound.Volume);
        else
            audioManagerInstance.PlayTriggerSound(ApplyColorSound.clip, ApplyColorSound.Volume);

    }

    public void SetItemTextureSO(SO_ItemTexture other)
    {
        soItemTextue = other;
        UpdateItem();

    }

    public void Buy()
    {
        areYouSureScript.animator.SetTrigger("Close");

        soItemTextue.boughted = true;
        soPlayerStats.nuts -= soItemTextue.price;
        TryEquip(true);
        UpdateData();
    }

    public void TryEquip(bool newItem)
    {
        if (soItemTextue.boughted && Resources.Load<Material>(soItemTextue.materialName) != Resources.Load<Material>(soPlayerStats.materialName))
        {
            EquipItem(newItem);
        }
    }

    private void UpdateData()
    {
        gameSaveManagerInstance.SaveGame(soItemTextue);
        gameSaveManagerInstance.SaveGame(soPlayerStats);
        UI_Events.UpdateStoreItems();
    }
}
