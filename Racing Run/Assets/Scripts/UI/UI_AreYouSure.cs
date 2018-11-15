using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AreYouSure : MonoBehaviour {


    [HideInInspector] public ShopUITextureItem item;
    [HideInInspector] public Animator animator;

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
        animator.SetTrigger("Close");

        item.Buy();
    }

}
