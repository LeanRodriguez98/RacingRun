using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUITextureItem : MonoBehaviour {
    [HideInInspector]public SO_ItemTexture soItemTextue;
    public SO_PlayerStats soPlayerStats;
    public RawImage itemReference;
    public Text priceTextReference;
    public GameObject nutIcon;
    public GameObject equipedIcon;
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
        }
        else
        {
            priceTextReference.gameObject.SetActive(true);
            nutIcon.SetActive(true);
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

        
        UpdateItem();
    }

    public void EquipItem()
    {
        soPlayerStats.material = soItemTextue.material;
        UpdateItem();

    }

    public void SetItemTextureSO(SO_ItemTexture other)
    {
        soItemTextue = other;
        UpdateItem();

    }
}
