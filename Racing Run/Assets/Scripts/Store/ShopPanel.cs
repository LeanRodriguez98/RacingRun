using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour {
    public Text nutsText;
    private StoreManager storeManager;



    void Start ()
    {
        storeManager = StoreManager.instance;

        UpdateText();
    }
	
    public void UpdateText()
    {
        nutsText.text = storeManager.nuts.ToString();
    }
}
