using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AreYouSure : MonoBehaviour {


    [HideInInspector] public ShopUITextureItem item;
    [HideInInspector] public Animator animator;
    private bool IsPurchased = true;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void NoPuchase()
    {
        animator.SetTrigger("Close");
    }

    public void YesPurchase()
    {
        if (IsPurchased)
        {
            animator.SetTrigger("Close");
            item.Buy();
            IsPurchased = false;
            Invoke("ResetPurchasePosibility", 3.0f);
        }
    }


    private void ResetPurchasePosibility()
    {
        IsPurchased = true;
    }
}
