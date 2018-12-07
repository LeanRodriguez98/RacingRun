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
    public GameObject areYouSurePrefab;
    public UI_AreYouSure areYouSureScript;


    [Header("AudioClips")]
    [Space(10)]
    private AudioManager audioManagerInstance;
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
            TryEquip();    
            UpdateData();
        }

    }

    public void EquipItem()
    {
        soPlayerStats.materialName = soItemTextue.materialName;
        audioManagerInstance.PlaySoundTrigger(ApplyColorSound.clip, ApplyColorSound.Volume);
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
        TryEquip();
        UpdateData();
    }

    public void TryEquip()
    {
        if (soItemTextue.boughted && Resources.Load<Material>(soItemTextue.materialName) != Resources.Load<Material>(soPlayerStats.materialName))
        {
            EquipItem();
        }
    }

    private void UpdateData()
    {
        gameSaveManagerInstance.SaveGame(soItemTextue);
        gameSaveManagerInstance.SaveGame(soPlayerStats);
        UI_Events.UpdateStoreItems();
    }
}
