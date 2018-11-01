using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Events : MonoBehaviour {

    public delegate void UIEvents();
    public static event UIEvents onStoreButtonPressed;

    public static void UpdateStoreItems()
    {
        if (onStoreButtonPressed != null)
            onStoreButtonPressed();
    }
}
