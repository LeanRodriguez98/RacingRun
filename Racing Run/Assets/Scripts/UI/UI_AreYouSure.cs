using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AreYouSure : MonoBehaviour {


    public ShopUITextureItem item;


    public void NoPuchase()
    {
        Destroy(gameObject);
    }

    public void YesPurchase()
    {
        item.Buy();
        Destroy(gameObject);
    }

}
